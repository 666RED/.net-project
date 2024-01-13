using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace Net_project
{
    public partial class login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UserLogin(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog = TestDatabase; Trusted_Connection = true; Pooling = False");
            try
            {
                validLibrarianID.Text = "";
                validPassword.Text = "";
                String command = "SELECT * FROM librarian WHERE librarianId LIKE @librarianId";
                SqlDataAdapter cmdCheck = new SqlDataAdapter();
                cmdCheck.InsertCommand = new SqlCommand(command, con);
                con.Open();

                cmdCheck.InsertCommand.Parameters.Add("@librarianId", SqlDbType.NText).Value = librarianID.Value;
                SqlDataReader reader = cmdCheck.InsertCommand.ExecuteReader();
                if (reader.Read())
                {
                    if (((String)reader[1]).Equals(password.Value))
                    {
                        string script = "alert('Login successful!'); setTimeout(function(){window.location.href='Default.aspx'}, 0);";
                        ScriptManager.RegisterStartupScript(this, GetType(), "LoginSuccess", script, true);
                    }
                    else
                    {
                        validPassword.Text = "Password Wrong!";
                    }
                }
                else
                {
                    validLibrarianID.Text = "Librarian ID does not exist!";
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }
    }
}