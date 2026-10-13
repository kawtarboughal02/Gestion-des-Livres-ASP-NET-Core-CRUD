using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_des_Livres.Pages
{
    public class CreateModel : PageModel
    {
        public AuteurInfo auteurInfo = new AuteurInfo();
        public string errorMessage = "";
        public string SuccessMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            auteurInfo.NomAuteur = Request.Form["NomAuteur"];
            auteurInfo.EmailAuteur = Request.Form["EmailAuteur"];
            auteurInfo.telephoneAut = Request.Form["telephoneAut"];
            auteurInfo.AdresseAut = Request.Form["AdresseAut"];
            if (auteurInfo.NomAuteur.Length == 0 || auteurInfo.EmailAuteur.Length == 0 || auteurInfo.telephoneAut.Length == 0 || auteurInfo.AdresseAut.Length == 0)
            {
                errorMessage = "tous les champs sont obligatoires";
                return;
            }
            try
            {
                string connectionString = AppDb.ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "insert into Auteur(NomAuteur,EmailAuteur,telephoneAut,AdresseAut)values(@NomAuteur, @EmailAuteur, @telephoneAut, @AdresseAut)";
                SqlCommand cmd = new SqlCommand(sql, con);
                // cmd.Parameters.AddWithValue("@IDAuteur",auteurInfo.IDAuteur);
                cmd.Parameters.AddWithValue("@NomAuteur", auteurInfo.NomAuteur);
                cmd.Parameters.AddWithValue("@EmailAuteur", auteurInfo.EmailAuteur);
                cmd.Parameters.AddWithValue("@telephoneAut", auteurInfo.telephoneAut);
                cmd.Parameters.AddWithValue("@AdresseAut", auteurInfo.AdresseAut);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception" + ex.ToString());
            }
        }
    }
}
