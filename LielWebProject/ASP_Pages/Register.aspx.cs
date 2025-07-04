using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LielWebProject.ASP_Pages
{
    public partial class Register : System.Web.UI.Page
    {
        public string message;
        public int s;

        string fileName = General.FileName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                s = Insert();
                if (s > 0)
                {
                    message = "User registered successfully!";
                }
                else
                {
                    message = "Error: User registration failed. Please try again.";
                }
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
            }

        }

        private int Insert()
        {
            int success = -1;
            string username = Request.Form["username"];
            string fullName = Request.Form["fullName"];
            string password = Request.Form["password"];
            string email = Request.Form["email"];

            if (username != null && fullName != null && password != null && email != null)
            {
                string sql = $"INSERT INTO {General.TableName} (username, fullName, password, email) VALUES ('{username}', '{fullName}', '{password}', '{email}')";
                Helper.DoQuery(fileName, sql);
                success = 1;
            }
            return success;

        }
    }
}