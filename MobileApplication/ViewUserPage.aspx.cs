using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MobileApplication
{
    public partial class ViewUserPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DataTable userTable = FetchUserDataFromDatabase();

                foreach (DataRow row in userTable.Rows)
                {
                    string userid = row["Id"].ToString();
                    string username = row["Username"].ToString();
                    string email = row["email"].ToString();
                    string password = row["Password"].ToString();

                    if(password != "Admin@_25")
                    {
                        HtmlGenericControl cardDiv = new HtmlGenericControl("div");
                        cardDiv.Attributes["class"] = "border border-gray-300 rounded-lg bg-white shadow-xl p-4 m-4 gap-4 hover:border-gray-400 card-div";

                        Label usernameLabel = new Label();
                        usernameLabel.Text = "Username: " + username;
                        usernameLabel.CssClass = "text-lg font-semibold text-gray-800";

                        Label emailLabel = new Label();
                        emailLabel.Text = "Email: " + email;
                        emailLabel.CssClass = "text-base mt-3 text-gray-600";

                        Label passwordLabel = new Label();
                        passwordLabel.Text = "Password: " + password;
                        passwordLabel.CssClass = "text-base mt-3 text-gray-600";

                        cardDiv.Controls.Add(usernameLabel);
                        cardDiv.Controls.Add(new LiteralControl("<br />"));
                        cardDiv.Controls.Add(emailLabel);
                        cardDiv.Controls.Add(new LiteralControl("<br />"));
                        cardDiv.Controls.Add(passwordLabel);

                        // Add the card to the userContainer
                        userContainer.Controls.Add(cardDiv);
                    }
                }
            }
        }

        private DataTable FetchUserDataFromDatabase()
        {
            string conn = WebConfigurationManager.ConnectionStrings["MobileApplication"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                string query = "SELECT * FROM Users";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void EditProfile_Click(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Write("User is authenticated.");
                Response.Redirect("EditUserProfile.aspx");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string email = Session["Email"] as string;
            if (!string.IsNullOrEmpty(email))
            {
                string conn = WebConfigurationManager.ConnectionStrings["MobileApplication"].ConnectionString;

                string deleteQuery = "DELETE FROM Users WHERE Email = @email";

                using (SqlConnection connection = new SqlConnection(conn))
                {
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        try
                        {
                            connection.Open();

                            int rowAffected = command.ExecuteNonQuery();

                            if (rowAffected > 0)
                            {
                                Response.Redirect("Signup.aspx");
                            }

                            else
                            {
                                Response.Write("Please Enter Correct Details");
                            }
                        }

                        catch (Exception ex)
                        {
                            Response.Write("Some is Occured, Please try again to delete the Account.");
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}