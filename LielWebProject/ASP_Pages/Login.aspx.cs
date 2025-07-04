using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LielWebProject.ASP_Pages
{
    public partial class Login : System.Web.UI.Page
    {
        public string msg;
        public int s;

        string fileName = General.FileName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)//הסבר נוסף יבוא
            {

                string email = Request.Form["email"].ToString();//לשנות
                string pass = Request.Form["password"].ToString();//לצורך זיהוי 

                if (email != null && pass != null)

                {
                    string loginsql = $"SELECT * FROM {General.TableName} WHERE Email = '{email}' AND Password = '{pass}'";
                    if (Helper.IsExist(fileName, loginsql))
                    {
                        DataTable table = Helper.ExecuteDataTable(fileName, loginsql);
                        int length = table.Rows.Count;
                        if (length == 0)
                        {
                            msg = "אין נרשמים";
                            Response.Redirect("LoginPage.aspx");
                        }
                        else
                        {
                            // msg = "welcome " + fname;
                            Session["UserName"] = table.Rows[0]["username"].ToString();//לדייק מול הבסיס נתונים 
                            Session["email"] = table.Rows[0]["Email"].ToString();
                            msg = "welcome " + Session["UserName"].ToString();
                            //  Response.Redirect("UpdateUser.aspx");
                        }
                    }
                }
            }
        }
    }
}