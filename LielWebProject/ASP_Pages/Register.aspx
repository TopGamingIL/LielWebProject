<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="LielWebProject.ASP_Pages.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <h1>Register Form</h1>
        <form action="Register.aspx" runat="server" method="post" class="myform">
            <input type="text" id="username" name="username" placeholder="Username" />
            <input type="text" id="fullName" name="fullName" placeholder="Full Name" />
            <input type="text" id="email" name="email" placeholder="Email" />
            <input type="password" id="password" name="password" placeholder="Password" />
            <input type="submit" id="submit" name="submit" value="register" />
        </form>
    </center>
</asp:Content>
