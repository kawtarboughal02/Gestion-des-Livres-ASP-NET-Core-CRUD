-- ============================================================
-- Gestion des Livres — Script de creation BDD SQL Server
-- Base : GestionLivre
-- Compatible : SQL Server LocalDB / Express / Standard
-- Ordre : creation base + tables + contraintes + jeu d'essai
-- ============================================================
IF DB_ID(N'GestionLivre') IS NULL
    CREATE DATABASE GestionLivre;
GO
USE GestionLivre;
GO

-- Nettoyage si re-execution (ordre inverse des FK)
IF OBJECT_ID(N'dbo.Livre','U') IS NOT NULL DROP TABLE dbo.Livre;
IF OBJECT_ID(N'dbo.Auteur','U') IS NOT NULL DROP TABLE dbo.Auteur;
IF OBJECT_ID(N'dbo.Categorie','U') IS NOT NULL DROP TABLE dbo.Categorie;
IF OBJECT_ID(N'dbo.Editeur','U') IS NOT NULL DROP TABLE dbo.Editeur;
GO

CREATE TABLE dbo.Auteur (
    IDAuteur     INT IDENTITY(1,1) PRIMARY KEY,
    NomAuteur    NVARCHAR(150) NOT NULL,
    EmailAuteur  NVARCHAR(150) NOT NULL,
    telephoneAut NVARCHAR(50)  NOT NULL,
    AdresseAut   NVARCHAR(250) NOT NULL
);

CREATE TABLE dbo.Categorie (
    IDCat          INT IDENTITY(1,1) PRIMARY KEY,
    NomCat         NVARCHAR(100) NOT NULL,
    DescriptionCat NVARCHAR(500) NOT NULL
);

CREATE TABLE dbo.Editeur (
    IDEditeur      INT IDENTITY(1,1) PRIMARY KEY,
    NomEditeur     NVARCHAR(150) NOT NULL,
    DescripEditeur NVARCHAR(500) NOT NULL,
    EmailEdit      NVARCHAR(150) NULL,
    telephoneEdi   NVARCHAR(50)  NULL,
    AddressEdit    NVARCHAR(250) NULL
);

CREATE TABLE dbo.Livre (
    IdLivre         INT IDENTITY(1,1) PRIMARY KEY,
    Titre           NVARCHAR(200) NOT NULL,
    ISBN            NVARCHAR(50)  NOT NULL UNIQUE,
    IDEditeur       INT NOT NULL,
    IDAuteur        INT NOT NULL,
    IDCat           INT NOT NULL,
    DescriptionLivre NVARCHAR(1000) NULL,
    AnneeEdition    INT NOT NULL,
    CONSTRAINT FK_Livre_Auteur   FOREIGN KEY (IDAuteur)  REFERENCES dbo.Auteur(IDAuteur),
    CONSTRAINT FK_Livre_Categorie FOREIGN KEY (IDCat)    REFERENCES dbo.Categorie(IDCat),
    CONSTRAINT FK_Livre_Editeur  FOREIGN KEY (IDEditeur) REFERENCES dbo.Editeur(IDEditeur)
);
GO

-- ============ JEU D'ESSAI (coherent et francophone) ============
SET IDENTITY_INSERT dbo.Auteur ON;
-- (laisse IDENTITY auto : INSERT sans ID)
SET IDENTITY_INSERT dbo.Auteur OFF;

INSERT INTO dbo.Auteur (NomAuteur, EmailAuteur, telephoneAut, AdresseAut) VALUES
(N'Tahar Ben Jelloun',  N't.benjelloun@mail.com',  N'+212600111111', N'Rabat, Maroc'),
(N'Leila Slimani',      N'l.slimani@mail.com',      N'+33600111222',  N'Paris, France'),
(N'Victor Hugo',        N'v.hugo@mail.com',         N'+33600222333',  N'Paris, France'),
(N'Chinua Achebe',      N'c.achebe@mail.com',       N'+234800333444', N'Lagos, Nigeria'),
(N'Fatima Mernissi',    N'f.mernissi@mail.com',     N'+212600555666', N'Fes, Maroc');

INSERT INTO dbo.Categorie (NomCat, DescriptionCat) VALUES
(N'Roman',        N'Fictions narratives longues, francophones et internationales'),
(N'Poesie',       N'Recueils et oeuvres poetiques'),
(N'Essai',        N'Analyses, sociologie et reflexions'),
(N'Jeunesse',     N'Albums et romans pour jeunes lecteurs'),
(N'Informatique', N'Programmation, bases de donnees et data');

INSERT INTO dbo.Editeur (NomEditeur, DescripEditeur, EmailEdit, telephoneEdi, AddressEdit) VALUES
(N'Seuil',            N'Maison d''edition francaise, litterature generale', N'contact@seuil.fr',      N'+33144050506', N'Paris, France'),
(N'Gallimard',        N'Maison d''edition francaise de reference',          N'contact@gallimard.fr',   N'+33149541400', N'Paris, France'),
(N'La Croisee des Chemins', N'Editeur marocain, litterature et sciences humaines', N'contact@lacroisee.ma', N'+212522391234', N'Casablanca, Maroc'),
(N'ENI Editions',      N'Editeur specialise informatique et technique',      N'contact@eni.fr',         N'+33251803434', N'Nantes, France');

INSERT INTO dbo.Livre (Titre, ISBN, IDEditeur, IDAuteur, IDCat, DescriptionLivre, AnneeEdition) VALUES
(N'L''Enfant de sable',        N'9782020201234', 1, 1, 1, N'Roman emblematique de la litterature marocaine.', 1985),
(N'Chanson douce',             N'9782070604567', 2, 2, 1, N'Prix Goncourt 2016, portrait d''une famille parisienne.', 2016),
(N'Les Miserables',            N'9782070405678', 2, 3, 1, N'Chef-d''oeuvre du XIXe siecle.', 1862),
(N'Le monde s''effondre',      N'9782719107890', 1, 4, 1, N'Classique de la litterature africaine anglophone.', 1958),
(N'Reves de femmes',           N'9789981034567', 3, 5, 3, N'Essai sur les femmes et l''espace public au Maroc.', 1994),
(N'ASP.NET Core en action',    N'9782409023456', 4, 3, 5, N'Guide pratique Razor Pages, ADO.NET et SQL Server.', 2023),
(N'Python pour la Data',       N'9782409034567', 4, 5, 5, N'Pandas, SQL et premiers modeles pour la data science.', 2024),
(N'Contes du Maroc',           N'9789981045678', 3, 1, 4, N'Recueil jeunesse illustre.', 2010);
GO

-- Verifications
SELECT 'Auteur' AS TableName, COUNT(*) AS Nb FROM dbo.Auteur
UNION ALL SELECT 'Categorie', COUNT(*) FROM dbo.Categorie
UNION ALL SELECT 'Editeur', COUNT(*) FROM dbo.Editeur
UNION ALL SELECT 'Livre', COUNT(*) FROM dbo.Livre;
GO
