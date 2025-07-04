using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LielWebProject.ASP_Pages
{
    public partial class Leaderboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLeaderboard();
            }
        }

        private void LoadLeaderboard()
        {
            string fileName = General.FileName;
            string sql = $"SELECT * FROM {General.TableName}";

            DataTable dt = Helper.ExecuteDataTable(fileName, sql);

            GridView.DataSource = dt;
            GridView.DataBind();
        }
    }
}