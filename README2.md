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
