using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_des_Livres.Pages
{
    public class EditModel : PageModel
    {
        public AuteurInfo AuteurInfo = new AuteurInfo();
        public void OnGet()
        {
            string IDAuteur = Request.Query["IDAuteur"];
            try
            {
                string connectionString = AppDb.ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "select * from Auteur where IDAuteur=@IDAuteur";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@IDAuteur", IDAuteur);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    AuteurInfo.IDAuteur = rd.GetInt32(0);
                    AuteurInfo.NomAuteur = rd.GetString(1);
                    AuteurInfo.EmailAuteur = rd.GetString(2);
                    AuteurInfo.telephoneAut = rd.GetString(3);
                    AuteurInfo.AdresseAut = rd.GetString(4);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.ToString());
            }
        }
        public void OnPost()
        {
            AuteurInfo.IDAuteur = Convert.ToInt32(Request.Form["IDAuteur"]);
            AuteurInfo.NomAuteur = Request.Form["NomAuteur"];
            AuteurInfo.EmailAuteur = Request.Form["EmailAuteur"];
            AuteurInfo.telephoneAut = Request.Form["telephoneAut"];
            AuteurInfo.AdresseAut = Request.Form["AdresseAut"];
            try
            {
                string connectionString = AppDb.ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "update Auteur set NomAuteur=@NomAuteur,EmailAuteur=@EmailAuteur,telephoneAut=@telephoneAut,AdresseAut=@AdresseAut where IDAuteur=@IDAuteur";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@IDAuteur", AuteurInfo.IDAuteur);
                cmd.Parameters.AddWithValue("@NomAuteur", AuteurInfo.NomAuteur);
                cmd.Parameters.AddWithValue("@EmailAuteur", AuteurInfo.EmailAuteur);
                cmd.Parameters.AddWithValue("@telephoneAut", AuteurInfo.telephoneAut);
                cmd.Parameters.AddWithValue("@AdresseAut", AuteurInfo.AdresseAut);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.ToString());
            }
            Response.Redirect("/Auteurs");
        }

    }
}
