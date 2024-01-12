using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Net_project
{
    public partial class EditBorrower : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string RetrieveBorrowerInfo(string borrowerIdString)
        {
            try
            {
                int borrowerId = Convert.ToInt32(borrowerIdString);

                string query = $"SELECT * FROM Borrower WHERE borrowerId = {borrowerId}";

                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        return JsonConvert.SerializeObject(new
                        {
                            name = row["borrowerName"].ToString(),
                            fineStatus = row["borrowerFineStatus"].ToString(),
                            age = row["borrowerAge"].ToString(),
                            gender = row["borrowerGender"].ToString(),
                            emailAddress = row["borrowerEmailAddress"].ToString(),
                            phoneNumber = row["borrowerPhoneNumber"].ToString(),
                            address = row["borrowerAddress"].ToString()
                        });
                    }

                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
            return string.Empty;
        }

        [WebMethod]
        public static string UpdateBorrowerInfo(string id, string name, string age, string gender, string fineStatus, string phoneNumber, string emailAddress, string address)
        {
            try
            {
                int borrowerId = Convert.ToInt32(id);
                string borrowerName = name;
                int borrowerAge = Convert.ToInt32(age);
                string borrowerGender = gender;
                Boolean borrowerFineStatus = Convert.ToBoolean(fineStatus);
                string borrowerEmailAddress = emailAddress;
                string borrowerPhoneNumber = phoneNumber;
                string borrowerAddress = address;

                string query = $"UPDATE Borrower SET borrowerName = '{borrowerName}', borrowerAge = {borrowerAge}, borrowerGender = '{borrowerGender}', borrowerFineStatus = '{borrowerFineStatus}', borrowerEmailAddress = '{borrowerEmailAddress}', borrowerPhoneNumber = '{borrowerPhoneNumber}', borrowerAddress = '{borrowerAddress}' WHERE borrowerId = {borrowerId}";

                using (SqlConnection con = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = TestDatabase; Integrated Security = True; Pooling = False"))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(query, con);
                    int result = cmd.ExecuteNonQuery();

                    return result.ToString();    
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}