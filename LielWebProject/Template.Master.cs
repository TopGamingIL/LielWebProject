using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LielWebProject
{
    public partial class Template : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                lblUserName.InnerText = "Hello, " + Session["UserName"].ToString() + "!";
                lblUserName.Style["display"] = "block";
                home.Style["display"] = "block";
                play.Style["display"] = "block";
                login.Style["display"] = "none";
                register.Style["display"] = "none";
                leaderboard.Style["display"] = "block";
                logout.Style["display"] = "block";
            }
            else
            {
                lblUserName.InnerText = "Hello, Guest!";
                lblUserName.Style["display"] = "block";
                home.Style["display"] = "block";
                play.Style["display"] = "none";
                login.Style["display"] = "block";
                register.Style["display"] = "block";
                leaderboard.Style["display"] = "block";
                logout.Style["display"] = "none";
            }
        }
    }
}