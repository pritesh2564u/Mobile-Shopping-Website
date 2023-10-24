<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUserProfile.aspx.cs" Inherits="MobileApplication.EditUserProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Profile</title>
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
<body>
    <form id="form1" runat="server">
        <div class="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 bg-white shadow-lg p-4 border rounded-md">
            <h2 class="text-gray-800 font-bold text-2xl text-center">Edit Profile</h2>
            <div class="text-base mt-3">
                <label for="EditUsername" class="text-gray-700 font-semibold">Username</label>
                <input id="EditUsername" runat="server" type="text" placeholder="Username" class="border rounded-sm outline-none p-2 w-full focus:ring focus:ring-blue-400 mt-2" required/>
            </div>
            <div class="text-base mt-3">
                <label for="EditEmail" class="text-gray-700 font-semibold">Email</label>
                <input id="EditEmail" runat="server" type="text" placeholder="Email" class="border rounded-sm outline-none p-2 w-full focus:ring focus:ring-blue-400 mt-2" required/>
            </div>
            <div class="text-base mt-3">
                <label for="EditPassword" class="text-gray-700 font-semibold">Password</label>
                <input id="EditPassword" runat="server" type="password" placeholder="Password" class="border rounded-sm outline-none p-2 w-full focus:ring focus:ring-blue-400 mt-2" required/>
            </div>
            <div class="text-base mt-3">
                <label for="EditConfirmPassword" class="text-gray-700 font-semibold">Confirm Password</label>
                <input id="EditConfirmPassword" runat="server" type="password" placeholder="Password" class="border rounded-sm outline-none p-2 w-full focus:ring focus:ring-blue-400 mt-2" required />
            </div>
            <asp:Button ID="btnEdit" runat="server" Text="Save Changes" CssClass="w-full bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded mt-4 cursor-pointer" OnClick="btnEdit_Click"></asp:Button>
        </div>
    </form>
</body>
</html>
