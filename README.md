# 📚 **Biblothèque Application**

Ce projet représente une **API REST** pour la gestion d'une bibliothèque de livres en utilisant **ASP.NET Core** comme backend. Il propose des fonctionnalités pour ajouter, mettre à jour, supprimer et rechercher des livres.


## **📂 Architecture du projet**

### **Structure des dossiers**
PLivres/ │ ├── Controllers/ # Contrôleurs API pour gérer les requêtes HTTP │ ├── BookController.cs │ ├── DTOs/ # Data Transfer Objects (DTOs) │ ├── CreateBookDto.cs │ ├── UpdateBookDto.cs │ └── BookDto.cs │ ├── Models/ # Entités du modèle de base de données │ └── Book.cs │ ├── Services/ # Services métier │ ├── IBookService.cs │ ├── BookService.cs │ ├── IBookMappingService.cs │ └── BookMappingService.cs │ ├── Data/ # Contexte de la base de données │ └── BookContext.cs │ ├── wwwroot/ # Répertoire public pour stocker les images │ └── uploads/ # Répertoire des images uploadées │ ├── appsettings.json # Configuration de l'application ├── Program.cs # Point d'entrée principal └── Startup.cs # Configuration des services
