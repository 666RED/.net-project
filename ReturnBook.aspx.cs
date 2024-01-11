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
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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

            string borrowerQuery = $"SELECT b.BorrowerName, bb.borrowDate, bb.returnDate FROM Borrower b JOIN Borrower_Book bb ON b.borrowerId = bb.borrowerId WHERE bb.bookId = {bookId}";

            using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
            {

            }
        }
    }
}