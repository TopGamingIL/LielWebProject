using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LielWebProject.ASP_Pages
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string email = Session["Email"] as string;
                if (string.IsNullOrEmpty(email))
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                LoadUserData(email);
            }
        }

        private void LoadUserData(string email)
        {
            string sql = $"SELECT * FROM {General.TableName} WHERE Email = '{email}'";
            DataTable dt = Helper.ExecuteDataTable(General.FileName, sql);

            if (dt.Rows.Count > 0)
            {
                txtUsername.Text = dt.Rows[0]["Username"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtPassword.Text = dt.Rows[0]["Password"].ToString();
            }
            else
            {
                lblMessage.Text = "User not found.";
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string oldEmail = Session["Email"] as string;
            if (string.IsNullOrEmpty(oldEmail)) return;

            string newUsername = txtUsername.Text.Trim();
            string newEmail = txtEmail.Text.Trim();
            string newPassword = txtPassword.Text.Trim();

            string sql;

            if (string.IsNullOrEmpty(newPassword))
            {
                // Don't update the password if the field is empty
                sql = $@"
            UPDATE {General.TableName}
            SET Username = '{newUsername}', Email = '{newEmail}'
            WHERE Email = '{oldEmail}'";
            }
            else
            {
                // Update including the password
                sql = $@"
            UPDATE {General.TableName}
            SET Username = '{newUsername}', Email = '{newEmail}', Password = '{newPassword}'
            WHERE Email = '{oldEmail}'";
            }

            Helper.DoQuery(General.FileName, sql);

            lblMessage.Text = "Profile updated successfully.";
            Session["Email"] = newEmail;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string email = Session["Email"] as string;
            if (string.IsNullOrEmpty(email)) return;

            string sql = $"DELETE FROM {General.TableName} WHERE Email = '{email}'";
            Helper.DoQuery(General.FileName, sql);

            Session.Clear();
            Response.Redirect("Register.aspx");
        }
    }
}