using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Net_project
{
    public partial class ViewBook : System.Web.UI.Page
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
                        FetchBookData(bookId);
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
        private void FetchBookData(int bookId)
        {
            try
            {
                string query = $"SELECT * FROM Book WHERE bookId={bookId}";
                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        PopulateFormFields(reader);
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

        private void PopulateFormFields(SqlDataReader reader)
        {
            title.Value = reader["bookTitle"].ToString();
            author.Value = reader["bookAuthor"].ToString();
            pages.Value = reader["bookPages"].ToString();
            isbn.Value = reader["bookISBN"].ToString();
            publisher.Value = reader["bookPublisher"].ToString();
            publishDate.Value = ((DateTime)reader["bookPublicationDate"]).ToString("M/d/yyyy"); ;
            language.Value = reader["bookLanguage"].ToString();
            price.Value = "RM " + reader["bookPrice"].ToString();
            rackNumber.Value = reader["bookRackNumber"].ToString();
            availability.Value = (bool)reader["bookAvailability"] ? "Available" : "Unavailable";
        }
    }
}