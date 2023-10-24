using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace MobileApplication
{
    public partial class MobileDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string email = Session["Email"] as string;
            if(!string.IsNullOrEmpty(email))
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["mobileId"] != null)
                    {
                        int mobileId = Convert.ToInt32(Request.QueryString["mobileId"]);
                        DataTable mobileDetails = FetchMobileDetailsFromDatabase(mobileId);

                        if (mobileDetails.Rows.Count > 0)
                        {
                            DataRow row = mobileDetails.Rows[0];

                            string brand = row["Brand"].ToString();
                            string modelName = row["ModelName"].ToString();
                            string ram = row["RAM"].ToString();
                            string storage = row["Storage"].ToString();
                            decimal price = Convert.ToDecimal(row["Price"]);
                            string imageUrl = row["ImageUrl"].ToString();

                            imageUrl = "images/" + Path.GetFileName(imageUrl);

                            // Display the details on the page
                            brandLabel.Text = "Brand: " + brand;
                            modelNameLabel.Text = "Model Name: " + modelName;
                            ramLabel.Text = "RAM: " + ram;
                            storageLabel.Text = "Storage: " + storage;
                            priceLabel.Text = "Price: " + price.ToString("0.00");
                            mobileImage.ImageUrl = imageUrl;

                            descriptionLabel.Text = "Description: " + row["Description"].ToString();
                            quantityLabel.Text = "Quantity: " + row["Quantity"].ToString();


                        }
                        else
                        {
                            Response.Write("Error Occured, Please Try Again");
                        }
                    }
                    else
                    {
                        
                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected DataTable FetchMobileDetailsFromDatabase(int mobileId)
        {
            string conn = WebConfigurationManager.ConnectionStrings["MobileApplication"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                string query = "SELECT * FROM Mobile WHERE mobileId = @MobileId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MobileId", mobileId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
            }
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

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
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

        protected void addToCartButton_Click(object sender, EventArgs e)
        {
                 string connString = WebConfigurationManager.ConnectionStrings["MobileApplication"].ConnectionString;

                if (Request.QueryString["mobileId"] != null)
                {
                    int mobileId = Convert.ToInt32(Request.QueryString["mobileId"]);

                    if (IsMobileInStock(connString, mobileId))
                    {
                        if (InsertIntoCustomerCart(connString, mobileId))
                        {
                            DecrementMobileQuantity(connString, mobileId);

                            Response.Redirect("MyCartPage.aspx"); 
                        }
                        else
                        {
                            
                            Response.Write("Failed to add to cart. Please try again.");
                        }
                    }
                    else
                    {
                        Response.Write("This mobile is currently out of stock.");
                    }
                }
                else
                {
                    Response.Write("Cant able to buy");
                }
        }

        private bool IsMobileInStock(string connString, int mobileId)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                string query = "SELECT Quantity FROM Mobile WHERE mobileId = @MobileId AND Quantity > 0";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MobileId", mobileId);
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        int quantity = Convert.ToInt32(result);
                        return quantity > 0;
                    }
                    else
                    {
                        Response.Write("<p class='w-full absolute bottom-5 mx-auto text-center text-red-500 font-bold text-lg'>Product is not Available</p>");
                        return false; 
                    }
                }
            }
        }

        private bool InsertIntoCustomerCart(string connString, int mobileId)
        {
            // Assume you have a UserId for the currently logged-in user, replace with actual UserId retrieval logic
            string email = Session["Email"] as string;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                string query = "INSERT INTO CustomerCart (Email, Brand, ModelName, Price, ImageUrl, RAM, Storage) " +
                               "SELECT @Email, Brand, ModelName, Price, ImageUrl, RAM, Storage FROM Mobile WHERE mobileId = @MobileId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@MobileId", mobileId);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        private void DecrementMobileQuantity(string connString, int mobileId)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                string query = "UPDATE Mobile SET Quantity = Quantity - 1 WHERE mobileId = @MobileId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MobileId", mobileId);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}