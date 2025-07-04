<%@ Page Title="Blackjack" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Play.aspx.cs" Inherits="LielWebProject.ASP_Pages.Play" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-container img {
            width: 80px;
            margin: 5px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>Blackjack</h2>

    <form runat="server">

        <asp:Label ID="lblChips" runat="server" Text=""></asp:Label>
        <br /><br />

        <!-- Bet Panel -->
        <asp:Panel ID="pnlBet" runat="server">
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label><br />
            Bet Amount:
            <asp:TextBox ID="txtBet" runat="server"></asp:TextBox>
            <asp:Button ID="btnPlaceBet" runat="server" Text="Place Bet & Start" OnClick="btnPlaceBet_Click" />
        </asp:Panel>

        <!-- Game Panel -->
        <asp:Panel ID="pnlGame" runat="server" Visible="false">

            <h3>Dealer</h3>
            <div class="card-container" id="dealerCards">
                <asp:PlaceHolder ID="phDealerCards" runat="server"></asp:PlaceHolder>
            </div>
            <asp:Label ID="lblDealerHand" runat="server" Text=""></asp:Label>
            <br /><br />

            <h3>Player</h3>
            <div class="card-container" id="playerCards">
                <asp:PlaceHolder ID="phPlayerCards" runat="server"></asp:PlaceHolder>
            </div>
            <asp:Label ID="lblPlayerHand" runat="server" Text=""></asp:Label>
            <br />

            <asp:Label ID="lblBet" runat="server" Text=""></asp:Label>
            <br /><br />

            <asp:Button ID="btnHit" runat="server" Text="Hit" OnClick="btnHit_Click" />
            <asp:Button ID="btnStand" runat="server" Text="Stand" OnClick="btnStand_Click" />
            <asp:Button ID="btnDouble" runat="server" Text="Double" OnClick="btnDouble_Click" />
            <br /><br />

            <asp:Label ID="lblResult" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
            <br /><br />

            <asp:Panel ID="pnlPlayAgain" runat="server" Visible="false">
                <asp:Button ID="btnPlayAgain" runat="server" Text="Play Again" OnClick="btnPlayAgain_Click" />
            </asp:Panel>
        </asp:Panel>
    </form>

</asp:Content>
