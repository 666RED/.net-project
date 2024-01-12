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
    public partial class BorrowHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            try
            {
                int borrowerId = -1;
                if (Request.QueryString["id"] != null)
                {
                    borrowerId = int.Parse(Request.QueryString["id"]);
                }

                string query = $"SELECT bb.borrowDate, bb.returnDate, bo.bookTitle, br.borrowerName FROM Borrower_Book bb INNER JOIN Book bo ON bb.bookId = bo.bookId INNER JOIN Borrower br ON bb.borrowerId = br.borrowerId INNER JOIN Book b ON b.bookId = bb.bookId WHERE bb.borrowerId = {borrowerId} AND b.deleted = 0";

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