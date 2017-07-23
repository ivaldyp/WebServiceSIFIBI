using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Globalization;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

//PENJELASAN AWAL
//flag 0 pada database = Belum terverifikasi
//flag 1 pada database = Sudah terverifikasi
//

public class WebService : System.Web.Services.WebService
{
    string[] results;
    int angka, angka2, angka3 = 0;
    string result, result2, result3 = null;
    string hasil, hasil2, hasil3 = null;
    int nilaiRow, nilaiCol;
    SqlConnection con = new SqlConnection();

    public WebService()
    {

    }

    //memeriksa apa email (ketika login) sudah ada didalam database
    public int checkEmail(string email_users)
    {
        int flag = 0;

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CheckEmail", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("email_users", SqlDbType.VarChar, 100).Value = email_users;

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                //kalau email tidak ditemukan
                if (ds.Tables[0].Select("email_users is not null").Length < 1)
                    return flag;
                //kalau email ditemukan
                else
                    return flag = 1;
            }
        }
    }

    [WebMethod]
    public string Login(string email_users, string password_users)
    {
        string login = "";
        int email_flag = 0;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Login", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("email_users", SqlDbType.VarChar, 100).Value = email_users;
                email_flag = checkEmail(email_users);
                if(email_flag == 0)
                {
                    return "Username Salah";
                }

                cmd.Parameters.Add("password_users", SqlDbType.VarChar, 100).Value = password_users;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                if (ds.Tables[0].Select("id_users is not null").Length < 1)
                    return "Password Salah";
                else
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0][2]) == 0)
                        return "Akun Anda Belum Terverifikasi";
                    else
                        login = ds.Tables[0].Rows[0][0].ToString() + "|" + ds.Tables[0].Rows[0][1].ToString() + "|" + ds.Tables[0].Rows[0][3].ToString();
                }       

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return login;
    }

    //----------------------------------------------------------------------
    //---------------------------C O U N T----------------------------------
    //----------------------------------------------------------------------

    //Menghitung laporan hasil sidang dengan flag 0, 
    //flag 1, dan menghitung total laporan
    //yang pernah dibuat user tersebut
    [WebMethod]
    public int CountHasilSidangByID0(int id_users)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CountHasilSidangByID0", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_users", SqlDbType.Int).Value = id_users;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
        }
    }

    [WebMethod]
    public int CountHasilSidangByID1(int id_users)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CountHasilSidangByID1", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_users", SqlDbType.Int).Value = id_users;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
        }
    }

    [WebMethod]
    public int CountHasilSidangByIDAll(int id_users)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CountHasilSidangByIDAll", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_users", SqlDbType.Int).Value = id_users;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
        }
    }

    //Menghitung laporan hasil sidang dengan flag 0, 
    //flag 1, dan menghitung total laporan
    //yang pernah dibuat berdasarkan satker user tersebut
    [WebMethod]
    public int CountHasilSidangByID0PJ(string nama_satker)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CountHasilSidangByID0PJ", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("nama_satker", SqlDbType.VarChar, 100).Value = nama_satker;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
        }
    }

    [WebMethod]
    public int CountHasilSidangByID1PJ(string nama_satker)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CountHasilSidangByID1PJ", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("nama_satker", SqlDbType.VarChar, 100).Value = nama_satker;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
        }
    }

    [WebMethod]
    public int CountHasilSidangByIDAllPJ(string nama_satker)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CountHasilSidangByIDAllPJ", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("nama_satker", SqlDbType.VarChar, 100).Value = nama_satker;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
        }
    }

    //----------------------------------------------------------------------

    //menghitung total kehadiran yang pernah dibuat berdasarkan user
    //dihitung berdasarkan id user / satker user
    [WebMethod]
    public int CountKehadiranByIDAll(int id_users)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CountKehadiranByIDAll", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_users", SqlDbType.Int).Value = id_users;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
        }
    }

    [WebMethod]
    public int CountKehadiranByIDAllPJ(string nama_satker)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CountKehadiranByIDAllPJ", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("nama_satker", SqlDbType.VarChar, 100).Value = nama_satker;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
        }
    }

    //----------------------------------------------------------------------

    //menghitung total penyelenggaraan yang pernah dibuat berdasarkan user
    //dihitung berdasarkan id user / satker user
    [WebMethod]
    public int CountPenyelenggaraanByIDAll(int id_users)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CountPenyelenggaraanByIDAll", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_users", SqlDbType.Int).Value = id_users;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
        }
    }

    [WebMethod]
    public int CountPenyelenggaraanByIDAllPJ(string nama_satker)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CountPenyelenggaraanByIDAllPJ", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("nama_satker", SqlDbType.VarChar, 100).Value = nama_satker;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
        }
    }

    //----------------------------------------------------------------------

    //Menghitung total user dengan flag 0, 
    //flag 1, dan menghitung total user
    [WebMethod]
    public int CountUsersByID0()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CountUsersByID0", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
        }
    }

    [WebMethod]
    public int CountUsersByID1()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CountUsersByID1", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
        }
    }

    [WebMethod]
    public int CountUsersByIDAll()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CountUsersByIDAll", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
        }
    }

    //----------------------------------------------------------------------
    //----------------------------H O M E-----------------------------------
    //----------------------------------------------------------------------

    //Fungsi menampilkan data-data untuk
    //tampilan home milik admin, PIC
    //dan penanggung jawab
    [WebMethod]
    public string HomeAdmin(int id_users)
    {
        string temp, home = null;
        temp = SelectUsersByID(id_users);

        char delimiter = '|';
        string[] words = temp.Split(delimiter);
        home += words[1] + "|";
        home += words[2] + "|";
        home += words[3] + "|";
        if (Convert.ToInt32(words[5]) == 1)
            home += "Admin|";
        else
            return "User tersebut bukan Admin";

        home += CountUsersByIDAll() + "|";
        home += CountHasilSidangByID0(id_users);
        return home;
    }

    [WebMethod]
    public string HomePIC(int id_users)
    {
        string temp, home = null;
        temp = SelectUsersByID(id_users);

        char delimiter = '|';
        string[] words = temp.Split(delimiter);
        home += words[1] + "|";
        home += words[2] + "|";
        home += words[3] + "|";
        if (Convert.ToInt32(words[5]) == 2)
            home += "PIC|";
        else
            return "User tersebut bukan PIC";

       
        home += CountKehadiranByIDAll(id_users) + "|";
        home += CountPenyelenggaraanByIDAll(id_users) + "|";
        home += CountHasilSidangByID0(id_users);

        return home;
    }

    [WebMethod]
    public string HomePJ(int id_users)
    {
        string temp, home = null;
        temp = SelectUsersByID(id_users);

        char delimiter = '|';
        string[] words = temp.Split(delimiter);
        string nama_satker = words[2];
        home += words[1] + "|";
        home += words[2] + "|";
        home += words[3] + "|";
        if (Convert.ToInt32(words[5]) == 3)
            home += "Penanggung Jawab|";
        else
            return "User tersebut bukan Penanggung Jawab";

        home += CountKehadiranByIDAllPJ(nama_satker) + "|";
        home += CountPenyelenggaraanByIDAllPJ(nama_satker) + "|";
        home += CountHasilSidangByID0PJ(nama_satker);
        return home;
    }

    //----------------------------------------------------------------------
    //--------------------------S E A R C H---------------------------------
    //----------------------------------------------------------------------

    //Fungsi mencari data untuk laporan hasil sidang,
    //rencana kehadiran, rencana penyelenggaraan
    //dan data user dari dalam database
    [WebMethod]
    public string SearchSidang(string input1, string input2)
    {
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            if (input2 == "Klik di sini")
                input2 = "";

            if (input1 == "" && input2 == "")
                return "Field Pencarian Kosong";
            else if (input1 == null || input1 == "")
            {
                using (SqlCommand cmd = new SqlCommand("SearchSidangByDate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("input2", SqlDbType.VarChar, 100).Value = input2;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);

                    nilaiRow = ds.Tables[0].Select("id_sidang is not null").Length;
                    nilaiCol = ds.Tables[0].Columns.Count;

                    if (nilaiRow < 1)
                    {
                        return "Data Kosong";
                    }

                    for (int i = 0; i < nilaiRow; i++)
                    {
                        for (int j = 0; j < nilaiCol; j++)
                        {
                            if (j == nilaiCol - 2)
                            {
                                result2 = ds.Tables[0].Rows[i][j].ToString();
                                if (result2 == null || result2 == "" || result2 == "-")
                                {
                                    waktu_event2 = "NULL";
                                }
                                else
                                {
                                    waktu_event2 = result2;
                                }

                                result += waktu_event2;
                                result += "|";
                            }
                            else if (j == nilaiCol - 1)
                            {
                                result += ds.Tables[0].Rows[i][j].ToString();
                                if (i != nilaiRow - 1)
                                {
                                    result += "~";
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                result += ds.Tables[0].Rows[i][j].ToString();
                                result += "|";
                            }
                        }
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            else if (input2 == null || input2 == "")
            {
                using (SqlCommand cmd = new SqlCommand("SearchSidangByText", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("input1", SqlDbType.VarChar, 100).Value = input1;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);

                    nilaiRow = ds.Tables[0].Select("id_sidang is not null").Length;
                    nilaiCol = ds.Tables[0].Columns.Count;

                    if (nilaiRow < 1)
                    {
                        return "Data Kosong";
                    }

                    for (int i = 0; i < nilaiRow; i++)
                    {
                        for (int j = 0; j < nilaiCol; j++)
                        {
                            if (j == nilaiCol - 2)
                            {
                                result2 = ds.Tables[0].Rows[i][j].ToString();
                                if (result2 == null || result2 == "")
                                {
                                    waktu_event2 = "NULL";
                                }
                                else
                                {
                                    waktu_event2 = result2;
                                }

                                result += waktu_event2;
                                result += "|";
                            }
                            else if (j == nilaiCol - 1)
                            {
                                result += ds.Tables[0].Rows[i][j].ToString();
                                if (i != nilaiRow - 1)
                                {
                                    result += "~";
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                result += ds.Tables[0].Rows[i][j].ToString();
                                result += "|";
                            }
                        }
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            else
            {
                using (SqlCommand cmd = new SqlCommand("SearchSidangByBoth", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("input1", SqlDbType.VarChar, 100).Value = input1;
                    cmd.Parameters.Add("input2", SqlDbType.VarChar, 100).Value = input2;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);

                    nilaiRow = ds.Tables[0].Select("id_sidang is not null").Length;
                    nilaiCol = ds.Tables[0].Columns.Count;

                    if (nilaiRow < 1)
                    {
                        return "Data Kosong";
                    }

                    for (int i = 0; i < nilaiRow; i++)
                    {
                        for (int j = 0; j < nilaiCol; j++)
                        {
                            if (j == nilaiCol - 2)
                            {
                                result2 = ds.Tables[0].Rows[i][j].ToString();
                                if (result2 == null || result2 == "")
                                {
                                    waktu_event2 = "NULL";
                                }
                                else
                                {
                                    waktu_event2 = result2;
                                }

                                result += waktu_event2;
                                result += "|";
                            }
                            else if (j == nilaiCol - 1)
                            {
                                result += ds.Tables[0].Rows[i][j].ToString();
                                if (i != nilaiRow - 1)
                                {
                                    result += "~";
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                result += ds.Tables[0].Rows[i][j].ToString();
                                result += "|";
                            }
                        }
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }
        return result;
    }

    [WebMethod]
    public string SearchKehadiran(string input1, string input2)
    {
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            if (input2 == "Klik di sini")
                input2 = "";

            if (input1 == "" && input2 == "")
                return "Field Pencarian Kosong";
            else if (input1 == null || input1 == "")
            {
                using (SqlCommand cmd = new SqlCommand("SearchKehadiranByDate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("input2", SqlDbType.VarChar, 100).Value = input2;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);

                    nilaiRow = ds.Tables[0].Select("id_kehadiran is not null").Length;
                    nilaiCol = ds.Tables[0].Columns.Count;
                    if (nilaiRow < 1)
                    {
                        return "Data Kosong";
                    }

                    for (int i = 0; i < nilaiRow; i++)
                    {
                        for (int j = 0; j < nilaiCol; j++)
                        {
                            if (j == nilaiCol - 1)
                            {
                                result2 = ds.Tables[0].Rows[i][j].ToString();
                                if (result2 == null || result2 == "" || result2 == "-")
                                {
                                    waktu_event2 = "NULL";
                                }
                                else
                                {
                                    waktu_event2 = result2;
                                }

                                result += waktu_event2;
                                if (i != nilaiRow - 1)
                                {
                                    result += "~";
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                result += ds.Tables[0].Rows[i][j].ToString();
                                result += "|";
                            }
                        }
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            else if (input2 == null || input2 == "")
            {
                using (SqlCommand cmd = new SqlCommand("SearchKehadiranByText", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("input1", SqlDbType.VarChar, 100).Value = input1;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);

                    nilaiRow = ds.Tables[0].Select("id_kehadiran is not null").Length;
                    nilaiCol = ds.Tables[0].Columns.Count;
                    if (nilaiRow < 1)
                    {
                        return "Data Kosong";
                    }

                    for (int i = 0; i < nilaiRow; i++)
                    {
                        for (int j = 0; j < nilaiCol; j++)
                        {
                            if (j == nilaiCol - 1)
                            {
                                result2 = ds.Tables[0].Rows[i][j].ToString();
                                if (result2 == null || result2 == "")
                                {
                                    waktu_event2 = "NULL";
                                }
                                else
                                {
                                    waktu_event2 = result2;
                                }

                                result += waktu_event2;
                                if (i != nilaiRow - 1)
                                {
                                    result += "~";
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                result += ds.Tables[0].Rows[i][j].ToString();
                                result += "|";
                            }
                        }
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            else
            {
                using (SqlCommand cmd = new SqlCommand("SearchKehadiranByBoth", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("input1", SqlDbType.VarChar, 100).Value = input1;
                    cmd.Parameters.Add("input2", SqlDbType.VarChar, 100).Value = input2;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);

                    nilaiRow = ds.Tables[0].Select("id_kehadiran is not null").Length;
                    nilaiCol = ds.Tables[0].Columns.Count;
                    if (nilaiRow < 1)
                    {
                        return "Data Kosong";
                    }

                    for (int i = 0; i < nilaiRow; i++)
                    {
                        for (int j = 0; j < nilaiCol; j++)
                        {
                            if (j == nilaiCol - 1)
                            {
                                result2 = ds.Tables[0].Rows[i][j].ToString();
                                if (result2 == null || result2 == "")
                                {
                                    waktu_event2 = "NULL";
                                }
                                else
                                {
                                    waktu_event2 = result2;
                                }

                                result += waktu_event2;
                                if (i != nilaiRow - 1)
                                {
                                    result += "~";
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                result += ds.Tables[0].Rows[i][j].ToString();
                                result += "|";
                            }
                        }
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }
        return result;
    }

    [WebMethod]
    public string SearchPenyelenggaraan(string input1, string input2)
    {
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            if (input2 == "Klik di sini")
                input2 = "";

            if (input1 == "" && input2 == "")
                return "Field Pencarian Kosong";
            else if (input1 == null || input1 == "")
            {
                using (SqlCommand cmd = new SqlCommand("SearchPenyelenggaraanByDate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("input2", SqlDbType.VarChar, 100).Value = input2;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);

                    nilaiRow = ds.Tables[0].Select("id_penyelenggaraan is not null").Length;
                    nilaiCol = ds.Tables[0].Columns.Count;
                    if (nilaiRow < 1)
                    {
                        return "Data Kosong";
                    }

                    for (int i = 0; i < nilaiRow; i++)
                    {
                        for (int j = 0; j < nilaiCol; j++)
                        {
                            if (j == nilaiCol - 1)
                            {
                                result2 = ds.Tables[0].Rows[i][j].ToString();
                                if (result2 == null || result2 == "" || result2 == "-")
                                {
                                    waktu_event2 = "NULL";
                                }
                                else
                                {
                                    waktu_event2 = result2;
                                }

                                result += waktu_event2;
                                if (i != nilaiRow - 1)
                                {
                                    result += "~";
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                result += ds.Tables[0].Rows[i][j].ToString();
                                result += "|";
                            }
                        }
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            else if (input2 == null || input2 == "")
            {
                using (SqlCommand cmd = new SqlCommand("SearchPenyelenggaraanByText", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("input1", SqlDbType.VarChar, 100).Value = input1;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);

                    nilaiRow = ds.Tables[0].Select("id_penyelenggaraan is not null").Length;
                    nilaiCol = ds.Tables[0].Columns.Count;
                    if (nilaiRow < 1)
                    {
                        return "Data Kosong";
                    }

                    for (int i = 0; i < nilaiRow; i++)
                    {
                        for (int j = 0; j < nilaiCol; j++)
                        {
                            if (j == nilaiCol - 1)
                            {
                                result2 = ds.Tables[0].Rows[i][j].ToString();
                                if (result2 == null || result2 == "")
                                {
                                    waktu_event2 = "NULL";
                                }
                                else
                                {
                                    waktu_event2 = result2;
                                }

                                result += waktu_event2;
                                if (i != nilaiRow - 1)
                                {
                                    result += "~";
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                result += ds.Tables[0].Rows[i][j].ToString();
                                result += "|";
                            }
                        }
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            else
            {
                using (SqlCommand cmd = new SqlCommand("SearchPenyelenggaraanByBoth", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("input1", SqlDbType.VarChar, 100).Value = input1;
                    cmd.Parameters.Add("input2", SqlDbType.VarChar, 100).Value = input2;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);

                    nilaiRow = ds.Tables[0].Select("id_penyelenggaraan is not null").Length;
                    nilaiCol = ds.Tables[0].Columns.Count;
                    if (nilaiRow < 1)
                    {
                        return "Data Kosong";
                    }

                    for (int i = 0; i < nilaiRow; i++)
                    {
                        for (int j = 0; j < nilaiCol; j++)
                        {
                            if (j == nilaiCol - 1)
                            {
                                result2 = ds.Tables[0].Rows[i][j].ToString();
                                if (result2 == null || result2 == "")
                                {
                                    waktu_event2 = "NULL";
                                }
                                else
                                {
                                    waktu_event2 = result2;
                                }

                                result += waktu_event2;
                                if (i != nilaiRow - 1)
                                {
                                    result += "~";
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                result += ds.Tables[0].Rows[i][j].ToString();
                                result += "|";
                            }
                        }
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }
        return result;
    }

    [WebMethod]
    public string SearchUsers(string input, string input2)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        if (input2 == "Admin")
            angka3 = 1;
        else if (input2 == "PIC")
            angka3 = 2;
        else if (input2 == "Penanggung Jawab")
            angka3 = 3;
        else if (input2 == "Kosong")
            input2 = "";

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            if (input == "" && input2 == "")
                return "Field Pencarian Kosong";
            else if (input == null || input == "")
            {
                using (SqlCommand cmd = new SqlCommand("SearchUsersByJabatan", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("input2", SqlDbType.VarChar, 100).Value = angka3;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);

                    nilaiRow = ds.Tables[0].Select("id_users is not null").Length;
                    nilaiCol = ds.Tables[0].Columns.Count;
                    if (nilaiRow < 1)
                    {
                        return "Data Kosong";
                    }

                    for (int i = 0; i < nilaiRow; i++)
                    {
                        result += ds.Tables[0].Rows[i][0].ToString() + "|";
                        result += ds.Tables[0].Rows[i][1].ToString() + "|";
                        result += ds.Tables[0].Rows[i][2].ToString();
                        if (i != nilaiRow - 1)
                        {
                            result += "~";
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }                
            }
            else if (input2 == null || input2 == "")
            {
                using (SqlCommand cmd = new SqlCommand("SearchUsersByText", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("input", SqlDbType.VarChar, 100).Value = input;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);

                    nilaiRow = ds.Tables[0].Select("id_users is not null").Length;
                    nilaiCol = ds.Tables[0].Columns.Count;
                    if (nilaiRow < 1)
                    {
                        return "Data Kosong";
                    }

                    for (int i = 0; i < nilaiRow; i++)
                    {
                        result += ds.Tables[0].Rows[i][0].ToString() + "|";
                        result += ds.Tables[0].Rows[i][1].ToString() + "|";
                        result += ds.Tables[0].Rows[i][2].ToString();
                        if (i != nilaiRow - 1)
                        {
                            result += "~";
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            else
            {
                using (SqlCommand cmd = new SqlCommand("SearchUsersByBoth", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("input", SqlDbType.VarChar, 100).Value = input;
                    cmd.Parameters.Add("input2", SqlDbType.VarChar, 100).Value = angka3;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);

                    nilaiRow = ds.Tables[0].Select("id_users is not null").Length;
                    nilaiCol = ds.Tables[0].Columns.Count;
                    if (nilaiRow < 1)
                    {
                        return "Data Kosong";
                    }

                    for (int i = 0; i < nilaiRow; i++)
                    {
                        result += ds.Tables[0].Rows[i][0].ToString() + "|";
                        result += ds.Tables[0].Rows[i][1].ToString() + "|";
                        result += ds.Tables[0].Rows[i][2].ToString();
                        if (i != nilaiRow - 1)
                        {
                            result += "~";
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }
        return result;
    }

    //----------------------------------------------------------------------
    //--------------------------S E L E C T A L L---------------------------
    //----------------------------------------------------------------------

    //Memanggil semua data laporan hasil sidang dari dalam database
    [WebMethod]
    public string SelectAllHasilSidang()
    {
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectAllHasilSidang", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_sidang is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 4 || j == nilaiCol - 21)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();

                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }

                            result += waktu_event2;
                            result += "|";
                        }
                        else if (j == nilaiCol - 1)
                        {
                            /*Kolom terakhirnya kolom nama users
                              makanya gausah dimunculin*/
                            //result += ds.Tables[0].Rows[i][j].ToString();
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if (j == nilaiCol - 2)
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        //return ds.Tables[0].Rows[0][3].ToString();
        //return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
        return result;
    }

    //memanggil semua ID, nama, dan tanggal hasil sidang dengan flag 1
    [WebMethod]
    public string SelectAllIDNamaTanggalHasilSidang()
    {
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectAllIDNamaTanggalHasilSidang", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_sidang is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 2)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();

                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }

                            result += waktu_event2;
                            result += "|";
                        }
                        else if (j == nilaiCol - 1)
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    //memanggil semua ID, nama, dan tanggal hasil sidang
    [WebMethod]
    public string SelectAllIDNamaTanggalHasilSidangAdmin()
    {
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectAllIDNamaTanggalHasilSidangAdmin", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_sidang is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 2)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();

                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }

                            result += waktu_event2;
                            result += "|";
                        }
                        else if (j == nilaiCol - 1)
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    //----------------------------------------------------------------------

    //Memanggil semua data rencana kehadiran dari dalam database
    [WebMethod]
    public string SelectAllRencanaKehadiran()
    {
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectAllRencanaKehadiran", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_kehadiran is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 5)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();
                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }
                            result += waktu_event2;
                            result += "|";

                            //if (result2 != "")
                            //{
                            //    char delimiterchars = ' ';
                            //    string[] words = result2.split(delimiterchars);

                            //    waktu_event2 = null;
                            //    char delimiterchar = '/';

                            //    string[] words2 = words[0].split(delimiterchar);
                            //    waktu_event2 += words2[1] + "/";
                            //    waktu_event2 += words2[0] + "/";
                            //    waktu_event2 += words2[2];
                            //}
                            //else
                            //    waktu_event2 = "";

                            //result += waktu_event2;
                            //result += "|";
                        }
                        else if (j == nilaiCol - 1)
                        {
                            /*Kolom terakhirnya kolom nama users
                              makanya gausah dimunculin*/
                            //result += ds.Tables[0].Rows[i][j].ToString();
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
        //return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
    }

    //memanggil semua ID, nama, dan tanggal rencana kehadiran
    [WebMethod]
    public string SelectAllIDNamaTanggalKehadiran()
    {
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectAllIDNamaTanggalKehadiran", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_kehadiran is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 1)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();
                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }

                            result += waktu_event2;
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    //----------------------------------------------------------------------

    //Memanggil semua data rencana penyelenggaraan dari dalam database
    [WebMethod]
    public string SelectAllRencanaPenyelenggaraan()
    {
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectAllRencanaPenyelenggaraan", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_penyelenggaraan is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 5)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();
                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }

                            result += waktu_event2;
                            result += "|";
                        }
                        else if (j == nilaiCol - 1)
                        {
                            /*Kolom terakhirnya kolom nama users
                              makanya gausah dimunculin*/
                            //result += ds.Tables[0].Rows[i][j].ToString();
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
        //return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
    }

    //memanggil semua ID, nama, dan tanggal rencana penyelenggaraan
    [WebMethod]
    public string SelectAllIDNamaTanggalPenyelenggaraan()
    {
        string waktu_event2 = null;

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectAllIDNamaTanggalPenyelenggaraan", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_penyelenggaraan is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 1)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();
                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }

                            result += waktu_event2;
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    //----------------------------------------------------------------------

    //Memanggil semua data pengguna dari dalam database
    [WebMethod]
    public string SelectAllUsers()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectAllUsers", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_users is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 1)
                        {
                            /*Kolom terakhirnya kolom nama users
                              makanya gausah dimunculin*/
                            result += ds.Tables[0].Rows[i][j].ToString();
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    //----------------------------------------------------------------------
    //--------------------------S E L E C T F I X---------------------------
    //----------------------------------------------------------------------

    //Fungsi yang memanggil semua data laporan hasil sidan
    //dan semua pengguna yang memiliki flag 1
    [WebMethod]
    public string SelectFixHasilSidang()
    {
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectFixHasilSidang", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_sidang is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 4 || j == nilaiCol - 21)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();

                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }

                            result += waktu_event2;
                            result += "|";
                        }
                        else if (j == nilaiCol - 2)
                        {
                            /*Kolom terakhirnya kolom nama users
                              makanya gausah dimunculin*/
                            //result += ds.Tables[0].Rows[i][j].ToString();
                            if (i != nilaiRow - 1)
                            {
                                result += ds.Tables[0].Rows[i][j].ToString();
                                result += "~";
                            }
                            else
                            {
                                result += ds.Tables[0].Rows[i][j].ToString();
                                continue;
                            }
                        }
                        else if (j == nilaiCol - 1)
                            continue;
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
        //return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
    }
    
    [WebMethod]
    public string SelectFixUsers()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectFixUsers", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_users is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 1)
                        {
                            /*Kolom terakhirnya kolom nama users
                              makanya gausah dimunculin*/
                            result += ds.Tables[0].Rows[i][j].ToString();
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if (j == 0 || j == 1 || j == 2)
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
        //return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
    }

    //----------------------------------------------------------------------
    //------------------S E L E C T K A L E N D E R-------------------------
    //----------------------------------------------------------------------

    //Memanggil id dan tanggal semua laporan hasil sidang
    [WebMethod]
    public string SelectKalenderHasilSidang()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectKalenderHasilSidang", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_sidang is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 1)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();
                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                result += "NULL";
                            }
                            else
                            {
                                char delimiterChar = '/';
                                string[] words2 = result2.Split(delimiterChar);

                                DateTime enddate = new DateTime(Convert.ToInt32(words2[2]), Convert.ToInt32(words2[1]), Convert.ToInt32(words2[0]));
                                DateTime startdate = new DateTime(1970, 1, 1);

                                double milliseconds = (enddate - startdate).TotalMilliseconds;
                                result += milliseconds.ToString();
                            }

                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += "ev" + (i + 1).ToString() + "|";
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    //Memanggil id, nama acara, tempat,
    //tanggal, nama satker, dan flag dari laporan hasil sidang
    //berdasarkan tanggal yang dipilih
    [WebMethod]
    public string SelectKalender2HasilSidang(string tanggal)
    {
        string caseSwitch, waktu_sidang = null;
        //string tanggal = "Fri Jan 01 00:00:00 GMT+07:00 1900";
        char delimiter = ' ';
        string[] words = tanggal.Split(delimiter);
        int monthInDigit = DateTime.ParseExact(words[1], "MMM", CultureInfo.InvariantCulture).Month;

        caseSwitch = words[2];
        switch (caseSwitch)
        {
            case "01":
                words[2] = "1";
                break;
            case "02":
                words[2] = "2";
                break;
            case "03":
                words[2] = "3";
                break;
            case "04":
                words[2] = "4";
                break;
            case "05":
                words[2] = "5";
                break;
            case "06":
                words[2] = "6";
                break;
            case "07":
                words[2] = "7";
                break;
            case "08":
                words[2] = "8";
                break;
            case "09":
                words[2] = "9";
                break;
            default:
                break;
        }

        waktu_sidang += words[2] + "/" + monthInDigit.ToString() + "/" + words[5];

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectKalender2HasilSidang", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("waktu_sidang", SqlDbType.VarChar, 255).Value = waktu_sidang;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_sidang is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        //if (j == nilaiCol - 2)
                        //{
                        //    result2 = ds.Tables[0].Rows[i][j].ToString();
                        //    char delimiterChars = ' ';
                        //    words = result2.Split(delimiterChars);

                        //    string waktu_event2 = null;
                        //    char delimiterChar = '/';

                        //    string[] words2 = words[0].Split(delimiterChar);
                        //    waktu_event2 += words2[1] + "/";
                        //    waktu_event2 += words2[0] + "/";
                        //    waktu_event2 += words2[2];

                        //    result += waktu_event2;
                        //    result += "|";
                        //}
                        if (j == nilaiCol - 1)
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    //----------------------------------------------------------------------

    [WebMethod]
    public string SelectKalenderKehadiran()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectKalenderKehadiran", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_kehadiran is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 1)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();
                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                result += "NULL";
                            }
                            else
                            {
                                char delimiterChar = '/';
                                string[] words2 = result2.Split(delimiterChar);

                                DateTime enddate = new DateTime(Convert.ToInt32(words2[2]), Convert.ToInt32(words2[1]), Convert.ToInt32(words2[0]));
                                DateTime startdate = new DateTime(1970, 1, 1);

                                double milliseconds = (enddate - startdate).TotalMilliseconds;
                                result += milliseconds.ToString();
                            }
                           
                            
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += "ev" + (i + 1).ToString() + "|";
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    [WebMethod]
    public string SelectKalender2Kehadiran(string tanggal)
    {
        string caseSwitch = null;
        //string tanggal = "Fri Jan 01 00:00:00 GMT+07:00 1900";
        char delimiter = ' ';
        string[] words = tanggal.Split(delimiter);
        int monthInDigit = DateTime.ParseExact(words[1], "MMM", CultureInfo.InvariantCulture).Month;

        caseSwitch = words[2];
        switch (caseSwitch)
        {
            case "01":
                words[2] = "1";
                break;
            case "02":
                words[2] = "2";
                break;
            case "03":
                words[2] = "3";
                break;
            case "04":
                words[2] = "4";
                break;
            case "05":
                words[2] = "5";
                break;
            case "06":
                words[2] = "6";
                break;
            case "07":
                words[2] = "7";
                break;
            case "08":
                words[2] = "8";
                break;
            case "09":
                words[2] = "9";
                break;
            default:
                break;
        }

        hasil3 += words[2] + "/" + monthInDigit.ToString() + "/" + words[5];
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectKalender2Kehadiran", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("waktu_event", SqlDbType.VarChar, 255).Value = hasil3;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_kehadiran is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 1)
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    //----------------------------------------------------------------------

    [WebMethod]
    public string SelectKalenderPenyelenggaraan()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectKalenderPenyelenggaraan", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_penyelenggaraan is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 1)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();
                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                result += "NULL";
                            }
                            else
                            {
                                char delimiterChar = '/';
                                string[] words2 = result2.Split(delimiterChar);

                                DateTime enddate = new DateTime(Convert.ToInt32(words2[2]), Convert.ToInt32(words2[1]), Convert.ToInt32(words2[0]));
                                DateTime startdate = new DateTime(1970, 1, 1);

                                double milliseconds = (enddate - startdate).TotalMilliseconds;
                                result += milliseconds.ToString();
                            }
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += "ev" + (i + 1).ToString() + "|";
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    [WebMethod]
    public string SelectKalender2Penyelenggaraan(string tanggal)
    {
        string caseSwitch = null;
        //string tanggal = "Fri Jan 01 00:00:00 GMT+07:00 1900";
        char delimiter = ' ';
        string[] words = tanggal.Split(delimiter);
        int monthInDigit = DateTime.ParseExact(words[1], "MMM", CultureInfo.InvariantCulture).Month;

        caseSwitch = words[2];
        switch (caseSwitch)
        {
            case "01":
                words[2] = "1";
                break;
            case "02":
                words[2] = "2";
                break;
            case "03":
                words[2] = "3";
                break;
            case "04":
                words[2] = "4";
                break;
            case "05":
                words[2] = "5";
                break;
            case "06":
                words[2] = "6";
                break;
            case "07":
                words[2] = "7";
                break;
            case "08":
                words[2] = "8";
                break;
            case "09":
                words[2] = "9";
                break;
            default:
                break;
        }

        hasil3 += words[2] + "/" + monthInDigit.ToString() + "/" + words[5];

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectKalender2Penyelenggaraan", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("waktu_event", SqlDbType.VarChar, 255).Value = hasil3;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_penyelenggaraan is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 1)
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    //----------------------------------------------------------------------
    //--------------------------S E L E C T---------------------------------
    //----------------------------------------------------------------------

    [WebMethod]
    public string SelectIDNamaTanggalHasilSidang(int id_users)
    {
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectIDNamaTanggalHasilSidang", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_users", SqlDbType.Int).Value = id_users;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_sidang is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;

                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 2)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();
                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }

                            result += waktu_event2;
                            result += "|";
                        }
                        else if (j == nilaiCol - 1)
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    [WebMethod]
    public string SelectIDNamaTanggalHasilSidangPJ(int id_users)
    {
        string waktu_event2 = null;
        string temp, nama_satker = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectIDNamaTanggalHasilSidangPJ", con))
            {
                temp = SelectUsersByID(id_users);
                char delimiter = '|';
                string[] huruf = temp.Split(delimiter);
                nama_satker += huruf[2];

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("nama_satker", SqlDbType.VarChar, 100).Value = nama_satker;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_sidang is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;

                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 2)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();
                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }

                            result += waktu_event2;
                            result += "|";
                        }
                        else if (j == nilaiCol - 1)
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    [WebMethod]
    public string SelectHasilSidangByID(int id_sidang)
    {
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectHasilSidangByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_sidang", SqlDbType.Int).Value = id_sidang;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_sidang is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 6 || j == nilaiCol - 23)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();

                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }

                            result += waktu_event2;
                            result += "|";
                        }
                        else if (j == nilaiCol - 4)
                        {
                            result += ds.Tables[0].Rows[i][j + 2].ToString() + "|" + ds.Tables[0].Rows[i][j + 1].ToString() + "|" + ds.Tables[0].Rows[i][j].ToString();
                            break;
                            //result += "~";
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    [WebMethod]
    public string SelectHasilSidang()
    {
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectHasilSidang", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_sidang is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 4 || j == nilaiCol - 21)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();
                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }
                            result += waktu_event2;
                            result += "|";
                        }
                        else if (j == nilaiCol - 1)
                        {
                            /*Kolom terakhirnya kolom nama users
                              makanya gausah dimunculin*/
                            //result += ds.Tables[0].Rows[i][j].ToString();
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
        //return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
    }

    //----------------------------------------------------------------------

    [WebMethod]
    public string SelectIDNamaTanggalKehadiran(int id_users)
    {
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectIDNamaTanggalKehadiran", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_users", SqlDbType.Int).Value = id_users;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_kehadiran is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 1)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();
                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }

                            result += waktu_event2;
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    [WebMethod]
    public string SelectIDNamaTanggalKehadiranPJ(int id_users)
    {
        string waktu_event2, temp, nama_satker = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectIDNamaTanggalKehadiranPJ", con))
            {
                temp = SelectUsersByID(id_users);
                char delimiter = '|';
                string[] huruf = temp.Split(delimiter);
                nama_satker += huruf[2];

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("nama_satker", SqlDbType.VarChar, 100).Value = nama_satker;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_kehadiran is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 1)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();
                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }

                            result += waktu_event2;
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    [WebMethod]
    public string SelectKehadiranByID(int id_kehadiran)
    {
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectKehadiranByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_kehadiran", SqlDbType.Int).Value = id_kehadiran;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_kehadiran is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 7)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();

                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }
                            result += waktu_event2;
                            result += "|";
                        }
                        else if (j == nilaiCol - 4)
                        {
                            result += ds.Tables[0].Rows[i][j+2].ToString() + "|" + ds.Tables[0].Rows[i][j+1].ToString() + "|" + ds.Tables[0].Rows[i][j].ToString();
                            break;
                            //result += "~";
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    [WebMethod]
    public string SelectRencanaKehadiran()
    {
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectRencanaKehadiran", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_kehadiran is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 5)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();
                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }
                            result += waktu_event2;
                            result += "|";
                        }
                        else if (j == nilaiCol - 1)
                        {
                            /*Kolom terakhirnya kolom nama users
                              makanya gausah dimunculin*/
                            //result += ds.Tables[0].Rows[i][j].ToString();
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
        //return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
    }

    //----------------------------------------------------------------------

    [WebMethod]
    public string SelectIDNamaTanggalPenyelenggaraan(int id_users)
    {
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectIDNamaTanggalPenyelenggaraan", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_users", SqlDbType.Int).Value = id_users;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_penyelenggaraan is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 1)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();
                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }

                            result += waktu_event2;
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    [WebMethod]
    public string SelectIDNamaTanggalPenyelenggaraanPJ(int id_users)
    {
        string temp, nama_satker = null;
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectIDNamaTanggalPenyelenggaraanPJ", con))
            {
                temp = SelectUsersByID(id_users);
                char delimiter = '|';
                string[] huruf = temp.Split(delimiter);
                nama_satker += huruf[2];

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("nama_satker", SqlDbType.VarChar, 100).Value = nama_satker;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_penyelenggaraan is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 1)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();
                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }

                            result += waktu_event2;
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    [WebMethod]
    public string SelectPenyelenggaraanByID(int id_penyelenggaraan)
    {
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectPenyelenggaraanByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_penyelenggaraan", SqlDbType.Int).Value = id_penyelenggaraan;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_penyelenggaraan is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 7)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();

                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }

                            result += waktu_event2;
                            result += "|";
                        }
                        else if (j == nilaiCol - 4)
                        {
                            result += ds.Tables[0].Rows[i][j + 2].ToString() + "|" + ds.Tables[0].Rows[i][j + 1].ToString() + "|" + ds.Tables[0].Rows[i][j].ToString();
                            break;                            
                            //result += "~";
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    [WebMethod]
    public string SelectRencanaPenyelenggaraan()
    {
        string waktu_event2 = null;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectRencanaPenyelenggaraan", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_penyelenggaraan is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 5)
                        {
                            result2 = ds.Tables[0].Rows[i][j].ToString();

                            if (result2 == null || result2 == "" || result2 == "-")
                            {
                                waktu_event2 = "NULL";
                            }
                            else
                            {
                                waktu_event2 = result2;
                            }

                            result += waktu_event2;
                            result += "|";
                        }
                        else if (j == nilaiCol - 1)
                        {
                            /*Kolom terakhirnya kolom nama users
                              makanya gausah dimunculin*/
                            //result += ds.Tables[0].Rows[i][j].ToString();
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
        //return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
    }

    //----------------------------------------------------------------------

    [WebMethod]
    public string SelectIDNamaSatkerUsers(string email_users)
    {
        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectIDNamaSatkerUsers", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("email_users", SqlDbType.VarChar, 100).Value = email_users;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_users is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 1)
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                        }
                        else
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
    }

    [WebMethod]
    public string SelectUsers()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectUsers", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_users is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        if (j == nilaiCol - 1)
                        {
                            /*Kolom terakhirnya kolom nama users
                              makanya gausah dimunculin*/
                            result += ds.Tables[0].Rows[i][j].ToString();
                            if (i != nilaiRow - 1)
                            {
                                result += "~";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if(j == 0 || j == 1 || j == 2 )
                        {
                            result += ds.Tables[0].Rows[i][j].ToString();
                            result += "|";
                        }
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return result;
        //return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
    }

    [WebMethod]
    public string SelectUsersByID(int id_users)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectUsersbyID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_users", SqlDbType.Int).Value = id_users;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                nilaiRow = ds.Tables[0].Select("id_users is not null").Length;
                nilaiCol = ds.Tables[0].Columns.Count;
                if (nilaiRow < 1)
                {
                    return "Data Kosong";
                }

                for (int i = 0; i < nilaiRow; i++)
                {
                    for (int j = 0; j < nilaiCol; j++)
                    {
                        hasil += ds.Tables[0].Rows[i][j].ToString();
                        if (j != nilaiCol - 1)
                        {
                            hasil += "|";
                        }
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return hasil;
        //return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
    }

    //----------------------------------------------------------------------
    //-----------------------------I N S E R T------------------------------
    //----------------------------------------------------------------------

    [WebMethod]
    public int InsertHasilSidang(string nama_satker, string tempat_sidang, string waktu_sidang, string nama_fora, string nama_working_group, string delegasi_bi, string delegasi_terkait, string negara_mitra, string agenda_pembahasan, string relevansi, string stance_bi, string stance_posisi_terkait, string stance_negara_mitra, string kesepakatan_telah, string kesepakatan_akan, string pending_issues, string rencana_tidak_lanjut, string fora_lain, string satker_lain, string jadwal_sidang_next, string lembaga_lain, int id_users)
    {
        int insRecord = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("InsertHasilSidang", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (waktu_sidang == "Klik di sini")
                    waktu_sidang = "-";
                if (jadwal_sidang_next == "Klik di sini")
                    jadwal_sidang_next = "-";

                cmd.Parameters.Add("nama_satker", SqlDbType.VarChar, 100).Value = nama_satker;
                cmd.Parameters.Add("tempat_sidang", SqlDbType.VarChar, 100).Value = tempat_sidang;
                cmd.Parameters.Add("waktu_sidang", SqlDbType.VarChar, 100).Value = waktu_sidang;
                cmd.Parameters.Add("nama_fora", SqlDbType.VarChar, 100).Value = nama_fora;
                cmd.Parameters.Add("nama_working_group", SqlDbType.VarChar, 100).Value = nama_working_group;
                cmd.Parameters.Add("delegasi_bi", SqlDbType.VarChar, 100).Value = delegasi_bi;
                cmd.Parameters.Add("delegasi_terkait", SqlDbType.VarChar, 100).Value = delegasi_terkait;
                cmd.Parameters.Add("negara_mitra", SqlDbType.VarChar, 100).Value = negara_mitra;
                cmd.Parameters.Add("agenda_pembahasan", SqlDbType.VarChar, 100).Value = agenda_pembahasan;
                cmd.Parameters.Add("relevansi", SqlDbType.VarChar, 100).Value = relevansi;
                cmd.Parameters.Add("stance_bi", SqlDbType.VarChar, 100).Value = stance_bi;
                cmd.Parameters.Add("stance_posisi_terkait", SqlDbType.VarChar, 100).Value = stance_posisi_terkait;
                cmd.Parameters.Add("stance_negara_mitra", SqlDbType.VarChar, 100).Value = stance_negara_mitra;
                cmd.Parameters.Add("kesepakatan_telah", SqlDbType.VarChar, 100).Value = kesepakatan_telah;
                cmd.Parameters.Add("kesepakatan_akan", SqlDbType.VarChar, 100).Value = kesepakatan_akan;
                cmd.Parameters.Add("pending_issues", SqlDbType.VarChar, 100).Value = pending_issues;
                cmd.Parameters.Add("rencana_tidak_lanjut", SqlDbType.VarChar, 100).Value = rencana_tidak_lanjut;
                cmd.Parameters.Add("fora_lain", SqlDbType.VarChar, 100).Value = fora_lain;
                cmd.Parameters.Add("satker_lain", SqlDbType.VarChar, 100).Value = satker_lain;

                cmd.Parameters.Add("jadwaL_sidang_next", SqlDbType.VarChar, 100).Value = jadwal_sidang_next;
                cmd.Parameters.Add("lembaga_lain", SqlDbType.VarChar, 100).Value = lembaga_lain;
                cmd.Parameters.Add("id_users", SqlDbType.Int).Value = id_users;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                insRecord = cmd.ExecuteNonQuery();
            }
        }
        return insRecord;
    }

    [WebMethod]
    public int InsertRencanaKehadiran(string periode_kehadiran, string nama_satker, string nama_event, string tempat_event, string waktu_event, string delegasi_adg, string delegasi_satker, string topik, int id_users)
    {
        int insRecord = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("InsertRencanaKehadiran", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (waktu_event == "Klik di sini")
                    waktu_event = "-";

                cmd.Parameters.Add("periode_kehadiran", SqlDbType.VarChar, 100).Value = periode_kehadiran;
                cmd.Parameters.Add("nama_satker", SqlDbType.VarChar, 100).Value = nama_satker;
                cmd.Parameters.Add("nama_event", SqlDbType.VarChar, 100).Value = nama_event;
                cmd.Parameters.Add("tempat_event", SqlDbType.VarChar, 100).Value = tempat_event;

                cmd.Parameters.Add("waktu_event", SqlDbType.VarChar, 100).Value = waktu_event;
                cmd.Parameters.Add("delegasi_adg", SqlDbType.VarChar, 100).Value = delegasi_adg;
                cmd.Parameters.Add("delegasi_satker", SqlDbType.VarChar, 100).Value = delegasi_satker;
                cmd.Parameters.Add("topik", SqlDbType.VarChar, 100).Value = topik;
                cmd.Parameters.Add("id_users", SqlDbType.Int).Value = id_users;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                insRecord = cmd.ExecuteNonQuery();
            }
        }
        return insRecord;
    }

    [WebMethod]
    public int InsertRencanaPenyelenggaraan(string periode_kehadiran, string nama_satker, string nama_event, string tempat_event, string waktu_event, string delegasi_adg, string delegasi_satker, string topik, int id_users)
    {
        int insRecord = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("InsertRencanaPenyelenggaraan", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (waktu_event == "Klik di sini")
                    waktu_event = "-";

                cmd.Parameters.Add("periode_kehadiran", SqlDbType.VarChar, 100).Value = periode_kehadiran;
                cmd.Parameters.Add("nama_satker", SqlDbType.VarChar, 100).Value = nama_satker;
                cmd.Parameters.Add("nama_event", SqlDbType.VarChar, 100).Value = nama_event;
                cmd.Parameters.Add("tempat_event", SqlDbType.VarChar, 100).Value = tempat_event;

                cmd.Parameters.Add("waktu_event", SqlDbType.VarChar, 100).Value = waktu_event;
                cmd.Parameters.Add("delegasi_adg", SqlDbType.VarChar, 100).Value = delegasi_adg;
                cmd.Parameters.Add("delegasi_satker", SqlDbType.VarChar, 100).Value = delegasi_satker;
                cmd.Parameters.Add("topik", SqlDbType.VarChar, 100).Value = topik;
                cmd.Parameters.Add("id_users", SqlDbType.Int).Value = id_users;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                insRecord = cmd.ExecuteNonQuery();
            }
        }
        return insRecord;
    }

    [WebMethod]
    public string InsertUsers(string nama_users, string satker_users, string email_users, string password_users, int jabatan_users)
    {
        int email_flag = 0;
        int insRecord = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("InsertUsers", con))
            {
                email_flag = checkEmail(email_users);
                if (email_flag == 1)
                {
                    return "Email sudah pernah terdaftar";
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("nama_users", SqlDbType.VarChar, 100).Value = nama_users;
                cmd.Parameters.Add("satker_users", SqlDbType.VarChar, 100).Value = satker_users;
                cmd.Parameters.Add("email_users", SqlDbType.VarChar, 100).Value = email_users;
                cmd.Parameters.Add("password_users", SqlDbType.VarChar, 100).Value = password_users;
                cmd.Parameters.Add("jabatan_users", SqlDbType.Int, 100).Value = jabatan_users;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                insRecord = cmd.ExecuteNonQuery();
            }
        }
        if (insRecord == 1)
            return "Data menunggu konfirmasi dari admin";
        else
            return "Data error";
    }

    //----------------------------------------------------------------------
    //--------------------------U P D A T E F I X---------------------------
    //----------------------------------------------------------------------

    [WebMethod]
    public int UpdateFixHasilSidang(int id_sidang)
    {
        int Rowdelete = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateFixHasilSidang", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_sidang", SqlDbType.Int).Value = id_sidang;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                Rowdelete = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return Rowdelete;
    }

    [WebMethod]
    public int UpdateFixUsers(int id_users)
    {
        int Rowdelete = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateFixUsers", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_users", SqlDbType.Int).Value = id_users;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                Rowdelete = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return Rowdelete;
    }

    [WebMethod]
    public int UpdateNotFixUsers(int id_users)
    {
        int Rowdelete = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateNotFixUsers", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_users", SqlDbType.Int).Value = id_users;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                Rowdelete = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return Rowdelete;
    }

    //----------------------------------------------------------------------
    //-----------------------------U P D A T E------------------------------
    //----------------------------------------------------------------------

    [WebMethod]
    public int UpdateHasilSidang(int id_sidang, string nama_satker, string tempat_sidang, string waktu_sidang, string nama_fora, string nama_working_group, string delegasi_bi, string delegasi_terkait, string negara_mitra, string agenda_pembahasan, string relevansi, string stance_bi, string stance_posisi_terkait, string stance_negara_mitra, string kesepakatan_telah, string kesepakatan_akan, string pending_issues, string rencana_tidak_lanjut, string fora_lain, string satker_lain, string jadwal_sidang_next, string lembaga_lain)
    {
        int updRecord = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateHasilSidang", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (waktu_sidang == "Klik di sini")
                    waktu_sidang = "-";
                if (jadwal_sidang_next == "Klik di sini")
                    jadwal_sidang_next = "-";

                cmd.Parameters.Add("id_sidang", SqlDbType.Int, 100).Value = id_sidang;
                cmd.Parameters.Add("nama_satker", SqlDbType.VarChar, 100).Value = nama_satker;
                cmd.Parameters.Add("tempat_sidang", SqlDbType.VarChar, 100).Value = tempat_sidang;

                cmd.Parameters.Add("waktu_sidang", SqlDbType.VarChar, 100).Value = waktu_sidang;
                cmd.Parameters.Add("nama_fora", SqlDbType.VarChar, 100).Value = nama_fora;
                cmd.Parameters.Add("nama_working_group", SqlDbType.VarChar, 100).Value = nama_working_group;
                cmd.Parameters.Add("delegasi_bi", SqlDbType.VarChar, 100).Value = delegasi_bi;
                cmd.Parameters.Add("delegasi_terkait", SqlDbType.VarChar, 100).Value = delegasi_terkait;
                cmd.Parameters.Add("negara_mitra", SqlDbType.VarChar, 100).Value = negara_mitra;
                cmd.Parameters.Add("agenda_pembahasan", SqlDbType.VarChar, 100).Value = agenda_pembahasan;
                cmd.Parameters.Add("relevansi", SqlDbType.VarChar, 100).Value = relevansi;
                cmd.Parameters.Add("stance_bi", SqlDbType.VarChar, 100).Value = stance_bi;
                cmd.Parameters.Add("stance_posisi_terkait", SqlDbType.VarChar, 100).Value = stance_posisi_terkait;
                cmd.Parameters.Add("stance_negara_mitra", SqlDbType.VarChar, 100).Value = stance_negara_mitra;
                cmd.Parameters.Add("kesepakatan_telah", SqlDbType.VarChar, 100).Value = kesepakatan_telah;
                cmd.Parameters.Add("kesepakatan_akan", SqlDbType.VarChar, 100).Value = kesepakatan_akan;
                cmd.Parameters.Add("pending_issues", SqlDbType.VarChar, 100).Value = pending_issues;
                cmd.Parameters.Add("rencana_tidak_lanjut", SqlDbType.VarChar, 100).Value = rencana_tidak_lanjut;
                cmd.Parameters.Add("fora_lain", SqlDbType.VarChar, 100).Value = fora_lain;
                cmd.Parameters.Add("satker_lain", SqlDbType.VarChar, 100).Value = satker_lain;

                cmd.Parameters.Add("jadwaL_sidang_next", SqlDbType.VarChar, 100).Value = jadwal_sidang_next;
                cmd.Parameters.Add("lembaga_lain", SqlDbType.VarChar, 100).Value = lembaga_lain;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                updRecord = cmd.ExecuteNonQuery();
            }
        }
        return updRecord;
    }

    [WebMethod]
    public int UpdateRencanaKehadiran(int id_kehadiran, string periode_kehadiran, string nama_satker, string nama_event, string tempat_event, string waktu_event, string delegasi_adg, string delegasi_satker, string topik)
    {
        int insRecord = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateRencanaKehadiran", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (waktu_event == "Klik di sini")
                    waktu_event = "-";

                cmd.Parameters.Add("id_kehadiran", SqlDbType.Int, 100).Value = id_kehadiran;
                cmd.Parameters.Add("periode_kehadiran", SqlDbType.VarChar, 100).Value = periode_kehadiran;
                cmd.Parameters.Add("nama_satker", SqlDbType.VarChar, 100).Value = nama_satker;
                cmd.Parameters.Add("nama_event", SqlDbType.VarChar, 100).Value = nama_event;
                cmd.Parameters.Add("tempat_event", SqlDbType.VarChar, 100).Value = tempat_event;

                cmd.Parameters.Add("waktu_event", SqlDbType.VarChar, 100).Value = waktu_event;
                cmd.Parameters.Add("delegasi_adg", SqlDbType.VarChar, 100).Value = delegasi_adg;
                cmd.Parameters.Add("delegasi_satker", SqlDbType.VarChar, 100).Value = delegasi_satker;
                cmd.Parameters.Add("topik", SqlDbType.VarChar, 100).Value = topik;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                insRecord = cmd.ExecuteNonQuery();
            }
        }
        return insRecord;
    }

    [WebMethod]
    public int UpdateRencanaPenyelenggaraan(int id_penyelenggaraan, string periode_kehadiran, string nama_satker, string nama_event, string tempat_event, string waktu_event, string delegasi_adg, string delegasi_satker, string topik)
    {
        int insRecord = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateRencanaPenyelenggaraan", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (waktu_event == "Klik di sini")
                    waktu_event = "-";

                cmd.Parameters.Add("id_penyelenggaraan", SqlDbType.Int, 100).Value = id_penyelenggaraan;
                cmd.Parameters.Add("periode_kehadiran", SqlDbType.VarChar, 100).Value = periode_kehadiran;
                cmd.Parameters.Add("nama_satker", SqlDbType.VarChar, 100).Value = nama_satker;
                cmd.Parameters.Add("nama_event", SqlDbType.VarChar, 100).Value = nama_event;
                cmd.Parameters.Add("tempat_event", SqlDbType.VarChar, 100).Value = tempat_event;

                cmd.Parameters.Add("waktu_event", SqlDbType.VarChar, 100).Value = waktu_event;
                cmd.Parameters.Add("delegasi_adg", SqlDbType.VarChar, 100).Value = delegasi_adg;
                cmd.Parameters.Add("delegasi_satker", SqlDbType.VarChar, 100).Value = delegasi_satker;
                cmd.Parameters.Add("topik", SqlDbType.VarChar, 100).Value = topik;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                insRecord = cmd.ExecuteNonQuery();
            }
        }
        return insRecord;
    }

    [WebMethod]
    public int UpdateUsers(int id_users, string nama_users, string satker_users, string email_users, string password_users, string jabatan_users)
    {
        int updRecord = 0;
        int jabatan2;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateUsers", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (jabatan_users == "Admin")
                    jabatan2 = 1;
                else if (jabatan_users == "PIC")
                    jabatan2 = 2;
                else
                    jabatan2 = 3;

                cmd.Parameters.Add("id_users", SqlDbType.Int).Value = id_users;
                cmd.Parameters.Add("nama_users", SqlDbType.VarChar, 100).Value = nama_users;
                cmd.Parameters.Add("satker_users", SqlDbType.VarChar, 100).Value = satker_users;
                cmd.Parameters.Add("email_users", SqlDbType.VarChar, 100).Value = email_users;
                cmd.Parameters.Add("password_users", SqlDbType.VarChar, 100).Value = password_users;
                cmd.Parameters.Add("jabatan_users", SqlDbType.Int).Value = jabatan2;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                updRecord = cmd.ExecuteNonQuery();
            }
        }
        return updRecord;
    }

    //----------------------------------------------------------------------
    //-----------------------------D E L E T E------------------------------
    //----------------------------------------------------------------------

    [WebMethod]
    public int DeleteHasilSidang(int id_sidang)
    {
        int Rowdelete = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("DeleteHasilSidang", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_sidang", SqlDbType.Int).Value = id_sidang;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                Rowdelete = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return Rowdelete;
    }

    [WebMethod]
    public int DeleteRencanaKehadiran(int id_kehadiran)
    {
        int Rowdelete = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("DeleteRencanaKehadiran", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_kehadiran", SqlDbType.Int).Value = id_kehadiran;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                Rowdelete = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return Rowdelete;
    }

    [WebMethod]
    public int DeleteRencanaPenyelenggaraan(int id_penyelenggaraan)
    {
        int Rowdelete = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("DeleteRencanaPenyelenggaraan", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_penyelenggaraan", SqlDbType.Int).Value = id_penyelenggaraan;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                Rowdelete = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return Rowdelete;
    }

    [WebMethod]
    public int DeleteUsers(int id_users)
    {
        int Rowdelete = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("DeleteUsers", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_users", SqlDbType.Int).Value = id_users;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                Rowdelete = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }
        return Rowdelete;
    }
}
