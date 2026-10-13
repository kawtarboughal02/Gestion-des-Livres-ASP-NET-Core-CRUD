using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_des_Livres.Pages
{
    public class EditeursModel : PageModel
    {
        public List<EditeurInfo> listEditeurs = new List<EditeurInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = AppDb.ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "select * from Editeur";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    EditeurInfo EditeurInfo = new EditeurInfo();
                    EditeurInfo.IDEditeur = rd.GetInt32(0);
                    EditeurInfo.NomEditeur = rd.GetString(1);
                    EditeurInfo.DescripEditeur = rd.GetString(2);
                    EditeurInfo.EmailEdit = rd.GetString(3);
                    EditeurInfo.telephoneEdi = rd.GetString(4);
                    EditeurInfo.AddressEdit = rd.GetString(5);
                    listEditeurs.Add(EditeurInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.ToString());
            }
        }
    }
}
