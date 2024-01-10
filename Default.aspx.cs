using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System;

namespace Net_project
{
    public partial class Default : System.Web.UI.Page
    {
        private int currentPage = 1; // Default to the first page
        private String searchString = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["page"] != null)
                {
                    currentPage = int.Parse(Request.QueryString["page"]);
                }

                if (Request.QueryString["value"] != null) {
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

                string query = $"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY bookAvailability ASC) AS RowNum, * FROM Book) AS RowConstrainedResult WHERE RowNum BETWEEN {startIndex} AND {endIndex}";

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

        public void BindSearchData()
        {
            try
            {
                int pageSize = 8;
                int startIndex = 1 + (currentPage - 1) * pageSize;
                int endIndex = currentPage * pageSize;

                string query = $@"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY bookAvailability ASC) AS RowNum, * FROM Book WHERE bookTitle LIKE '%{searchString}%') AS RowConstrainedResult WHERE RowNum BETWEEN {startIndex} AND {endIndex}";

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

        public void GeneratePage()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM Book";
                    SqlCommand cmd = new SqlCommand(query, con);
                    int totalBooks = (int)cmd.ExecuteScalar();

                    int numberOfPages = (int)Math.Ceiling((double)totalBooks / 8);

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
                    string query = $"SELECT COUNT(*) FROM Book WHERE bookTitle LIKE '%{searchString}%'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    int totalBooks = (int)cmd.ExecuteScalar();

                    int numberOfPages = (int)Math.Ceiling((double)totalBooks / 8);

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

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string inputValue = Request.Form["search"];
            Label1.Text = inputValue;
        }

    }
}


