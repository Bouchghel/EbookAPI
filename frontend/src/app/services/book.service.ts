import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Book} from '../models/book.model';
import {environment} from '../../environement/environment'; // URL API

@Injectable({
  providedIn: 'root',
})
export class BookService {
  private apiUrl = `${environment.apiUrl}/Book`; // API URL

  constructor(private http: HttpClient) {}

  // Recherche de livres par titre ou auteur
  searchBooks(search: string): Observable<Book[]> {
    // Si le terme de recherche est vide, ne pas inclure le paramètre search dans l'URL
    const params = search ? new HttpParams().set('search', search) : undefined;
    return this.http.get<Book[]>(`${this.apiUrl}/search`, { params });
  }


  // Ajouter un livre
  addBook(formData: FormData): Observable<Book> {
    return this.http.post<Book>(this.apiUrl, formData);
  }

  // Supprimer un livre
  deleteBook(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  // Ordonner les livres par titre
  orderBooksByTitle(): Observable<Book[]> {
    return this.http.get<Book[]>(`${this.apiUrl}`);
  }

  getBookById(id: number): Observable<Book> {
    return this.http.get<Book>(`${this.apiUrl}/${id}`);
  }

  updateBook(id: number, updatedBook: FormData): Observable<void> {
    // Ici, nous recevons directement le FormData
    return this.http.put<void>(`${this.apiUrl}/${id}`, updatedBook);
  }
}
