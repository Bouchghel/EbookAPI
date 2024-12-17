import { Routes } from '@angular/router';
import {BookEditComponent} from './components/book-edit/book-edit.component';
import {BookListComponent} from './components/book-list/book-list.component';

export const routes: Routes =  [
  { path: 'books', component: BookListComponent }, // Liste des livres
  { path: 'edit-book/:id', component: BookEditComponent }, // Page d'édition du livre
  { path: '', redirectTo: '/books', pathMatch: 'full' } // Route par défaut
];
