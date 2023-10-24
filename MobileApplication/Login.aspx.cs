using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileApplication
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = LoginEmail.Value;
            string password = LoginPassword.Value;

            string conn = WebConfigurationManager.ConnectionStrings["MobileApplication"].ConnectionString;

            using(SqlConnection con = new SqlConnection(conn))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @email AND Password = @password";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    con.Open();
                    int userCount = (int)cmd.ExecuteScalar();

                    if(userCount > 0 && password == "Admin@_25")
                    {
                        Session["Email"] = email;
                        Response.Redirect("AdminPage.aspx");
                    }

                    if(userCount > 0)
                    {
                        Session["Email"] = email;
                        Response.Redirect("HomePage.aspx");
                    }
                    else
                    {
                        Response.Write("<div id='userNotExistsAlert' class='bg-red-100 border border-red-400 text-red-700 px-4 py-2 rounded relative mt-1'>");
                        Response.Write("<strong class='font-bold'>User with this email not Exits. </strong>");
                        Response.Write("<span class='block sm:inline'>Please Regsiter Yourself.</span>");
                        Response.Write("</div>");
                        return;
                    }
                }
            }
        }
    }
}