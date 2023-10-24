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
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string email = Session["Email"] as string;

            if(string.IsNullOrEmpty(email)) 
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                DataTable mobileTable = FetchMobileDataFromDatabase();

                foreach (DataRow row in mobileTable.Rows)
                {
                    string brand = row["Brand"].ToString();
                    string modelName = row["ModelName"].ToString();
                    string ram = row["RAM"].ToString();
                    string storage = row["Storage"].ToString();
                    decimal price = Convert.ToDecimal(row["Price"]);
                    string imageUrl = row["ImageUrl"].ToString();

                    imageUrl = "images/" + Path.GetFileName(imageUrl);

                    HtmlGenericControl cardDiv = new HtmlGenericControl("div");
                    cardDiv.Attributes["class"] = "border border-gray-300 rounded-lg bg-white shadow-xl p-4 m-4 gap-4 hover:border-gray-400 card-div";

                    HtmlGenericControl imageDiv = new HtmlGenericControl("img");
                    imageDiv.Attributes["src"] = imageUrl;
                    imageDiv.Attributes["alt"] = brand + " " + modelName;
                    imageDiv.Attributes["class"] = "w-50 h-50 mx-auto";

                    Label brandLabel = new Label();
                    brandLabel.Text = "Brand: " + brand;
                    brandLabel.CssClass = "text-lg font-semibold text-gray-800";

                    Label modelNameLabel = new Label();
                    modelNameLabel.Text = "Model Name: " + modelName;
                    modelNameLabel.CssClass = "text-base mt-3 text-gray-600";

                    Label ramLabel = new Label();
                    ramLabel.Text = "RAM: " + ram;
                    ramLabel.CssClass = "text-base mt-3 text-gray-600";

                    Label storageLabel = new Label();
                    storageLabel.Text = "Storage: " + storage;
                    storageLabel.CssClass = "text-base mt-3 text-gray-600";

                    Label priceLabel = new Label();
                    priceLabel.Text = "Price: ₹" + price.ToString("0.00");
                    priceLabel.CssClass = "text-lg mt-3 mb-4 font-semibold text-green-600";

                    HtmlAnchor viewDetailsLink = new HtmlAnchor();
                    viewDetailsLink.HRef = "MobileDetails.aspx?mobileId=" + row["mobileId"].ToString();
                    viewDetailsLink.InnerHtml = "View Details";
                    viewDetailsLink.Attributes["class"] = "w-full relative top-3 bottom-2 mt-4 px-4 py-2 bg-yellow-500 text-white shadow-md border rounded-md cursor-pointer";

                    cardDiv.Controls.Add(imageDiv);
                    cardDiv.Controls.Add(new LiteralControl("<br />"));
                    cardDiv.Controls.Add(brandLabel);
                    cardDiv.Controls.Add(new LiteralControl("<br />"));
                    cardDiv.Controls.Add(modelNameLabel);
                    cardDiv.Controls.Add(new LiteralControl("<br />"));
                    cardDiv.Controls.Add(ramLabel);
                    cardDiv.Controls.Add(new LiteralControl("<br />"));
                    cardDiv.Controls.Add(storageLabel);
                    cardDiv.Controls.Add(new LiteralControl("<br />"));
                    cardDiv.Controls.Add(priceLabel);
                    cardDiv.Controls.Add(new LiteralControl("<br />"));
                    cardDiv.Controls.Add(viewDetailsLink);
                    cardDiv.Attributes["class"] += " text-gray-800";
                    cardContainer.Controls.Add(cardDiv);
                }
            }
        }

        protected DataTable FetchMobileDataFromDatabase()
        {
            string conn = WebConfigurationManager.ConnectionStrings["MobileApplication"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                string query = "SELECT mobileId, Brand, ModelName, Price, ImageUrl, RAM, Storage FROM Mobile";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
            }
        }

        protected void EditProfile_Click(object sender, EventArgs e)
        {
            if(User.Identity.IsAuthenticated)
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

                using(SqlConnection connection = new SqlConnection(conn))
                {
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        try
                        {
                            connection.Open();

                            int rowAffected = command.ExecuteNonQuery();

                            if(rowAffected > 0)
                            {
                                Response.Redirect("Signup.aspx");
                            }

                            else
                            {
                                Response.Write("Please Enter Correct Details");
                            }
                        }

                        catch(Exception ex)
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim().ToLower();

            DataTable mobileTable = FetchMobileDataFromDatabase2(searchQuery);

            cardContainer.Controls.Clear();

            foreach (DataRow row in mobileTable.Rows)
            {
                string brand = row["Brand"].ToString();
                string modelName = row["ModelName"].ToString();
                string ram = row["RAM"].ToString();
                string storage = row["Storage"].ToString();
                decimal price = Convert.ToDecimal(row["Price"]);
                string imageUrl = row["ImageUrl"].ToString();

                imageUrl = "images/" + Path.GetFileName(imageUrl);

                HtmlGenericControl cardDiv = new HtmlGenericControl("div");
                cardDiv.Attributes["class"] = "border border-gray-300 rounded-lg bg-white shadow-xl p-4 m-4 gap-4 hover:border-gray-400 card-div";

                HtmlGenericControl imageDiv = new HtmlGenericControl("img");
                imageDiv.Attributes["src"] = imageUrl;
                imageDiv.Attributes["alt"] = brand + " " + modelName;
                imageDiv.Attributes["class"] = "w-50 h-50 mx-auto";

                Label brandLabel = new Label();
                brandLabel.Text = "Brand: " + brand;
                brandLabel.CssClass = "text-lg font-semibold text-gray-800";

                Label modelNameLabel = new Label();
                modelNameLabel.Text = "Model Name: " + modelName;
                modelNameLabel.CssClass = "text-base mt-3 text-gray-600";

                Label ramLabel = new Label();
                ramLabel.Text = "RAM: " + ram;
                ramLabel.CssClass = "text-base mt-3 text-gray-600";

                Label storageLabel = new Label();
                storageLabel.Text = "Storage: " + storage;
                storageLabel.CssClass = "text-base mt-3 text-gray-600";

                Label priceLabel = new Label();
                priceLabel.Text = "Price: $" + price.ToString("0.00");
                priceLabel.CssClass = "text-lg mt-3 font-semibold text-green-600";

                HtmlAnchor viewDetailsLink = new HtmlAnchor();
                viewDetailsLink.HRef = "MobileDetails.aspx?mobileId=" + row["mobileId"].ToString();
                viewDetailsLink.InnerHtml = "View Details";
                viewDetailsLink.Attributes["class"] = "w-full relative top-5 mt-4 px-4 py-2 bg-yellow-500 text-white shadow-md border rounded-md cursor-pointer";

                cardDiv.Controls.Add(imageDiv);
                cardDiv.Controls.Add(new LiteralControl("<br />"));
                cardDiv.Controls.Add(brandLabel);
                cardDiv.Controls.Add(new LiteralControl("<br />"));
                cardDiv.Controls.Add(modelNameLabel);
                cardDiv.Controls.Add(new LiteralControl("<br />"));
                cardDiv.Controls.Add(ramLabel);
                cardDiv.Controls.Add(new LiteralControl("<br />"));
                cardDiv.Controls.Add(storageLabel);
                cardDiv.Controls.Add(new LiteralControl("<br />"));
                cardDiv.Controls.Add(priceLabel);
                cardDiv.Controls.Add(new LiteralControl("<br />"));
                cardDiv.Controls.Add(viewDetailsLink);
                cardDiv.Attributes["class"] += " text-gray-800";

                cardContainer.Controls.Add(cardDiv);
            }
        }

        private DataTable FetchMobileDataFromDatabase2(string searchQuery)
        {
            string conn = WebConfigurationManager.ConnectionStrings["MobileApplication"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                string query = "SELECT * FROM Mobile WHERE LOWER(Brand) LIKE @SearchQuery";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%"); // Use '%' for wildcard matching

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
            }
        }


    }
}