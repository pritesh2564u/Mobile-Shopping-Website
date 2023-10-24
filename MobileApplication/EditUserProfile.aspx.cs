using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;

namespace MobileApplication
{
    public partial class EditUserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userEmail = Session["Email"] as string;

                if (userEmail != null)
                {
                    EditEmail.Value = userEmail;
                }
                else
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string username = EditUsername.Value.Trim();
            string email = EditEmail.Value.Trim();
            string password = EditPassword.Value.Trim();
            string confirmPassword = EditConfirmPassword.Value.Trim();

            string userEmail = Session["Email"] as string;

            if (email != userEmail && UserExists(email))
            {
                Response.Write("<div id='userExistsAlert' class='bg-red-100 border border-red-400 text-red-700 px-4 py-2 rounded relative mt-1'>");
                Response.Write("<strong class='font-bold'>User already exists!</strong>");
                Response.Write("<span class='block sm:inline'>A user with the same email address already exists.</span>");
                Response.Write("</div>");
                return;
            }

            if (password != confirmPassword)
            {
                Response.Write("<div class='bg-red-100 border border-red-400 text-red-700 px-4 py-2 rounded relative mt-1'>");
                Response.Write("<strong class='font-bold'>Passwords do not match! </strong>");
                Response.Write("<span class='block sm:inline'>Please make sure the passwords match.</span>");
                Response.Write("</div>");
                return;
            }

            string conn = WebConfigurationManager.ConnectionStrings["MobileApplication"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                string updateQuery = "UPDATE Users SET Username = @username, Email = @email, Password = @password WHERE Email = @userEmail";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@UserEmail", userEmail);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        Response.Write("Failed to update profile.");
                    }
                }
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
