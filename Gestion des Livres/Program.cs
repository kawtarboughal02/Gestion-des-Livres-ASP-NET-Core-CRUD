namespace Gestion_des_Livres
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            // Centralisation de la chaîne de connexion.
            // Priorité : variable d'environnement GESTIONLIVRE_CS, puis appsettings.json,
            // sinon la valeur par défaut (SQL Server LocalDB) définie dans AppDb.
            var csEnv = Environment.GetEnvironmentVariable("GESTIONLIVRE_CS");
            var csConfig = builder.Configuration.GetConnectionString("GestionLivre");
            if (!string.IsNullOrWhiteSpace(csEnv))
                Gestion_des_Livres.Pages.AppDb.ConnectionString = csEnv;
            else if (!string.IsNullOrWhiteSpace(csConfig))
                Gestion_des_Livres.Pages.AppDb.ConnectionString = csConfig;

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
