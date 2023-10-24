<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="MobileApplication.Signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Up</title>
    <script src="https://cdn.tailwindcss.com"></script>
</head>
<body style="background-image: url('images/bg1.jpg'); background-repeat: no-repeat;">
    <form id="form1" runat="server">
        <div class="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 bg-white shadow-lg p-4 border rounded-md">
            <h2 class="text-gray-800 font-bold text-2xl text-center">Register</h2>
            <div class="text-base mt-3">
                <label for="SignupUsername" class="text-gray-700 font-semibold">Username</label>
                <input id="SignupUsername" runat="server" type="text" placeholder="Username" class="border rounded-sm outline-none p-2 w-full focus:ring focus:ring-blue-400 mt-2" required/>
            </div>
            <div class="text-base mt-3">
                <label for="SignupEmail" class="text-gray-700 font-semibold">Email</label>
                <input id="SignupEmail" runat="server" type="text" placeholder="Email" class="border rounded-sm outline-none p-2 w-full focus:ring focus:ring-blue-400 mt-2" required/>
            </div>
            <div class="text-base mt-3">
                <label for="SignupPassword" class="text-gray-700 font-semibold">Password</label>
                <input id="SignupPassword" runat="server" type="password" placeholder="Password" class="border rounded-sm outline-none p-2 w-full focus:ring focus:ring-blue-400 mt-2" required/>
            </div>
            <div class="text-base mt-3">
                <label for="SignupConfirmPassword" class="text-gray-700 font-semibold">Confirm Password</label>
                <input id="SignupConfirmPassword" runat="server" type="password" placeholder="Password" class="border rounded-sm outline-none p-2 w-full focus:ring focus:ring-blue-400 mt-2" required />
            </div>
            <asp:Button ID="btnSignup_click" runat="server" Text="Signup" CssClass="w-full bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded mt-4 cursor-pointer" OnClick="btnSignup_Click"></asp:Button>
            <div class="text-center mt-4">
                <p class="text-gray-700">Already have an account? <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Login.aspx" CssClass="text-blue-500 hover:underline">Login</asp:HyperLink></p>
            </div>
        </div>
    </form>
    <script>
        function closePasswordMismatchAlert() {
            document.getElementById('passwordMismatchAlert').style.display = 'none';
        }
    </script>
</body>
</html>
