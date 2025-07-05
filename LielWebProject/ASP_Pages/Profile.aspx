<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="LielWebProject.ASP_Pages.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="profile-container" style="max-width: 500px; margin: 30px auto; padding: 20px; border: 1px solid #ccc; border-radius: 12px;">
    <h2>My Profile</h2>
        <form runat="server">
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <asp:Label runat="server" Text="Username:" AssociatedControlID="txtUsername" />
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
            <asp:Label runat="server" Text="Email:" AssociatedControlID="txtEmail" />
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
            <asp:Label runat="server" Text="Password:" AssociatedControlID="txtPassword" />
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
            <br />
            <asp:Button ID="btnUpdate" runat="server" Text="Update Profile" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete Account" CssClass="btn btn-danger" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete your account?');" />
        </form>
</div>
</asp:Content>
