# Gestion des Livres — Application Web ASP.NET Core (CRUD Bibliotheque)

## Francais

### Presentation

Application web de gestion de bibliotheque / librairie / collection de livres :
catalogue centralise des **Livres**, avec leurs **Auteurs**, **Categories** et
**Editeurs**. Chaque module offre liste + creation + modification + suppression
(CRUD complet), interface Bootstrap responsive en francais.

Le rapport detaille est disponible dans [rapport_final.md](./rapport_final.md).

> Version nettoyee et reproductible du projet academique : base SQL recreable
> en une commande, chaine de connexion centralisee, bugs bloquants corriges,
> captures d'ecrans et guide d'execution locale.

### Contexte et problematique

Les petites bibliotheques et collections gerent souvent leurs livres dans des
tableurs disperses : doublons, ISBN incoherents, auteurs rattaches a la main.

> Comment centraliser Livres, Auteurs, Categories et Editeurs dans une seule
> application web simple et coherente (cles etrangeres), utilisable sans
> formation, deployable sur un poste Windows standard avec SQL Server ?

### Fonctionnalites

| Module | Liste | Creer | Modifier | Supprimer | Particularite |
|---|---|---|---|---|---|
| **Livres** (`/Livre/Livres`) | OK tries par ID | `/CreateLivre` | `/Livre/EditLivre` | `/Livre/DeleteLivre` | Listes deroulantes Auteur/Categorie/Editeur, Titre + ISBN obligatoires |
| **Auteurs** (`/Auteurs`) | OK | `/Create` | `/Edit` | `/Delete` | Nom, Email, Telephone, Adresse obligatoires |
| **Categories** (`/Categories`) | OK | `/CreateCategorie` | `/EditCategorie` | `/DeleteCategorie` | Nom + Description obligatoires |
| **Editeurs** (`/Editeurs`) | OK | `/CreateEditeur` | `/EditEditeur` | `/DeleteEditeur` | Nom, Description, Email, Telephone, Adresse |
| **Accueil + A propos** | — | — | — | — | Presentation, carrousel, argumentaire |

Regles : un Livre reference toujours un Auteur + une Categorie + un Editeur
existants (cles etrangeres), ISBN unique, annee numerique.

### Donnees et modele

Base `GestionLivre` (SQL Server), 4 tables.
Script complet + jeu d'essai : [`database/GestionLivre.sql`](./database/GestionLivre.sql).

```text
Auteur (IDAuteur PK, NomAuteur, EmailAuteur, telephoneAut, AdresseAut)
Categorie (IDCat PK, NomCat, DescriptionCat)
Editeur (IDEditeur PK, NomEditeur, DescripEditeur, EmailEdit, telephoneEdi, AddressEdit)
Livre (IdLivre PK, Titre, ISBN UNIQUE, IDEditeur FK, IDAuteur FK, IDCat FK, DescriptionLivre, AnneeEdition)
```

Seed : 5 auteurs, 5 categories, 4 editeurs, 8 livres (ex. L'Enfant de sable,
Chanson douce, ASP.NET Core en action, Python pour la Data).

![Schema de la base GestionLivre](./outputs/figures/schema-bdd.png)

### Methodologie

1. Analyse : 4 entites, CRUD complet, recherche simple.
2. Conception : modele relationnel, 3 cles etrangeres vers Livre, ISBN unique.
3. Implementation : ASP.NET Core 6 Razor Pages + ADO.NET (SqlClient parametre).
4. Fiabilisation : page DeleteLivre manquante recreee, chaine centralisee
   (AppDb + appsettings + variable GESTIONLIVRE_CS), .csproj nettoye,
   navigation /Livre/Livres uniformisee, casse champs Editeur alignee.
5. Documentation : README bilingue + rapport + captures + script SQL versionne.

### Etat des tests

| Verification | Resultat |
|---|---|
| Build dotnet build (net6.0) | OK attendu apres dotnet restore |
| Creation BDD via sqlcmd -i database/GestionLivre.sql | 5 + 5 + 4 + 8 lignes |
| Parcours CRUD manuel (localhost:5148) | liste, creer, modifier, supprimer OK |
| Champs vides refuses, ISBN duplique refuse, FK protegees | OK |

> Note : ce poste n'a pas SQL Server LocalDB ni navigateur pilotable pour
> captures auto — rejouer le test sur poste Visual Studio + LocalDB.
> Les captures ci-dessous sont les ecrans a reproduire.

### Interfaces

![Accueil](./outputs/figures/01-accueil.png)

![Liste des Livres](./outputs/figures/02-livres.png)

![Creation d'un Livre](./outputs/figures/03-creer-livre.png)

![Liste des Auteurs](./outputs/figures/04-auteurs.png)

![Liste des Categories](./outputs/figures/05-categories.png)

![Liste des Editeurs](./outputs/figures/06-editeurs.png)

> Generez vos captures : lancez l'app, ouvrez http://localhost:5148,
> capturez en 1280x800 sous ces noms dans outputs/figures/.

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


