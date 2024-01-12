using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Policy;

namespace Net_project
{
    public partial class BorrowBook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the id from the query string
                string idParam = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(idParam))
                {
                    int bookId;
                    if (int.TryParse(idParam, out bookId))
                    {
                        FetchBookTitle(bookId);
                    }
                    else
                    {
                        Response.Redirect("Error.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Error.aspx");
                }
            }
        }

        private void FetchBookTitle(int bookId)
        {
            try
            {
                string query = $"SELECT bookTitle FROM Book WHERE bookId={bookId}";
                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);

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
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }


        [WebMethod]
        public static string RetrieveBorrowerInformation()
        {
            try
            {
                string query = "SELECT borrowerId, borrowerName FROM Borrower ORDER BY borrowerName";

                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    string jsonResult = JsonConvert.SerializeObject(dt);

                    return jsonResult;
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [WebMethod]
        public static string RetrieveBorrowerInformationByName(string borrowerId)
        {
            try
            {
                string query = $"SELECT * FROM Borrower WHERE borrowerId = '{borrowerId}'";

                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        return JsonConvert.SerializeObject(new
                        {
                            fineStatus = row["borrowerFineStatus"].ToString(),
                            age = row["borrowerAge"].ToString(),
                            gender = row["borrowerGender"].ToString(),
                            emailAddress = row["borrowerEmailAddress"].ToString(),
                            phoneNumber = row["borrowerPhoneNumber"].ToString(),
                            address = row["borrowerAddress"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // handle exception
            }

            return string.Empty;
        }

        [WebMethod]
        public static string BorrowBookAndUpdateAvailability(string borrowerId, string bookId, string borrowDate, string returnDate)
        {
            try
            {
                string insertBorrowerBookQuery = $"INSERT INTO Borrower_Book (borrowerId, bookId, borrowDate, returnDate, returnStatus) VALUES ({borrowerId}, {bookId}, '{borrowDate}', '{returnDate}', 0)";
                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();
                    SqlCommand cmdInsert = new SqlCommand(insertBorrowerBookQuery, con);
                    cmdInsert.ExecuteNonQuery();
                }

                string updateBookAvailabilityQuery = $"UPDATE Book SET bookAvailability = 0 WHERE bookId = {bookId}";
                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();
                    SqlCommand cmdUpdate = new SqlCommand(updateBookAvailabilityQuery, con);
                    cmdUpdate.ExecuteNonQuery();
                }

                return "Success";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [WebMethod]
        public static string AddBorrowerAndUpdateAvailability(string name, string bookId, string age, string gender, string fineStatus, string email, string phoneNumber, string address, string borrowDate, string returnDate)
        {
            try
            {
                int borrowerAge = int.Parse(age);
                int borrowerFineStatus = fineStatus.ToLower() == "true" ? 1 : 0;
                int newBorrowerId;

                string addNewBorrowerQuery = $"INSERT INTO Borrower (borrowerName, borrowerGender, borrowerAge, borrowerEmailAddress, borrowerPhoneNumber, borrowerAddress, borrowerFineStatus) OUTPUT INSERTED.borrowerId VALUES ('{name}', '{gender}', {borrowerAge}, '{email}', '{phoneNumber}', '{address}', {borrowerFineStatus})";

                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();
                    SqlCommand cmdInsert = new SqlCommand(addNewBorrowerQuery, con);
                    newBorrowerId = (int)cmdInsert.ExecuteScalar();
                }

                string updateBookAvailabilityQuery = $"UPDATE Book SET bookAvailability = 0 WHERE bookId = {bookId}";
                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();
                    SqlCommand cmdUpdate = new SqlCommand(updateBookAvailabilityQuery, con);
                    cmdUpdate.ExecuteNonQuery();
                }

                string insertBorrowerBookQuery = $"INSERT INTO Borrower_Book (borrowerId, bookId, borrowDate, returnDate, returnStatus) VALUES ({newBorrowerId}, {bookId}, '{borrowDate}', '{returnDate}', 0)";
                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();
                    SqlCommand cmdInsert = new SqlCommand(insertBorrowerBookQuery, con);
                    cmdInsert.ExecuteNonQuery();
                }

                return "Success";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }


    }
}