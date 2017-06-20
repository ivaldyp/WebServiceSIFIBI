using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{
    string[] results;
    //int i = 0;
    string result;
    string first;
    int nilaiRow, nilaiCol;
    SqlConnection con = new SqlConnection();
     

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string Test()
    {
        return "Hola!";
    }

    [WebMethod]
    public string GetNameByID(int id_user)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectUsers", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("id_user", SqlDbType.Int).Value = id_user;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                nilaiRow = ds.Tables[0].Select("id_user is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;

                int x = 0;
                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        result += ds.Tables[0].Rows[i][j].ToString();
                        if (j == nilaiCol - 1)
                        {
                            result += "||";
                        }
                        else
                        {
                            result += ",";
                        }
                    }
                    x++;
                }
                //first = Convert.ToString(ds.Tables[0].Rows[0].ToString());
                //result = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    [WebMethod]
    public string GetData()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectAllUserss", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("id_user", SqlDbType.Int).Value = id_user;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                nilaiRow = ds.Tables[0].Select("id_user is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;

                int x = 0;
                for (int i=0; i<nilaiRow; i++)
                {
                    for (int j=0; j<nilaiCol; j++)
                    {
                        result += ds.Tables[0].Rows[i][j].ToString();
                        if (j == nilaiCol - 1)
                        {
                            result += "||";
                        }
                        else
                        {
                            result += ",";
                        }
                    }
                    x++;
                }
                //first = Convert.ToString(ds.Tables[0].Rows[0].ToString());
                //result = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    [WebMethod]
    public int InsertData (string nama_user, string satker_user, string email_user, string password_user, string jabatan_user)
    {
        int retRecord = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("InsertUsers", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("nama_user", SqlDbType.VarChar, 100).Value = nama_user;
                cmd.Parameters.Add("satker_user", SqlDbType.VarChar, 100).Value = satker_user;
                cmd.Parameters.Add("email_user", SqlDbType.VarChar, 100).Value = email_user;
                cmd.Parameters.Add("password_user", SqlDbType.VarChar, 100).Value = password_user;
                cmd.Parameters.Add("jabatan_user", SqlDbType.VarChar, 100).Value = jabatan_user;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                retRecord = cmd.ExecuteNonQuery();
            }

        }
        return retRecord;
    }

    [WebMethod]
    public int DeleteData(string id_user)
    {
        int Rowupdate = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("DeleteUsers", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_user", SqlDbType.Int).Value = id_user;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                Rowupdate = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }
        return Rowupdate;

        /*string queryDel = ("Delete from users where id_user = '" + id_user.ToString() + "'");
        var constr = System.Configuration.ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString;

        using (var con = new SqlConnection(constr))
        {
            using (var cmd = new SqlCommand(queryDel, con))
            {
                con.Open();
                var result = cmd.ExecuteScalar();
            }
        }

        return "Values Are Deleted!";*/
    }

    /*[WebMethod]
    public string SetData(string nama_user, string satker_user, string email_user, string password_user, string jabatan_user)
    {
        string queryIns = "Insert into users (nama_user, satker_user, email_user, password_user, jabatan_user) values ('" + nama_user.ToString() + "', '" + satker_user.ToString() + "', '" + email_user.ToString() + "', '" + password_user.ToString() + "', '" + jabatan_user.ToString() + "')";
        var constr = System.Configuration.ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString;
        using (var con = new SqlConnection(constr))
        {
            using (var cmd = new SqlCommand(queryIns, con))
            {
                con.Open();
                var result = cmd.ExecuteScalar();
            }
        }
        return "Values Are Inserted";
    }*/
}
