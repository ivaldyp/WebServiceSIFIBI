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
    int nilaiRow, nilaiCol;
    SqlConnection con = new SqlConnection();


    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "Simple Test.")]
    public string ATest()
    {
        return JsonConvert.SerializeObject("hola", Newtonsoft.Json.Formatting.Indented);
        //return "Hola!";
    }

    [WebMethod]
    public int Add(int a, int b)
    {
        return a + b;
    }


    //----------------------------------------------------------------------
    //--------------------------S E L E C T A L L---------------------------
    //----------------------------------------------------------------------

    [WebMethod]
    public string SelectAllHasilSidang()
    {
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

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
    }

    [WebMethod]
    public string SelectAllRencanaKehadiran()
    {
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

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
    }

    [WebMethod]
    public string SelectAllRencanaPenyelenggaraan()
    {
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

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
    }

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

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
    }

    //----------------------------------------------------------------------
    //--------------------------S E L E C T F I X---------------------------
    //----------------------------------------------------------------------

    [WebMethod]
    public string SelectFixHasilSidang()
    {
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

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
    }

    [WebMethod]
    public string SelectFixRencanaKehadiran()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectFixRencanaKehadiran", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
    }

    [WebMethod]
    public string SelectFixRencanaPenyelenggaraan()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SelectFixRencanaPenyelenggaraan", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
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

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
    }

    //----------------------------------------------------------------------
    //--------------------------S E L E C T---------------------------------
    //----------------------------------------------------------------------

    [WebMethod]
    public string SelectHasilSidang()
    {
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

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
    }

    [WebMethod]
    public string SelectRencanaKehadiran()
    {
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

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
    }

    [WebMethod]
    public string SelectRencanaPenyelenggaraan()
    {
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

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
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
                //cmd.Parameters.Add("id_user", SqlDbType.Int).Value = id_user;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                /*nilaiRow = ds.Tables[0].Select("id_user is not null").Length;
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
                }*/
                //first = Convert.ToString(ds.Tables[0].Rows[0].ToString());
                //result = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        //JavaScriptSerializer js = new JavaScriptSerializer();
        //Context.Response.Write(js.Serialize(ds));
        return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
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
                /*nilaiRow = ds.Tables[0].Select("id_users is not null").Length;
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
                }*/
                //result = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        //HttpContext.Current.Response.Write(result);
        return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
        //JavaScriptSerializer js = new JavaScriptSerializer();
        //Context.Response.Write(js.Serialize(ds));
        //return ds;
    }

    //----------------------------------------------------------------------
    //-----------------------------I N S E R T------------------------------
    //----------------------------------------------------------------------

    [WebMethod]
    public int InsertHasilSidang(string nama_satker, string tempat_sidang, string waktu_sidang, string nama_fora, string nama_working_group, string delegasi_bi, string delegasi_terkait, string negara_mitra, string agenda_pembahasan, string relevansi, string stance_bi, string stance_posisi_terkait, string stance_negara_mitra, string kesepakatan_telah, string kesepakatan_akan, string pending_issues, string rencana_tidak_lanjut, string fora_lain, string satker_lain, string jadwal_sidang_next, string lembaga_lain)
    {
        int insRecord = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("InsertHasilSidang", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
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
                insRecord = cmd.ExecuteNonQuery();
            }
        }
        return insRecord;
    }

    [WebMethod]
    public int InsertRencanaKehadiran(int periode_kehadiran, string nama_satker, string nama_event, string tempat_event, string waktu_event, string delegasi_adg, string delegasi_satker, string topik)
    {
        int insRecord = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("InsertRencanaKehadiran", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("periode_kehadiran", SqlDbType.Int, 100).Value = periode_kehadiran;
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
    public int InsertRencanaPenyelenggaraan(int periode_kehadiran, string nama_satker, string nama_event, string tempat_event, string waktu_event, string delegasi_adg, string delegasi_satker, string topik)
    {
        int insRecord = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("InsertRencanaPenyelenggaraan", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("periode_kehadiran", SqlDbType.Int, 100).Value = periode_kehadiran;
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
    public int InsertUsers(string nama_users, string satker_users, string email_users, string password_users, int jabatan_users)
    {
        int insRecord = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("InsertUsers", con))
            {
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
        return insRecord;
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
    public int UpdateFixRencanaKehadiran(int id_kehadiran)
    {
        int Rowdelete = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateFixRencanaKehadiran", con))
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
    public int UpdateFixRencanaPenyelenggaraan(int id_penyelenggaraan)
    {
        int Rowdelete = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateFixRencanaPenyelenggaraan", con))
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
    public int UpdateRencanaKehadiran(int id_kehadiran, int periode_kehadiran, string nama_satker, string nama_event, string tempat_event, string waktu_event, string delegasi_adg, string delegasi_satker, string topik)
    {
        int insRecord = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateRencanaKehadiran", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_kehadiran", SqlDbType.Int, 100).Value = id_kehadiran;
                cmd.Parameters.Add("periode_kehadiran", SqlDbType.Int, 100).Value = periode_kehadiran;
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
    public int UpdateRencanaPenyelenggaraan(int id_penyelenggaraan, int periode_kehadiran, string nama_satker, string nama_event, string tempat_event, string waktu_event, string delegasi_adg, string delegasi_satker, string topik)
    {
        int insRecord = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateRencanaPenyelenggaraan", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_penyelenggaraan", SqlDbType.Int, 100).Value = id_penyelenggaraan;
                cmd.Parameters.Add("periode_kehadiran", SqlDbType.Int, 100).Value = periode_kehadiran;
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
    public int UpdateUsers(int id_users, string nama_users, string satker_users, string email_users, string password_users, int jabatan_users)
    {
        int updRecord = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateUsers", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id_users", SqlDbType.Int).Value = id_users;
                cmd.Parameters.Add("nama_users", SqlDbType.VarChar, 100).Value = nama_users;
                cmd.Parameters.Add("satker_users", SqlDbType.VarChar, 100).Value = satker_users;
                cmd.Parameters.Add("email_users", SqlDbType.VarChar, 100).Value = email_users;
                cmd.Parameters.Add("password_users", SqlDbType.VarChar, 100).Value = password_users;
                cmd.Parameters.Add("jabatan_users", SqlDbType.Int).Value = jabatan_users;
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
