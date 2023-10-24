<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="MobileApplication.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
    <script src="https://cdn.tailwindcss.com"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <style>
        #sidebar {
            display: none;
            margin-left: -240px;
            transition: all 0.4s ease;
        }
        #sidebar.active {
            display: block;
            margin-left: 0px;
            transition: all 0.4s ease;
        }
        .card-div:hover {
            transform: scale(1.01);
            transition: all 0.3s ease;
            cursor: pointer;
        }
    </style>
</head>
<body runat="server">
    <form id="form1" runat="server">
    <nav runat="server" class="fixed top-0 left-0 border rounded-sm bg-white shadow-md w-full h-16 flex items-center justify-center z-20 transition-all duration-300">
        <div class="relative items-center ml-5">
            <i id="toggleNavbar" class="fas fa-bars text-xl cursor-pointer"></i>
        </div>
        <div class="flex items-center border border-gray-800 text-gray-900 ml-auto mr-auto rounded-sm">
            <i class="fa fa-magnifying-glass ml-2 mr-1"></i>
            <asp:TextBox ID="txtSearch" runat="server" placeholder="Search..." CssClass="border-none outline-none"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="border-none outline-none p-2 bg-blue-600 text-white hover:bg-blue-700 cursor-pointer" OnClick="btnSearch_Click" />
        </div>

        <div class="mr-5">
            <asp:HyperLink ID="HyperLink1" runat="server" Text="My Cart" CssClass="flex bg-green-500 text-white p-2 border rounded-sm shadow-lg cursor-pointer hover:bg-green-600" NavigateUrl="MyCartPage.aspx">
                <i class="fa-solid fa-cart-shopping mt-1 mr-2"></i>
                <p>My Cart</p>
            </asp:HyperLink>
        </div>
        <div class="mr-5">
            <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="border border-blue-500 text-blue-500 hover:bg-blue-700 hover:text-white p-2 rounded cursor-pointer" OnClick="btnLogout_Click" />
        </div>
    </nav>
    <aside>
        <nav id="sidebar" class="fixed top-0 left-0 h-full w-60 bg-white shadow-md z-10 transition-all duration-400">
            <div class="w-full mx-auto text-center hover:bg-gray-200" style="margin-top: 66px;">
                <asp:HyperLink ID="HyperLink2" runat="server" Text="My Cart" CssClass="flex items-center px-4 py-2 text-gray-700 hover:text-gray-900" NavigateUrl="HomePage.aspx">
                    <i class="fas fa-home mr-2"></i>
                    Home
                </asp:HyperLink>
            </div>
            <div class="absolute bottom-4 w-full mx-auto p-2 text-center">
                <asp:HyperLink ID="EditProfile" runat="server" Text="My Cart" CssClass="flex items-center justify-center bg-blue-500 text-white mx-auto mb-3 p-2 border rounded-md shadow-lg hover:bg-blue-700 cursor-pointer" NavigateUrl="EditUserProfile.aspx">
                    <i class="fa fa-user mt-1 mr-2"></i>
                    <p>Edit My Profile</p>
                </asp:HyperLink>
                <asp:Button ID="btnDeleteAccount" runat="server" Text="Delete Account" CssClass="w-full flex items-center justify-center bg-red-500 text-white mx-auto p-2 border rounded-md shadow-lg hover:bg-red-700 cursor-pointer" OnClick="btnDeleteAccount_Click">
                </asp:Button>
            </div>
        </nav>
    </aside>
    <asp:Panel ID="cardContainer" runat="server" CssClass="grid grid-cols-4 mt-16 p-4">
    </asp:Panel>
    </form>
    <script>
        const toggleNavbar = document.getElementById("toggleNavbar");
        const sidebar = document.getElementById("sidebar");
        let flag = 1;
        toggleNavbar.addEventListener("click", () => {
            if (flag == 1) {
                sidebar.classList.add("active");
                flag = !flag;
            }
            else {
                sidebar.classList.remove("active");
                flag = !flag;
            }
        });
    </script>
    </body>
</html>
