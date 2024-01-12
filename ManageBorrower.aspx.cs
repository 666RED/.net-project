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
    public partial class ViewBorrower : System.Web.UI.Page
    {
        private int currentPage = 1; // Default to the first page
        private String searchString = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                updateFineStatus();
                if (Request.QueryString["page"] != null)
                {
                    currentPage = int.Parse(Request.QueryString["page"]);
                }

                if (Request.QueryString["value"] != null)
                {
                    if (Request.QueryString["page"] == null)
                    {
                        currentPage = 1;
                    }
                    searchString = Request.QueryString["value"];
                    BindSearchData();
                    GenerateSearchPage();
                    return;
                }

                BindData();
                GeneratePage();
            }
        }

        private void BindData()
        {
            try
            {

                int pageSize = 8;
                int startIndex = 1 + (currentPage - 1) * pageSize;
                int endIndex = currentPage * pageSize;

                string query = $"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY borrowerFineStatus DESC, borrowerName ASC) AS RowNum, * FROM Borrower) AS RowConstrainedResult WHERE RowNum BETWEEN {startIndex} AND {endIndex}";

                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        BorrowerTable.DataSource = dt;
                        BorrowerTable.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        public void BindSearchData()
        {
            try
            {
                int pageSize = 8;
                int startIndex = 1 + (currentPage - 1) * pageSize;
                int endIndex = currentPage * pageSize;

                string query = $"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY borrowerFineStatus DESC, borrowerName ASC) AS RowNum, * FROM Borrower WHERE borrowerName LIKE '%{searchString}%') AS RowConstrainedResult WHERE RowNum BETWEEN {startIndex} AND {endIndex}";

                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        BorrowerTable.DataSource = dt;
                        BorrowerTable.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        public void GeneratePage()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM Borrower";
                    SqlCommand cmd = new SqlCommand(query, con);
                    int totalBorrowers = (int)cmd.ExecuteScalar();

                    int numberOfPages = (int)Math.Ceiling((double)totalBorrowers / 8);

                    for (int i = 1; i <= numberOfPages; i++)
                    {
                        Label pageLabel = new Label();
                        pageLabel.ID = "PageLabel_" + i;
                        pageLabel.Text = i.ToString();
                        pageLabel.CssClass = "mx-2 border-0 page-label";
                        pageLabel.Style["cursor"] = "pointer";
                        pageLabel.Attributes["onclick"] = $"handlePageLabelClick({i});";

                        PageContainer.Controls.Add(pageLabel);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        public void GenerateSearchPage()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();
                    string query = $"SELECT COUNT(*) FROM Borrower WHERE borrowerName LIKE '%{searchString}%'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    int totalBorrowers = (int)cmd.ExecuteScalar();

                    int numberOfPages = (int)Math.Ceiling((double)totalBorrowers / 8);

                    for (int i = 1; i <= numberOfPages; i++)
                    {
                        Label pageLabel = new Label();
                        pageLabel.ID = "PageLabel_" + i;
                        pageLabel.Text = i.ToString();
                        pageLabel.CssClass = "mx-2 border-0 page-label";
                        pageLabel.Style["cursor"] = "pointer";
                        pageLabel.Attributes["onclick"] = $"handleSearchPageLabelClick({i}, '{searchString}');";

                        PageContainer.Controls.Add(pageLabel);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected int CalculateItemIndex(int index)
        {
            return index + (currentPage - 1) * 8;
        }

        public void updateFineStatus()
        {
            try
            {
                string query = $"UPDATE Borrower SET borrowerFineStatus = 1 FROM Borrower b INNER JOIN Borrower_Book bb ON b.borrowerId = bb.borrowerId WHERE bb.returnDate < GETDATE()";


                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();
                    SqlCommand cmdInsert = new SqlCommand(query, con);
                    cmdInsert.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
    }
}