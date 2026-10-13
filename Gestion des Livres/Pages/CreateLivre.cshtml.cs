using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace Gestion_des_Livres.Pages
{
    public class CreateLivreModel : PageModel
    {
        public LivreInfo livreInfo = new LivreInfo();
        public string errorMessage = "";
        public string SuccessMessage = "";

        public List<SelectListItem> ListeAuteurs = new List<SelectListItem>();
        public List<SelectListItem> ListeCategories = new List<SelectListItem>();
        public List<SelectListItem> ListeEditeurs = new List<SelectListItem>();

        public void OnGet()
        {
            ChargerListes();
        }

        private void ChargerListes()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppDb.ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("select IDAuteur, NomAuteur from Auteur order by NomAuteur", con))
                    using (SqlDataReader rd = cmd.ExecuteReader())
                        while (rd.Read())
                            ListeAuteurs.Add(new SelectListItem(rd.GetString(1), rd.GetInt32(0).ToString()));

                    using (SqlCommand cmd = new SqlCommand("select IDCat, NomCat from Categorie order by NomCat", con))
                    using (SqlDataReader rd = cmd.ExecuteReader())
                        while (rd.Read())
                            ListeCategories.Add(new SelectListItem(rd.GetString(1), rd.GetInt32(0).ToString()));

                    using (SqlCommand cmd = new SqlCommand("select IDEditeur, NomEditeur from Editeur order by NomEditeur", con))
                    using (SqlDataReader rd = cmd.ExecuteReader())
                        while (rd.Read())
                            ListeEditeurs.Add(new SelectListItem(rd.GetString(1), rd.GetInt32(0).ToString()));
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Erreur de connexion : " + ex.Message;
            }
        }

        public void OnPost()
        {
            livreInfo.Titre = Request.Form["Titre"];
            livreInfo.ISBN = Request.Form["ISBN"];
            livreInfo.DescriptionLivre = Request.Form["DescriptionLivre"];
            int.TryParse(Request.Form["IDAuteur"], out int idAuteur);
            int.TryParse(Request.Form["IDCat"], out int idCat);
            int.TryParse(Request.Form["IDEditeur"], out int idEditeur);
            int.TryParse(Request.Form["AnneeEdition"], out int annee);
            livreInfo.IDAuteur = idAuteur;
            livreInfo.IDCat = idCat;
            livreInfo.IDEditeur = idEditeur;
            livreInfo.AnneeEdition = annee;

            if (string.IsNullOrWhiteSpace(livreInfo.Titre) || string.IsNullOrWhiteSpace(livreInfo.ISBN))
            {
                errorMessage = "Le titre et l'ISBN sont obligatoires";
                ChargerListes();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(AppDb.ConnectionString))
                {
                    con.Open();
                    string sql = "insert into Livre(Titre,ISBN,IDEditeur,IDAuteur,IDCat,DescriptionLivre,AnneeEdition) " +
                                 "values(@Titre,@ISBN,@IDEditeur,@IDAuteur,@IDCat,@DescriptionLivre,@AnneeEdition)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Titre", livreInfo.Titre);
                        cmd.Parameters.AddWithValue("@ISBN", livreInfo.ISBN);
                        cmd.Parameters.AddWithValue("@IDEditeur", livreInfo.IDEditeur);
                        cmd.Parameters.AddWithValue("@IDAuteur", livreInfo.IDAuteur);
                        cmd.Parameters.AddWithValue("@IDCat", livreInfo.IDCat);
                        cmd.Parameters.AddWithValue("@DescriptionLivre", livreInfo.DescriptionLivre);
                        cmd.Parameters.AddWithValue("@AnneeEdition", livreInfo.AnneeEdition);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Erreur lors de l'enregistrement : " + ex.Message;
                ChargerListes();
                return;
            }

            Response.Redirect("/Livre/Livres");
        }
    }
}
