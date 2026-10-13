### Application locale

| Service | Adresse |
|---|---|
| Application | http://localhost:5148 (HTTP) / https://localhost:7264 (HTTPS) |

Routes : /Index, /About, /Auteurs, /Categories, /Editeurs, /Livre/Livres,
/CreateLivre, /Create, /CreateCategorie, /CreateEditeur.

### Structure

```text
.
├── Gestion des Livres.sln
├── Gestion des Livres/        # app ASP.NET Core 6 Razor Pages
│   ├── Pages/ (Index, About, Auteurs, Categories, Editeurs, Livre/Livres,
│   │   Create*, Edit*, Delete*, AppDb.cs, *Info.cs)
│   ├── Program.cs
│   ├── appsettings.json
│   └── wwwroot/ (css, js, img, lib)
├── database/GestionLivre.sql  # creation BDD + FK + seed
├── outputs/figures/           # captures + schema
├── rapport_final.md
└── .gitignore
```

### Installation et execution

Prerequis : Windows, .NET 6 SDK, SQL Server (LocalDB ou Express), sqlcmd.

```powershell
cd "Gestion des Livres"
sqlcmd -S "(localdb)\MSSQLLocalDB" -i database\GestionLivre.sql
dotnet restore "Gestion des Livres.sln"
dotnet build "Gestion des Livres.sln" --no-restore
dotnet run --project "Gestion des Livres\Gestion des Livres.csproj"
# ouvrir http://localhost:5148
```

Surcharge sans toucher au code :

```powershell
$env:GESTIONLIVRE_CS="Server=.\SQLEXPRESS;Database=GestionLivre;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
dotnet run --project "Gestion des Livres\Gestion des Livres.csproj"
```

Test rapide : Livres -> Add Livre -> Titre Le Petit Prince, ISBN 9782070408504,
Auteur/Categorie/Editeur au choix, Annee 1943 -> Enregistrer -> Modifier ->
Supprimer. Repeter sur les 3 autres modules.

### Technologies et competences

| Technologie | Utilisation |
|---|---|
| C#, ASP.NET Core 6 Razor Pages | Pages OnGet/OnPost, routage, redirections |
| ADO.NET (System.Data.SqlClient) | SqlConnection, SqlCommand parametrees, SqlDataReader |
| SQL Server / T-SQL | 4 tables, PK/FK, ISBN UNIQUE, script + seed |
| Bootstrap 5, jQuery, OwlCarousel | Responsive, tableaux, carrousel |
| Visual Studio 2022, .NET CLI | Dev, restore, build, run |

### Objectif, problematique, solutions, impact

- **Objectif :** centraliser une collection de livres, CRUD relationnel en .NET.
- **Problematique :** tableurs disperses, doublons, pas d'integrite.
- **Solutions :** schema contraint (FK + UNIQUE), formulaires listes liees,
  requetes parametrees, chaine centralisee, script SQL rejouable.
- **Impact :** saisie guidee sans erreur, collection requetable unique,
  install en 4 commandes, base saine pour extensions (recherche, stats, IA).

### Lien Data Scientist et IA

Pas de modele ML embarque : brique data engineering / applicative.
Suite naturelle : export CSV/Parquet vers Pandas, recherche semantique
(embeddings titre+description), recommandation, classification NLP des
categories, dashboard livres par categorie/editeur/annee.
Socle Data : SQL, qualite, collecte applicative.

### Limites

- Pas d'authentification reelle, suppressions sans confirmation, recherche
  navbar decorative, concurrence minimale, seed de demo francophone.

### Auteure

**Kawtar Boughal** — [LinkedIn](https://www.linkedin.com/in/kawtar-boughal02) — [GitHub](https://github.com/kawtarboughal02)

---

## English

### Overview

Library collection manager, ASP.NET Core 6 Razor Pages + SQL Server. Central
catalogue of Books linked to Authors, Categories, Publishers. Full CRUD each,
French Bootstrap UI. Report: [rapport_final.md](./rapport_final.md).

### Problem and solution

Scattered spreadsheets -> duplicates, broken links. Central relational DB
(FK, unique ISBN), guided dropdown forms, parameterised queries, one-command
seed (database/GestionLivre.sql).

### Data model

GestionLivre DB, 4 tables (Auteur, Categorie, Editeur, Livre). Seed: 5 + 5 + 4 + 8.

### Run locally

Windows, .NET 6 SDK, SQL Server LocalDB/Express.

```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -i database\GestionLivre.sql
dotnet restore "Gestion des Livres.sln"
dotnet build "Gestion des Livres.sln" --no-restore
dotnet run --project "Gestion des Livres\Gestion des Livres.csproj"
# open http://localhost:5148
```

Override connection string via GESTIONLIVRE_CS env var.
Routes: /Index, /About, /Auteurs, /Categories, /Editeurs, /Livre/Livres.

### Tech

C#, ASP.NET Core 6 Razor Pages, ADO.NET, SQL Server/T-SQL, Bootstrap 5,
Visual Studio 2022, .NET CLI.

### Limits

Demo scope: no real auth, deletes without confirm dialog, decorative search,
French demo seed. Clean relational data ready for Data/ML extensions.
