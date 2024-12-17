# 📚 **Biblothèque Application**

Ce projet représente une **API REST** pour la gestion d'une bibliothèque de livres en utilisant **ASP.NET Core** comme backend. Il propose des fonctionnalités pour ajouter, mettre à jour, supprimer et rechercher des livres.


## **📂 Architecture du projet**

### **Structure des dossiers**

![ArchitectureBackend](https://github.com/user-attachments/assets/dcccc07c-7aae-4220-aa1c-d74831678b7a)

#### Controllers/

Contient les fichiers responsables de gérer les requêtes HTTP.
Chaque fichier correspond à un endpoint de l'API.
Exemple : BookController.cs contient les endpoints pour Ajouter, Modifier, Supprimer et Rechercher des livres.
#### DTOs/

Contient les objets servant à transférer les données entre le client et le serveur.  

CreateBookDto : Pour créer un livre.  
UpdateBookDto : Pour mettre à jour les informations d'un livre.  
BookDto : Pour renvoyer un livre au format JSON.  

#### Models/  
  
Contient les classes représentant la structure des données stockées dans la base de données.  
Book.cs : Décrit les champs comme Title, Author, Description et ImageCover.  
  
#### Services/  

Contient la logique métier de l'application.
BookService.cs : Implémente les opérations pour gérer les livres (ajouter, rechercher, etc.).  
BookMappingService.cs : Gère la conversion entre les DTOs et les modèles.  
  
#### Data/

Contient le contexte Entity Framework Core pour gérer les accès à la base de données.
BookContext.cs : Permet de gérer les tables et de faire des requêtes SQL via le code.
  
#### wwwroot/uploads/

Contient les fichiers statiques, notamment les images uploadées par les utilisateurs.
  
#### appsettings.json

Fichier de configuration pour les chaînes de connexion à la base de données et autres paramètres.
  
#### Program.cs

Point d'entrée principal qui configure et démarre l'application.
 
#### Startup.cs

Configure les services et middleware nécessaires pour le bon fonctionnement de l'application.
