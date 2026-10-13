using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_des_Livres.Pages
{
    public class AuteursModel : PageModel
    {
        public List<AuteurInfo> listAuteurs = new List<AuteurInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = AppDb.ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "select * from Auteur";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    AuteurInfo auteurinf = new AuteurInfo();
                    auteurinf.IDAuteur = rd.GetInt32(0);
                    auteurinf.NomAuteur = rd.GetString(1);
                    auteurinf.EmailAuteur = rd.GetString(2);
                    auteurinf.telephoneAut = rd.GetString(3);
                    auteurinf.AdresseAut = rd.GetString(4);
                    listAuteurs.Add(auteurinf);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.ToString());
            }
        }
    }
}
