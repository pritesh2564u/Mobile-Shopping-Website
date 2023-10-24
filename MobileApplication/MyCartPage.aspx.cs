using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileApplication
{
    public partial class MyCartPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string email = Session["email"] as string;

            if (!string.IsNullOrEmpty(email))
            {
                DataTable cartItems = FetchCartItemsFromDatabase(email);

                foreach (DataRow row in cartItems.Rows)
                {
                    var brandLabel = new Label();
                    brandLabel.CssClass = "text-lg font-bold cart-item-brand text-gray-800";
                    brandLabel.Text = "Brand: " + row["Brand"].ToString();

                    var modelLabel = new Label();
                    modelLabel.CssClass = "text-base cart-item-model text-gray-600";
                    modelLabel.Text = "Model: " + row["ModelName"].ToString();

                    var ramLabel = new Label();
                    ramLabel.CssClass = "text-base cart-item-ram text-gray-600";
                    ramLabel.Text = "RAM: " + row["RAM"].ToString();

                    var storageLabel = new Label();
                    storageLabel.CssClass = "text-base cart-item-storage text-gray-600";
                    storageLabel.Text = "Storage: " + row["Storage"].ToString();

                    var priceLabel = new Label();
                    priceLabel.CssClass = "text-lg font-bold cart-item-price text-green-600";
                    priceLabel.Text = "Price: ₹" + row["Price"].ToString();

                    var removeButton = new Button();
                    removeButton.CssClass = "bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded-full mt-3 cursor-pointer remove-button";
                    removeButton.Text = "Remove from Cart";
                    removeButton.Click += RemoveButton_Click;
                    removeButton.CommandArgument = row["CartId"].ToString();

                    var cartItemImg = new Image();
                    cartItemImg.CssClass = "w-32 h-32 cart-item-image";
                    cartItemImg.ImageUrl = "images/" + Path.GetFileName(row["ImageUrl"].ToString());

                    var cartItem = new Panel();
                    cartItem.CssClass = "p-4 border border-gray-300 rounded-lg shadow-md flex flex-col cart-item";
                    cartItem.Controls.Add(cartItemImg);
                    cartItem.Controls.Add(brandLabel);
                    cartItem.Controls.Add(modelLabel);
                    cartItem.Controls.Add(ramLabel);
                    cartItem.Controls.Add(storageLabel);
                    cartItem.Controls.Add(priceLabel);
                    cartItem.Controls.Add(removeButton);

                    CartItemsContainer.Controls.Add(cartItem);
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected DataTable FetchCartItemsFromDatabase(string email)
        {
            string connString = WebConfigurationManager.ConnectionStrings["MobileApplication"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                string query = @"
                    SELECT *
                    FROM CustomerCart
                    WHERE Email = @Email
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
            }
        }

        protected void RemoveButton_Click(object sender, EventArgs e)
        {
            Button removeButton = (Button)sender;
            string CartId = removeButton.CommandArgument;

            RemoveCartItem(CartId);

            Response.Redirect(Request.RawUrl);
        }

        protected void RemoveCartItem(string CartId)
        {
            string email = Session["email"] as string;
            if (!string.IsNullOrEmpty(email))
            {
                string connString = WebConfigurationManager.ConnectionStrings["MobileApplication"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();

                    string query = @"
                DELETE FROM CustomerCart
                WHERE Email = @Email AND CartId = @CartId
            ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@CartId", CartId);

                        command.ExecuteNonQuery();
                    }
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
    }
}