using Newtonsoft.Json;
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
    public partial class ViewBorrower1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string borrowerIdString = Request.Params["id"];
                

                if (!string.IsNullOrEmpty(borrowerIdString))
                {
                    int borrowerId;
                    if (int.TryParse(borrowerIdString, out borrowerId))
                    {
                        RetrieveBorrowerInfo(borrowerId);
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

        public void RetrieveBorrowerInfo(int borrowerId)
        {
            try
            {
                string query = $"SELECT * FROM Borrower WHERE borrowerId = {borrowerId}";

                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                        
                    if(reader.Read())
                    {
                        name.Value = reader["borrowerName"].ToString();
                        age.Value = reader["borrowerAge"].ToString();
                        gender.Value = reader["borrowerGender"].ToString();
                        fineStatus.Value = reader["borrowerFineStatus"].ToString();
                        email.Value = reader["borrowerEmailAddress"].ToString();
                        phoneNumber.Value = reader["borrowerPhoneNumber"].ToString();
                        address.Value = reader["borrowerAddress"].ToString();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }
    }
}