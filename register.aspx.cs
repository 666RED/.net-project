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
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void UserRegister(object sender, EventArgs e)
        {
            

            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog = TestDatabase; Trusted_Connection = true; Pooling = False");
            try
            {
<<<<<<< HEAD
                validLibrarianID.Text = "";
                validEmail.Text = "";
                validTelephone.Text = "";
                String command = "SELECT * FROM librarian WHERE librarianId LIKE @librarianId OR email = @email OR telephone = @telephone";
=======
                String command = "SELECT * From librarian WHERE email = @email OR telephone = @telephone";
>>>>>>> 5e453cd5e1185ee596a8930f6aa597473984d59c
                SqlDataAdapter cmdCheck = new SqlDataAdapter();
                cmdCheck.InsertCommand = new SqlCommand(command, con);
                con.Open();

                cmdCheck.InsertCommand.Parameters.Add("@librarianId", SqlDbType.NText).Value = librarianID.Value;
                cmdCheck.InsertCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = email.Value;
                cmdCheck.InsertCommand.Parameters.Add("@telephone", SqlDbType.VarChar).Value = telephone.Value;
                SqlDataReader reader = cmdCheck.InsertCommand.ExecuteReader();
                if (reader.Read())
                {
                    if (((String)reader[0]) == librarianID.Value)
                    {
                        validLibrarianID.Text = "Librarian already exist";
                        return;
                    }
                    else if (((String)reader[2]).Equals(email.Value))
                    {
                        validEmail.Text = "Email already exist!";
                        return;
                    }
                    else if (((String)reader[5]).Equals(telephone.Value))
                    {
                        validTelephone.Text = "Telephone already exist!";
                        return;
                    }
                }
                reader.Close();
    
                SqlDataAdapter cmd = new SqlDataAdapter();
                cmd.InsertCommand = new SqlCommand("INSERT INTO librarian VALUES(@librarianId, @password, @email, @username, @gender, @telephone) ", con);

                cmd.InsertCommand.Parameters.Add("@librarianId", SqlDbType.NText).Value = librarianID.Value;
                cmd.InsertCommand.Parameters.Add("@password", SqlDbType.VarChar).Value = password.Value;
                cmd.InsertCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = email.Value;
                cmd.InsertCommand.Parameters.Add("@username", SqlDbType.VarChar).Value = username.Value;
                cmd.InsertCommand.Parameters.Add("@gender", SqlDbType.VarChar).Value = gender.Value;
                cmd.InsertCommand.Parameters.Add("@telephone", SqlDbType.VarChar).Value = telephone.Value;

                cmd.InsertCommand.ExecuteNonQuery();

                string script = "alert('Registration successful!'); setTimeout(function(){window.location.href='login.aspx'}, 0);";
                ScriptManager.RegisterStartupScript(this, GetType(), "RegistrationSuccess", script, true);
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