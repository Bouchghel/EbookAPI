import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../../services/book.service';
import { Book } from '../../models/book.model';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-book-edit',
  standalone: true,
  templateUrl: './book-edit.component.html',
  imports: [
    ReactiveFormsModule,
    NgIf
  ],
  styleUrls: ['./book-edit.component.css']
})
export class BookEditComponent implements OnInit {
  editBookForm!: FormGroup;
  book: Book | null = null;
  bookId!: number;
  selectedImage: File | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private bookService: BookService
  ) {}

  ngOnInit(): void {
    // Récupérer l'ID du livre depuis les paramètres de la route
    this.bookId = +this.route.snapshot.paramMap.get('id')!;
    this.createForm();
    this.loadBookDetails();
  }

  // Initialisation du formulaire
  createForm(): void {
    this.editBookForm = this.fb.group({
      title: ['', [Validators.required]],
      author: ['', [Validators.required]],
      description: ['', Validators.required]
    });
  }

  // Charger les détails du livre depuis l'API
  loadBookDetails(): void {
    this.bookService.getBookById(this.bookId).subscribe(
      (book: Book) => {
        this.book = book;

        // Remplir le formulaire avec les valeurs récupérées
        this.editBookForm.patchValue({
          title: book.title,
          author: book.author,
          description: book.description
        });
      },
      (error) => {
        console.error('Erreur lors du chargement des détails du livre', error);
      }
    );
  }

  // Gestion du changement d'image
  onImageChange(event: Event): void {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length > 0) {
      this.selectedImage = target.files[0];
    }
  }

  // Soumission du formulaire
  onSubmit(): void {
    if (this.editBookForm.invalid) {
      return; // Ne pas soumettre si le formulaire est invalide
    }

    // Préparation des données à envoyer sous forme de FormData
    const formData = new FormData();
    formData.append('title', this.editBookForm.value.title);
    formData.append('author', this.editBookForm.value.author);
    formData.append('description', this.editBookForm.value.description);

    if (this.selectedImage) {
      formData.append('ImageCover', this.selectedImage); // Ajout de l'image si sélectionnée
    }

    this.bookService.updateBook(this.bookId, formData).subscribe(
      () => {
        this.router.navigate(['/books']); // Redirection vers la liste des livres après succès
      },
      (error) => {
        console.error('Erreur lors de la mise à jour du livre', error);
        if (error.error?.errors) {
          console.log('Erreurs de validation:', error.error.errors);
        }
      }
    );
  }

  // Annulation de l'édition
  onCancel(): void {
    this.router.navigate(['/books']);
  }
}
