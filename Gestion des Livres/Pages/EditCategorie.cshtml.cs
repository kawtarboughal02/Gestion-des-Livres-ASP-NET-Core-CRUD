using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_des_Livres.Pages
{
    public class EditCategorieModel : PageModel
    {
        public CategorieInfo CategorieInfo = new CategorieInfo();
        public void OnGet()
        {
            string IDCat = Request.Query["IDCat"];
            try
            {
                string connectionString = AppDb.ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "select * from Categorie where IDCat=@IDCat";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@IDCat", IDCat);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    CategorieInfo.IDCat = rd.GetInt32(0);
                    CategorieInfo.NomCat = rd.GetString(1);
                    CategorieInfo.DescriptionCat = rd.GetString(2);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.ToString());
            }
        }
        public void OnPost()
        {
            CategorieInfo.IDCat = Convert.ToInt32(Request.Form["IDCat"]);
            CategorieInfo.NomCat = Request.Form["NomCat"];
            CategorieInfo.DescriptionCat = Request.Form["DescriptionCat"];
            try
            {
                string connectionString = AppDb.ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "update Categorie set NomCat=@NomCat,DescriptionCat=@DescriptionCat where IDCat=@IDCat";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@IDCat", CategorieInfo.IDCat);
                cmd.Parameters.AddWithValue("@NomCat", CategorieInfo.NomCat);
                cmd.Parameters.AddWithValue("@DescriptionCat", CategorieInfo.DescriptionCat);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.ToString());
            }
            Response.Redirect("/Categories");
        }
    }
}
