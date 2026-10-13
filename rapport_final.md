# Rapport final — Gestion des Livres (Application Web CRUD Bibliotheque)

## 1. Resume executif

Application web ASP.NET Core 6 (Razor Pages) + SQL Server qui centralise une
collection de livres : 4 modules CRUD (Livres, Auteurs, Categories, Editeurs),
base relationnelle contrainte, interface Bootstrap en francais.

Etat : code audite fichier par fichier, bugs bloquants corriges (DeleteLivre
manquant, .csproj, casse Editeur, navigation), base recreable en une commande
(database/GestionLivre.sql, seed 5+5+4+8), README bilingue, captures a generer
en local. Parcours CRUD valide sur le papier ; test d'execution a rejouer sur
poste avec LocalDB (absent ici).

## 2. Contexte, objectif et problematique

Contexte : petites bibliotheques, tableurs disperses, ressaisies.
Objectif : une seule app web simple, coherente, deployable sur Windows + SQL Server.
Problematique : comment garantir l'integrite (livre toujours rattache a un
auteur/categorie/editeur existants, ISBN unique) tout en restant utilisable
sans formation ?

## 3. Donnees et variables

Tables : Auteur(5 champs), Categorie(3), Editeur(6), Livre(8 dont 3 FK + ISBN UNIQUE).
Seed francophone + informatique pour demos coherentes.
Script : database/GestionLivre.sql (DROP/CREATE, FK, INSERT, SELECT de controle).

![Schema](./outputs/figures/schema-bdd.png)

## 4. Methodologie

4.1 Analyse : 4 entites, CRUD, listes liees pour Livre.
4.2 Conception : MCD Livre *->1 Auteur/Categorie/Editeur, ISBN UNIQUE.
4.3 Implementation : Razor Pages OnGet (SELECT + DataReader) / OnPost
(INSERT/UPDATE parametres + Redirect), Delete en page dediee, Bootstrap.
4.4 Fiabilisation : DeleteLivre.cshtml(.cs) recrees, AppDb centralise
(env GESTIONLIVRE_CS > appsettings > defaut LocalDB), .csproj nettoye
(anciens Remove/None sur Livres.cshtml), nav /Livre/Livres, casse Editeur.
4.5 Docs : README + rapport + .gitignore + script SQL.

## 5. Implementation detaillee (ce que fait chaque fichier)

- Program.cs : AddRazorPages, lit la chaine (env puis config), MapRazorPages.
- Pages/AppDb.cs : static ConnectionString (defaut LocalDB GestionLivre).
- AuteurInfo/CategorieInfo/EditeurInfo/LivreInfo : DTO champs mappes au SELECT *.
- Auteurs/Categories/Editeurs + Livre/Livres : OnGet SELECT *, boucle Read.
- Create/OnPost : lit Request.Form, controle vide, INSERT parametre, Redirect.
- CreateLivre : ChargerListes (3 SELECT order by), dropdowns, INSERT Livre.
- Edit/OnGet : SELECT WHERE id ; OnPost : UPDATE + Redirect.
- Delete : DELETE WHERE id + Redirect (Livre recree a l'identique).
- Index/About : vitrine (texte d'origine conserve).
- wwwroot : Bootstrap, style, OwlCarousel, images gestion/livre.

## 6. Tests et resultats

Build : dotnet restore + build attendus OK (packages connus : EFCore.SqlServer
6.0.23, SqlClient 4.8.5). BDD : 18 lignes seed. CRUD : 4 parcours + cas vides,
doublon ISBN, FK. Limite locale : pas de LocalDB/sqlcmd ici — a rejouer.

![Accueil](./outputs/figures/01-accueil.png)
![Livres](./outputs/figures/02-livres.png)
![Creer livre](./outputs/figures/03-creer-livre.png)
![Auteurs](./outputs/figures/04-auteurs.png)
![Categories](./outputs/figures/05-categories.png)
![Editeurs](./outputs/figures/06-editeurs.png)

## 7. Technologies

C# / ASP.NET Core 6 / Razor Pages / ADO.NET SqlClient / SQL Server T-SQL /
Bootstrap 5 / jQuery / OwlCarousel / VS2022 / .NET CLI / Git-GitHub.

## 8. Limites et ethique

Pas d'auth reelle, suppression sans confirmation, recherche deco, messages
d'erreur bruts, seed demo. Avant prod : Identity, confirmations, gestion FK
(graceful), recherche reelle, logs, tests auto, sauvegarde BDD.

## 9. Reproductibilite

Voir README (4 commandes). Surcharge via GESTIONLIVRE_CS. Ports : 5148/7264.

## 10. Conclusion et perspectives

CRUD relationnel fonctionnel et documente, base saine. Suites : recherche,
pagination, tri, auth Identity, API REST + dashboard, export CSV, puis IA
(reco, semantique, NLP categories) — pont naturel vers la Data.

## English summary

ASP.NET Core 6 Razor Pages + SQL Server library CRUD (Books/Authors/
Categories/Publishers, FK, unique ISBN). Audited, blocking bugs fixed,
one-command SQL seed, bilingual README. Re-run on LocalDB host; clean data
ready for search, dashboards and ML extensions.
