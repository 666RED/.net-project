using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection.Emit;

namespace Net_project
{
    public partial class EditBook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string RetrieveBookInfo(string bookIdString)
        {
            try
            {
                int bookId = Convert.ToInt32(bookIdString);

                string query = $"SELECT * FROM Book WHERE bookId = {bookId}";

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
                            title = row["bookTitle"].ToString(),
                            author = row["bookAuthor"].ToString(),
                            pages = row["bookPages"].ToString(),
                            isbn = row["bookISBN"].ToString(),
                            publisher = row["bookPublisher"].ToString(),
                            publishDate = row.Field<DateTime>("bookPublicationDate").ToString("yyyy-MM-dd"),
                            language = row["bookLanguage"].ToString(),
                            price = row["bookPrice"].ToString(),
                            rackNumber = row["bookRackNumber"].ToString(),
                            availability = row["bookAvailability"].ToString(),
                        });
                    }

                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
            return string.Empty;
        }

        [WebMethod]
        public static string UpdateBookInfo(string id, string title, string author, string pages, string isbn, string publisher, string publishDate, string language, string price, string rackNumber, string availability)
        {
            try
            {
                int bookId = Convert.ToInt32(id);
                string bookTitle = title;
                string bookAuthor = author;
                int bookPages = Convert.ToInt32(pages);
                string bookISBN = isbn;
                string bookPublisher = publisher;
                DateTime bookPublicationDate = Convert.ToDateTime(publishDate);
                string bookLanguage = language;
                double bookPrice = Convert.ToDouble(price);
                string bookRackNumber = rackNumber;
                bool bookAvailability = Convert.ToBoolean(availability);

                string query = @"UPDATE Book SET bookTitle = @BookTitle, bookAuthor = @BookAuthor, bookPages = @BookPages, bookISBN = @BookISBN, bookPublisher = @BookPublisher, bookPublicationDate = @BookPublicationDate, bookLanguage = @BookLanguage, bookPrice = @BookPrice, bookRackNumber = @BookRackNumber, bookAvailability = @BookAvailability WHERE bookId = @BookId";

                using (SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS; Initial Catalog=TestDatabase; Integrated Security=True; Pooling=False"))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@BookTitle", bookTitle);
                    cmd.Parameters.AddWithValue("@BookAuthor", bookAuthor);
                    cmd.Parameters.AddWithValue("@BookPages", bookPages);
                    cmd.Parameters.AddWithValue("@BookISBN", bookISBN);
                    cmd.Parameters.AddWithValue("@BookPublisher", bookPublisher);
                    cmd.Parameters.AddWithValue("@BookPublicationDate", bookPublicationDate);
                    cmd.Parameters.AddWithValue("@BookLanguage", bookLanguage);
                    cmd.Parameters.AddWithValue("@BookPrice", bookPrice);
                    cmd.Parameters.AddWithValue("@BookRackNumber", bookRackNumber);
                    cmd.Parameters.AddWithValue("@BookAvailability", bookAvailability);
                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    int result = cmd.ExecuteNonQuery();

                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

        }
    }
}