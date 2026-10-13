using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_des_Livres.Pages.Livre
{
    public class LivresModel : PageModel
    {
        public List<LivreInfo> ListLivre = new List<LivreInfo>();

        public void OnGet()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppDb.ConnectionString))
                {
                    con.Open();
                    string sql = "select * from Livre order by IdLivre";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            LivreInfo liv = new LivreInfo();
                            liv.IdLivre = rd.GetInt32(0);
                            liv.Titre = rd.GetString(1);
                            liv.ISBN = rd.GetString(2);
                            liv.IDEditeur = rd.GetInt32(3);
                            liv.IDAuteur = rd.GetInt32(4);
                            liv.IDCat = rd.GetInt32(5);
                            liv.DescriptionLivre = rd.GetString(6);
                            liv.AnneeEdition = rd.GetInt32(7);
                            ListLivre.Add(liv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.ToString());
            }
        }
    }
}
