using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_des_Livres.Pages
{
    public class CreateEditeurModel : PageModel
    {
        public EditeurInfo EditeurInfo = new EditeurInfo();
        public string errorMessage = "";
        public string SuccessMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            EditeurInfo.NomEditeur = Request.Form["nom"];
            EditeurInfo.DescripEditeur = Request.Form["description"];
            EditeurInfo.EmailEdit = Request.Form["email"];
            EditeurInfo.telephoneEdi = Request.Form["telephone"];
            EditeurInfo.AddressEdit = Request.Form["adresse"];


            if (EditeurInfo.NomEditeur.Length == 0 || EditeurInfo.DescripEditeur.Length == 0)
            {
                errorMessage = "tous les champs sont obligatoires";
                return;
            }
            try
            {
                string connectionString = AppDb.ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "insert into Editeur(NomEditeur,DescripEditeur,EmailEdit,telephoneEdi,AddressEdit) values(@NomEditeur, @DescripEditeur, @EmailEdit, @telephoneEdi,@AddressEdit)";
                SqlCommand cmd = new SqlCommand(sql, con);
                // cmd.Parameters.AddWithValue("@id",EditeurInfo.id);
                cmd.Parameters.AddWithValue("@NomEditeur", EditeurInfo.NomEditeur);
                cmd.Parameters.AddWithValue("@DescripEditeur", EditeurInfo.DescripEditeur);
                cmd.Parameters.AddWithValue("@EmailEdit", EditeurInfo.EmailEdit);
                cmd.Parameters.AddWithValue("@telephoneEdi", EditeurInfo.telephoneEdi);
                cmd.Parameters.AddWithValue("@AddressEdit", EditeurInfo.AddressEdit);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception" + ex.ToString());
            }
            Response.Redirect("/Editeurs");
        }
    }
}
