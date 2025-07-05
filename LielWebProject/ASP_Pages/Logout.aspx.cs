using System;
using System.Web;
using System.Web.UI;

namespace LielWebProject.ASP_Pages
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear(); // Remove all session data
            Session.Abandon(); // End the session completely (optional but good practice)

            // Optional: You can also expire the session cookie manually if needed
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddDays(-1);
            }

            // Redirect to login or homepage
            Response.Redirect("Login.aspx");
        }
    }
}
