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

## **🛠️ Technologies utilisées**

- **ASP.NET Core 8.0** : Framework pour le développement de l'API REST.
- **Entity Framework Core** : ORM pour la gestion de la base de données.
- **SQL Server** : Base de données utilisée.
- **Swagger** : Documentation interactive de l'API.
- **C#** : Langage principal.
- **Frontend** :Angular.

## **🚀 Fonctionnalités**

1. Ajouter un livre.
2. Mettre à jour les informations d'un livre existant.
3. Supprimer un livre.
4. Récupérer la liste complète des livres(par ordre)
5. Rechercher des livres par titre ou auteur.
6. Gestion des uploads d'image pour la couverture d'un livre.

## **🧪 Tester l'API avec Swagger**

Swagger permet de tester directement les endpoints de votre API.  

### Ajouter :
![AjouterUnLivre](https://github.com/user-attachments/assets/8442c2fa-59a9-43f9-be2b-5b506d55457a)  
### Modifier :  
![Put](https://github.com/user-attachments/assets/339b1b0c-1207-4597-a4e6-9a4d000ca620)  
### Supprimer :  
![Delete](https://github.com/user-attachments/assets/9e5de054-043a-4b34-8ef2-ed12f7dda47c)  
### Récupérer la liste complète des livres(par ordre) : 
![getParOrdre](https://github.com/user-attachments/assets/9d620f9a-1005-40c2-8250-16ac7c4065cc)  
### Rechercher des livres par titre ou auteur  
![searchByTitleOrAuthor](https://github.com/user-attachments/assets/56afcd96-1dd7-49ac-988b-8876b6a8d462)

## **🖼️ Screenshots de l'application
### Liste des livres (par ordre) :  
![ListeDesLivres](https://github.com/user-attachments/assets/e7ba2b11-fa21-4421-b8e9-2d7fae12a2f6)  
### Ajouter un Livre :  
![AjouterUnLivre](https://github.com/user-attachments/assets/df008830-bcf7-4286-8bf7-8c634edcfd6e)  
### Modifier un livre :  
![modifierUnLivre](https://github.com/user-attachments/assets/7304d239-8214-4ba5-8ed8-d9546c2a78b5)  
### Supprimer un Livre :  
![SupprimmerLivre](https://github.com/user-attachments/assets/e95e135f-a1d2-4284-bc78-ff8019a32647)  
### Rechercher des livres par titre ou auteur 
![RechercherParTitreAuteur](https://github.com/user-attachments/assets/1f41b13d-41e0-4b9f-80ec-220fb70d56f1)  

# 🚀 Instructions d'installation

## Backend : 

1. **Cloner le projet** :
    ```bash
    git clone https://github.com/Bouchghel/EbookAPI.git
    cd backend
    ```

2. **Restaurer les packages** :
    ```bash
    dotnet restore
    ```

3. **Migration de la base de données** :
    - Ouvrez la **Package Manager Console** :
      ```bash
      addMigration BookMigration
      update-database  
      ```

4. **Démarrer le projet Backend** :

## Frontend :

1. **Cloner le projet frontend** :
    ```bash
    cd frontend
    ```

2. **Installer les dépendances** :
    ```bash
    npm install
    ```

3. **Démarrer le projet frontend** :
    ```bash
    ng serve
    ```


