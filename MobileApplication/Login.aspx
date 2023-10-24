<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MobileApplication.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <script src="https://cdn.tailwindcss.com"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css" integrity="sha512-z3gLpd7yknf1YoNbCzqRKc4qyor8gaKU1qmn+CShxbuBusANI9QpRohGBreCFkKxLhei6S9CQXFEbbKuqLg0DA==" crossorigin="anonymous" referrerpolicy="no-referrer" />

</head>
<body style="background-image: url('images/bg1.jpg'); background-repeat: no-repeat;">
    <form id="form1" runat="server">
        <div class="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 bg-white shadow-lg p-4 border rounded-md">
            <h2 class="text-gray-800 font-bold text-2xl text-center">Login</h2>
            <div class="text-base mt-3">
                <label for="LoginEmail" class="text-gray-700 font-semibold">Email</label>
                <input id="LoginEmail" runat="server" type="text" placeholder="Email" class="border rounded-sm outline-none p-2 w-full focus:ring focus:ring-blue-400 mt-2" />
            </div>
            <div class="text-base mt-3">
                <label for="LoginPassword" class="text-gray-700 font-semibold">Password</label>
                <input id="LoginPassword" runat="server" type="password" placeholder="Password" class="border rounded-sm outline-none p-2 w-full focus:ring focus:ring-blue-400 mt-2" />
            </div>
            <asp:Button runat="server" Text="Login" CssClass="w-full bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded mt-4 cursor-pointer" OnClick="btnLogin_Click" ID="btnLogin"></asp:Button>
            <div class="text-center mt-4">
                <p class="text-gray-700">Don't have an account? <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Signup.aspx" CssClass="text-blue-500 hover:underline">Sign Up</asp:HyperLink></p>
            </div>
        </div>
    </form>
    </body>
</html>
