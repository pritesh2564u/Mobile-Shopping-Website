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
using System.IO;

namespace MobileApplication
{
    public partial class AddMobile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

            string email = Session["Email"] as string;

            if(!string.IsNullOrEmpty(email))
            {
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
        protected void btnAddMobile_Click(object sender, EventArgs e)
        {
            string email = Session["Email"] as string;
            if(!string.IsNullOrEmpty(email))
            {
                string brand = BrandName.Value;
                string modelName = ModelName.Value;
                decimal price = Convert.ToDecimal(MobilePrice.Value);
                string mobileImage = "";
                string mobileRAM = MobileRAM.Value;
                string mobileStorage = MobileStorage.Value;
                string mobileDescription = MobileDescription.Value;
                int mobileQuantity = Convert.ToInt32(MobileQuantity.Value);

                if (MobileImageUpload.HasFile)
                {
                    string fileName = Path.GetFileName(MobileImageUpload.PostedFile.FileName);

                    string uploadDirectory = Server.MapPath("~/images/");

                    string filePath = Path.Combine(uploadDirectory, fileName);

                    MobileImageUpload.SaveAs(filePath);

                    mobileImage = filePath;
                }

                string connectionString = WebConfigurationManager.ConnectionStrings["MobileApplication"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Mobile (Brand, ModelName, Price, ImageUrl, RAM, Storage, Description, Quantity) VALUES (@brand, @modelName, @price, @mobileImage, @mobileRAM, @mobileStorage, @mobileDescription, @mobileQuantity)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Brand", brand); // Use consistent parameter names
                        command.Parameters.AddWithValue("@modelName", modelName);
                        command.Parameters.AddWithValue("@Price", price); // Use consistent parameter names
                        command.Parameters.AddWithValue("@mobileImage", mobileImage);
                        command.Parameters.AddWithValue("@mobileRAM", mobileRAM);
                        command.Parameters.AddWithValue("@mobileStorage", mobileStorage);
                        command.Parameters.AddWithValue("@mobileDescription", mobileDescription);
                        command.Parameters.AddWithValue("@mobileQuantity", mobileQuantity);

                        try
                        {
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Response.Redirect("AdminPage.aspx");
                            }
                            else
                            {
                                Response.Write("Failed to add mobile.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Write("An error occurred: " + ex.Message);
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