using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Net_project
{
    public partial class AddNewBook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string AddBook(string id, string title, string author, string pages, string isbn, string publisher, string publishDate, string language, string price, string rackNumber, string availability)
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
                Boolean bookAvailability = Convert.ToBoolean(availability);

                string query = $"INSERT INTO Book (bookTitle, bookAuthor, bookPages, bookISBN, bookPublisher, bookPublicationDate, bookLanguage, bookPrice, bookRackNumber, bookAvailability, deleted) VALUES ('{bookTitle}', '{bookAuthor}', {bookPages}, '{bookISBN}', '{bookPublisher}', '{bookPublicationDate}', '{bookLanguage}', {bookPrice}, '{bookRackNumber}', '{bookAvailability}', 0)";

                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(query, con);
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