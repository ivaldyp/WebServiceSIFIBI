using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        
        con.Open();

        SqlCommand cmd = new SqlCommand("Insert into users (nama_user, satker_user, email_user, password_user, jabatan_user) values ('"+TextBox1.Text+"', '"+TextBox2.Text+"', '"+TextBox3.Text+"', '"+TextBox4.Text+"', '"+TextBox5.Text+"')", con);
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["WebServiceConnection"].ToString();
        cmd.ExecuteNonQuery();
        con.Close();
        Label1.Text = "Values are inserted";
    }
}