using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Net_project
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateCardData();
        }

        private void PopulateCardData()
        {
            mostPopularBook();
            lateReturn();
            leastPopularBook();
        }

        public void mostPopularBook()
        {
            String command = "SELECT bookTitle, bookAuthor, quantity " +
                "FROM (SELECT bookId, Count(*) AS quantity FROM Borrower_Book GROUP BY bookId) AS temp " +
                "INNER JOIN Book ON temp.bookId = Book.bookId " +
                "ORDER BY quantity DESC";

            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog = TestDatabase; Trusted_Connection = true; Pooling = False");
            SqlDataAdapter cmdCheck = new SqlDataAdapter();
            cmdCheck.InsertCommand = new SqlCommand(command, con);
            con.Open();
            SqlDataReader reader = cmdCheck.InsertCommand.ExecuteReader();
            if (reader.Read())
            {
                MostPopularBookTitle.Text = "Book Title: " + reader.GetString(0);
                MostPopularBookAuthor.Text = "Author: " + reader.GetString(1);
                MostQuantity.Text = "Borrowed Quantity: " + reader["quantity"];
            }
            con.Close();
        }

        public void lateReturn()
        {
            DateTime currentDate = DateTime.Now;
            string now = currentDate.ToString("yyyy/MM/dd");

            String command = "SELECT COUNT(*) AS 'quantity' FROM Borrower_Book WHERE returnDate < '" + now + "' AND returnStatus = 0";

            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog = TestDatabase; Trusted_Connection = true; Pooling = False");
            SqlDataAdapter cmdCheck = new SqlDataAdapter();
            cmdCheck.InsertCommand = new SqlCommand(command, con);
            con.Open();
            SqlDataReader reader = cmdCheck.InsertCommand.ExecuteReader();
            if (reader.Read())
            {
                CurrentlyLateforReturn.Text = "Currently Late for Return: " + reader["quantity"];
            }
            reader.Close();

            command = "SELECT COUNT(*) AS 'quantity' FROM Borrower_Book WHERE DATEDIFF(day, borrowDate, returnDate) > 30; ";
            cmdCheck.InsertCommand = new SqlCommand(command, con);
            reader = cmdCheck.InsertCommand.ExecuteReader();
            if (reader.Read())
            {
                TotalLateReturns.Text = "Total Late Returns: " + reader["quantity"];
            }

            con.Close();
        }

        public void leastPopularBook()
        {
            String command = "SELECT bookTitle, bookAuthor, COALESCE(quantity, 0) AS quantity FROM (SELECT bookId, COUNT(*) AS 'quantity' FROM Borrower_Book GROUP BY bookId) AS temp RIGHT JOIN Book ON temp.bookId = Book.bookId ORDER BY quantity;";

            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog = TestDatabase; Trusted_Connection = true; Pooling = False");
            SqlDataAdapter cmdCheck = new SqlDataAdapter();
            cmdCheck.InsertCommand = new SqlCommand(command, con);
            con.Open();
            SqlDataReader reader = cmdCheck.InsertCommand.ExecuteReader();
            if (reader.Read())
            {
                LeastPopularBookTitle.Text = "Book Title: " + reader.GetString(0);
                LeastPopularBookAuthor.Text = "Author: " + reader.GetString(1);
                LeastQuantity.Text = "Borrowed Quantity: " + reader["quantity"];
            }
            con.Close();
        }
    }
}