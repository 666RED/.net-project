﻿using System;
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
                String command = "SELECT * From librarian WHERE email = @email OR telephone = @telephone";
                SqlDataAdapter cmdCheck = new SqlDataAdapter;
                cmdCheck.InsertCommand = new SqlCommand(command, con);

                cmdCheck.InsertCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = email.Value;
                cmdCheck.InsertCommand.Parameters.Add("@telephone", SqlDbType.VarChar).Value = telephone.Value;
                SqlDataReader reader = cmdCheck.InsertCommand.ExecuteReader();
                if (reader.Read())
                {
                    if (reader[0].Equals(email.Value))
                    {
                        validEmail.Text = "Email already exist";
                    }
                    else
                    {
                        validEmail.Text = "";
                    }
                }
    

                con.Open();
                SqlDataAdapter cmd = new SqlDataAdapter();
                cmd.InsertCommand = new SqlCommand("INSERT INTO librarian VALUES(@librarainId, @password, @email, @username, @gender, @telephone) ", con);

                cmd.InsertCommand.Parameters.Add("@librarainId", SqlDbType.NText).Value = librarianID.Value;
                cmd.InsertCommand.Parameters.Add("@password", SqlDbType.VarChar).Value = password.Value;
                cmd.InsertCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = email.Value;
                cmd.InsertCommand.Parameters.Add("@username", SqlDbType.VarChar).Value = username.Value;
                cmd.InsertCommand.Parameters.Add("@gender", SqlDbType.VarChar).Value = gender.Value;
                cmd.InsertCommand.Parameters.Add("@telephone", SqlDbType.VarChar).Value = telephone.Value;

                cmd.InsertCommand.ExecuteNonQuery();
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