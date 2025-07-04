using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LielWebProject.ASP_Pages
{
    public partial class Play : System.Web.UI.Page
    {
        // Game state
        Player Player;
        Player Dealer;
        Deck Deck;
        bool IsPlayerTurn;
        double BetAmount;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string email = Session["email"]?.ToString();
                if (string.IsNullOrEmpty(email))
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                LoadPlayer(email);
                pnlBet.Visible = true;
                pnlGame.Visible = false;
            }
            else
            {
                Player = (Player)Session["Player"];
                Dealer = (Player)Session["Dealer"];
                Deck = (Deck)Session["Deck"];
                IsPlayerTurn = (bool)Session["IsPlayerTurn"];
                BetAmount = Convert.ToDouble(Session["BetAmount"] ?? 0);
            }
        }

        private void LoadPlayer(string email)
        {
            string sql = $"SELECT * FROM {General.TableName} WHERE Email = '{email}'";
            DataTable dt = Helper.ExecuteDataTable(General.FileName, sql);

            if (dt.Rows.Count > 0)
            {
                double chips = dt.Rows[0]["Chips"] != DBNull.Value ? Convert.ToDouble(dt.Rows[0]["Chips"]) : 2500;

                Player = new Player
                {
                    Email = email,
                    Chips = chips
                };

                Dealer = new Player();
                Deck = new Deck();
                Deck.Shuffle();

                Session["Player"] = Player;
                Session["Dealer"] = Dealer;
                Session["Deck"] = Deck;
                Session["IsPlayerTurn"] = true;
                Session["BetAmount"] = 0;

                lblChips.Text = $"Chips: {Player.Chips}";
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnPlaceBet_Click(object sender, EventArgs e)
        {
            double bet;
            if (!double.TryParse(txtBet.Text, out bet) || bet <= 0 || bet > Player.Chips)
            {
                lblError.Text = "Invalid bet.";
                return;
            }

            BetAmount = bet;
            Player.Bet = bet;
            Player.Chips -= bet;

            Session["BetAmount"] = BetAmount;
            Session["Player"] = Player;

            StartGame();
        }

        private void StartGame()
        {
            Player.Hand.Clear();
            Dealer.Hand.Clear();

            Player.Chips -= Player.Bet;

            Deck = new Deck();
            Deck.Shuffle();

            Player.Deal(Deck);
            Dealer.Deal(Deck);
            Player.Deal(Deck);
            Dealer.Deal(Deck, hidden: true);

            IsPlayerTurn = true;

            Session["IsPlayerTurn"] = true;
            Session["Deck"] = Deck;
            Session["Player"] = Player;
            Session["Dealer"] = Dealer;

            pnlBet.Visible = false;
            pnlGame.Visible = true;

            RefreshGameUI();

            if (Player.HandValue() == 21)
            {
                IsPlayerTurn = false;
                Session["IsPlayerTurn"] = false;
                EndTurn();
            }
        }


        protected void btnHit_Click(object sender, EventArgs e)
        {
            Player.Deal(Deck);
            Session["Player"] = Player;

            if (Player.HandValue() >= 21)
            {
                IsPlayerTurn = false;
                Session["IsPlayerTurn"] = false;
                EndTurn();
            }

            RefreshGameUI();
        }

        protected void btnStand_Click(object sender, EventArgs e)
        {
            IsPlayerTurn = false;
            Session["IsPlayerTurn"] = false;
            EndTurn();
        }

        protected void btnDouble_Click(object sender, EventArgs e)
        {
            if (!IsPlayerTurn || Player.Hand.Count != 2 || Player.Chips < Player.Bet)
                return;

            Player.Chips -= Player.Bet;
            Player.Bet *= 2;
            Player.Deal(Deck);

            IsPlayerTurn = false;
            Session["Player"] = Player;
            Session["IsPlayerTurn"] = false;

            EndTurn();
        }

        private void EndTurn()
        {
            Dealer.Hand[1].Hidden = false;

            while (Dealer.HandValue() < 17)
            {
                Dealer.Deal(Deck);
            }

            string result;
            if (Player.HandValue() > 21)
            {
                result = "You busted!";
            }
            else if (Dealer.HandValue() > 21 || Player.HandValue() > Dealer.HandValue())
            {
                result = "You win!";
                Player.Chips += Player.Bet * 2;
            }
            else if (Player.HandValue() == Dealer.HandValue())
            {
                result = "Push.";
                Player.Chips += Player.Bet;
            }
            else
            {
                result = "Dealer wins.";
            }

            UpdatePlayerChipsInDb(Player.Email, Player.Chips);

            lblResult.Text = result;
            Session["Player"] = Player;

            pnlPlayAgain.Visible = true;
            RefreshGameUI();
        }

        protected void btnPlayAgain_Click(object sender, EventArgs e)
        {
            StartGame();
            pnlPlayAgain.Visible = false;
        }

        private void RefreshGameUI()
        {
            lblPlayerHand.Text = $"Player: {Player.HandValue()}";
            lblDealerHand.Text = $"Dealer: {Dealer.HandValue()}";
            lblChips.Text = $"Chips: {Player.Chips}";
            lblBet.Text = $"Bet: {Player.Bet}";

            // Clear previous images
            phPlayerCards.Controls.Clear();
            phDealerCards.Controls.Clear();

            // Show Player's cards
            foreach (var card in Player.Hand)
            {
                Image img = new Image();
                img.ImageUrl = card.GetImagePath();
                img.Width = 60;  // optional: control size
                img.Height = 90; // optional
                img.Style.Add("margin-right", "5px");
                phPlayerCards.Controls.Add(img);
            }

            // Show Dealer's cards
            foreach (var card in Dealer.Hand)
            {
                Image img = new Image();
                img.ImageUrl = card.GetImagePath();
                img.Width = 60;
                img.Height = 90;
                img.Style.Add("margin-right", "5px");
                phDealerCards.Controls.Add(img);
            }
        }

        private void UpdatePlayerChipsInDb(string email, double chips)
        {
            string sql = $"UPDATE {General.TableName} SET Chips = {chips} WHERE Email = '{email}'";
            Helper.DoQuery(General.FileName, sql);
        }
    }
}
