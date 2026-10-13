using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_des_Livres.Pages
{
    public class CreateCategorieModel : PageModel
    {

        public CategorieInfo categorieInfo = new CategorieInfo();
        public string errorMessage = "";
        public string SuccessMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            categorieInfo.NomCat = Request.Form["NomCat"];
            categorieInfo.DescriptionCat = Request.Form["DescriptionCat"];
            if (categorieInfo.NomCat.Length == 0 || categorieInfo.DescriptionCat.Length == 0)
            {
                errorMessage = "tous les champs sont obligatoires";
                return;
            }
            try
            {
                string connectionString = AppDb.ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "insert into Categorie(NomCat,DescriptionCat)values(@NomCat, @DescriptionCat)";
                SqlCommand cmd = new SqlCommand(sql, con);
                // cmd.Parameters.AddWithValue("@IDCat",categorieInfo.IDCat);
                cmd.Parameters.AddWithValue("@NomCat", categorieInfo.NomCat);
                cmd.Parameters.AddWithValue("@DescriptionCat", categorieInfo.DescriptionCat);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception" + ex.ToString());
            }
        }
    }
}
