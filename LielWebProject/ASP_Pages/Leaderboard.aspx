<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Leaderboard.aspx.cs" Inherits="LielWebProject.ASP_Pages.Leaderboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <form runat="server">
            <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered" />
        </form>
    </center>
</asp:Content>
