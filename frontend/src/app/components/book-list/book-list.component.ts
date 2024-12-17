import { Component, OnInit } from '@angular/core';
import {Book} from '../../models/book.model';
import {BookService} from '../../services/book.service';
import {FormsModule} from '@angular/forms';
import {NgForOf, NgIf} from '@angular/common';
import {Router} from '@angular/router';

@Component({
  selector: 'app-book-list',
  standalone: true,
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css'],
  imports: [
    FormsModule, NgIf, NgForOf
  ]
})
export class BookListComponent implements OnInit {
  books: Book[] = [];
  searchTerm: string = '';
  newBook: Book = { id: 0, title: '', author: '', description: '', imageCover: null }; // Ensure imageCover is null initially
  errorMessage: string | null = null;

  constructor(private bookService: BookService,private router : Router) {}

  ngOnInit(): void {
    this.orderBooks();
  }



  onImageSelect(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.newBook.imageCover = file;
    }
  }

  addBook(): void {
    const formData = new FormData();
    formData.append('title', this.newBook.title);
    formData.append('author', this.newBook.author);
    formData.append('description', this.newBook.description);

    // Check if an image is selected
    if (this.newBook.imageCover instanceof File) {
      formData.append('imageCover', this.newBook.imageCover);
    } else {
      console.error('No valid image selected');
    }

    // Call the service to add the book
    this.bookService.addBook(formData).subscribe(
      (book) => {
        // On success, update the list of books and add the newly created book
        this.books.push(book);

        // Reset form
        this.newBook = { id: 0, title: '', author: '', description: '', imageCover: null };

      },
      (error) => {
        this.errorMessage = 'Error adding book';
      }
    );
  }



  deleteBook(id: number): void {
    if (confirm('Are you sure you want to delete this book?')) {
      this.bookService.deleteBook(id).subscribe(() => {
        this.books = this.books.filter((book) => book.id !== id);
      });
    }
  }

  orderBooks(): void {
    this.bookService.orderBooksByTitle().subscribe((books) => {
      this.books = books;
    });
  }

  // Recherche de livres
  searchBooks(): void {
    const searchQuery = this.searchTerm.trim(); // Supprimer les espaces inutiles

    if (!searchQuery) {
      // Si la recherche est vide, réinitialiser et retourner la liste ordonnée
      this.orderBooks();
    } else {
      // Sinon, effectuer la recherche
      this.bookService.searchBooks(searchQuery).subscribe({
        next: (data) => {
          this.books = data;
          this.errorMessage = null;
        },
        error: (err) => {
          this.errorMessage = 'An error occurred while fetching books.';
        },
      });
    }
  }




  editBook(book: Book) {
    this.router.navigate([`/edit-book/${book.id}`]);
  }

  onSearchChange(event: Event): void {
    const input = event.target as HTMLInputElement;  // Cast explicite de l'élément target vers un HTMLInputElement
    if (input) {
      this.searchTerm = input.value.trim();
    } else {
      this.searchTerm = '';  // Réinitialise searchTerm si aucune valeur
    }

    if (!this.searchTerm) {
      this.orderBooks();  // Si la recherche est vide, afficher tous les livres
    }
  }


}
