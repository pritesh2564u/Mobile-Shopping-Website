using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileApplication
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }
        protected void btnSignup_Click(object sender, EventArgs e)
        {
            string username = SignupUsername.Value;
            string email = SignupEmail.Value;
            string password = SignupPassword.Value;
            string confirmpassword = SignupConfirmPassword.Value;

            if (UserExists(email))
            {
                Response.Write("<div id='userExistsAlert' class='bg-red-100 border border-red-400 text-red-700 px-4 py-2 rounded relative mt-1'>");
                Response.Write("<strong class='font-bold'>User already exists!</strong>");
                Response.Write("<span class='block sm:inline'>A user with the same email address already exists.</span>");
                Response.Write("</div>");
                return;
            }

            if (password != confirmpassword)
            {
                Response.Write("<div class='bg-red-100 border border-red-400 text-red-700 px-4 py-2 rounded relative mt-1'>");
                Response.Write("<strong class='font-bold'>Passwords do not match! </strong>");
                Response.Write("<span class='block sm:inline'>Please make sure the passwords match.</span>");
                Response.Write("</div>");
                return;
            }

            string conn = WebConfigurationManager.ConnectionStrings["MobileApplication"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string insertQuery = "INSERT INTO Users (Username, Email, Password) VALUES (@username, @email, @password)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Response.Redirect("Login.aspx");
                        }
                        else
                        {
                            
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private bool UserExists(string email)
        {
            string connString = WebConfigurationManager.ConnectionStrings["MobileApplication"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Users WHERE Email = @email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    int userCount = (int)command.ExecuteScalar();

                    return userCount > 0;
                }
            }
        }

    }
}