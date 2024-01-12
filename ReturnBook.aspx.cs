using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Net_project
{
    public partial class ReturnBook : System.Web.UI.Page
    {
        public static int historyId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string bookIdString = Request.QueryString["id"];
                int bookId = Convert.ToInt32(bookIdString);
                RetrieveBorrowRecord(bookId);
            }
        }

        public void RetrieveBorrowRecord(int bookId)
        {
            string bookQuery = $"SELECT bookTitle FROM Book WHERE bookId = {bookId}";

            using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(bookQuery, con);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    title.Value = reader["bookTitle"].ToString();
                }
                else
                {
                    Response.Redirect("Error.aspx");
                }
            }

            string borrowerQuery = $"SELECT TOP 1 b.borrowerName, bb.borrower_bookId, bb.borrowDate, bb.returnDate FROM Borrower b JOIN Borrower_Book bb ON b.borrowerId = bb.borrowerId WHERE bb.bookId = {bookId} ORDER BY borrower_bookId DESC";

            using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(borrowerQuery, con);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    historyId = Convert.ToInt32(reader["borrower_bookId"]);
                    name.Value = reader["borrowerName"].ToString();
                    if (DateTime.TryParse(reader["borrowDate"].ToString(), out DateTime borrowDateTime))
                    {
                        borrowDate.Value = borrowDateTime.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        borrowDate.Value = "Invalid Date";
                    }

                    if (DateTime.TryParse(reader["returnDate"].ToString(), out DateTime returnDateTime))
                    {
                        returnDate.Value = returnDateTime.ToString("yyyy-MM-dd");
                        if (returnDateTime < DateTime.Now)
                        {
                            lateReturn.Style["display"] = "";
                        }
                    }
                    else
                    {
                        returnDate.Value = "Invalid Date";
                    }
                }
                else
                {
                    Response.Redirect("Error.aspx");
                }
            }
        }

        [WebMethod]
        public static string UpdateBookAvailability(string bookIdString)
        {
            try
            {
                int bookId = Convert.ToInt32(bookIdString);

                string bookQuery = $"UPDATE Book SET bookAvailability = 1 WHERE bookId = {bookId}";

                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();
                    SqlCommand cmdInsert = new SqlCommand(bookQuery, con);
                    cmdInsert.ExecuteNonQuery();
                }

                string borrowerBookQuery = $"UPDATE Borrower_Book SET returnDate = GETDATE(), returnStatus = 1 WHERE borrower_bookId = {historyId}";

                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();
                    SqlCommand cmdInsert = new SqlCommand(borrowerBookQuery, con);
                    cmdInsert.ExecuteNonQuery();
                }

                string updateQuery = $"UPDATE Borrower SET borrowerFineStatus = CASE WHEN (SELECT COUNT(*) FROM Borrower_Book WHERE borrowerId = (SELECT borrowerId FROM Borrower_Book WHERE borrower_bookId = {historyId}) AND returnStatus = 0 AND returnDate < GETDATE()) > 0 THEN 1 ELSE 0 END WHERE borrowerId = (SELECT borrowerId FROM Borrower_Book WHERE borrower_bookId = {historyId})";

                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(updateQuery, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return "Success";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}