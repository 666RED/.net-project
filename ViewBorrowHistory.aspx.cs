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
    public partial class ViewBorrowHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            try
            {
                int bookId = -1;
                if (Request.QueryString["id"] != null)
                {
                    bookId = int.Parse(Request.QueryString["id"]);
                }

                string query = $"SELECT bb.borrowDate, bb.returnDate, bo.bookTitle, br.borrowerName FROM Borrower_Book bb INNER JOIN Book bo ON bb.bookId = bo.bookId INNER JOIN Borrower br ON bb.borrowerId = br.borrowerId WHERE bb.bookId = {bookId}";

                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        BorrowTable.DataSource = dt;
                        BorrowTable.DataBind();
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