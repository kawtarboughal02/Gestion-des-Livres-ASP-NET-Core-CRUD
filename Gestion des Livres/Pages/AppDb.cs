namespace Gestion_des_Livres.Pages
{
    /// <summary>
    /// Point d'accès centralisé à la chaîne de connexion.
    /// La valeur par défaut cible SQL Server LocalDB (fourni avec Visual Studio),
    /// ce qui rend le projet portable d'une machine à l'autre.
    /// Elle peut être redéfinie via appsettings.json (ConnectionStrings:GestionLivre)
    /// ou la variable d'environnement GESTIONLIVRE_CS.
    /// </summary>
    public static class AppDb
    {
        public static string ConnectionString { get; set; } =
            "Server=(localdb)\\MSSQLLocalDB;Database=GestionLivre;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
    }
}
