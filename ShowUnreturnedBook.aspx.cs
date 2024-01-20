using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Net_project
{
    public partial class ShowUnreturnedBook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                int borrowerId = Convert.ToInt32(Request.QueryString["id"]);
                RetrieveUnreturnedBookInfo(borrowerId);
            }
        }

        public void RetrieveUnreturnedBookInfo(int borrowerId)
        {
            try
            {

                string query = $"SELECT bb.bookId, b.bookTitle, bb.borrowDate, bb.returnDate FROM Borrower_Book bb JOIN Book b ON bb.bookId = b.bookId WHERE bb.returnStatus = 0 AND bb.returnDate < GETDATE() AND bb.borrowerId = {borrowerId}";

                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        BookTable.DataSource = dt;
                        BookTable.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
    }
}