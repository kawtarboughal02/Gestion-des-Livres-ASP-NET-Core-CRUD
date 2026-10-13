using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_des_Livres.Pages
{
    public class EditEditeurModel : PageModel
    {
        public EditeurInfo EditeurInfo = new EditeurInfo();
        public string errorMessage = "";
        public string SuccessMessage = "";
        public void OnGet()
        {
            String IDEditeur = Request.Query["IDEditeur"];

            try
            {

                string connectionString = AppDb.ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "select * from Editeur where IDEditeur=@IDEditeur";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@IDEditeur", IDEditeur);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    EditeurInfo.IDEditeur = rd.GetInt32(0);
                    EditeurInfo.NomEditeur = rd.GetString(1);
                    EditeurInfo.DescripEditeur = rd.GetString(2);
                    EditeurInfo.EmailEdit = rd.GetString(3);
                    EditeurInfo.telephoneEdi = rd.GetString(4);
                    EditeurInfo.AddressEdit = rd.GetString(5);



                }


            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;

            }
        }
        public void OnPost()
        {
            int.TryParse(Request.Form["IDEditeur"], out int idValue);

            EditeurInfo.IDEditeur = idValue;
            EditeurInfo.NomEditeur = Request.Form["nom"];
            EditeurInfo.DescripEditeur = Request.Form["description"];
            EditeurInfo.EmailEdit = Request.Form["email"];
            EditeurInfo.telephoneEdi = Request.Form["telephone"];
            EditeurInfo.AddressEdit = Request.Form["adresse"];
            if (EditeurInfo.NomEditeur.Length == 0)
            {
                errorMessage = "tous les champs sont obligatoires";
                return;

            }
            try
            {

                string connectionString = AppDb.ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "update Editeur set NomEditeur=@nom, DescripEditeur=@description ,EmailEdit=@email ,telephoneEdi=@telephone, AddressEdit=@adresse  where IDEditeur=@IDEditeur";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@IDEditeur", EditeurInfo.IDEditeur);
                cmd.Parameters.AddWithValue("@nom", EditeurInfo.NomEditeur);
                cmd.Parameters.AddWithValue("@description", EditeurInfo.DescripEditeur);
                cmd.Parameters.AddWithValue("@email", EditeurInfo.EmailEdit);
                cmd.Parameters.AddWithValue("@telephone", EditeurInfo.telephoneEdi);
                cmd.Parameters.AddWithValue("@adresse", EditeurInfo.AddressEdit);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.ToString());
            }
            Response.Redirect("/Editeurs");




        }
    }
}
