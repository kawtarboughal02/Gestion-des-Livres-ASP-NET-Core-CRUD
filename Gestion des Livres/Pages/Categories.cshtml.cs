using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_des_Livres.Pages
{
    public class CategoriesModel : PageModel
    {
        public List<CategorieInfo> listCategories = new List<CategorieInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = AppDb.ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "select * from Categorie";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    CategorieInfo categorieinf = new CategorieInfo();
                    categorieinf.IDCat = rd.GetInt32(0);
                    categorieinf.NomCat = rd.GetString(1);
                    categorieinf.DescriptionCat = rd.GetString(2);
                    listCategories.Add(categorieinf);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.ToString());
            }
        }
    }
}
