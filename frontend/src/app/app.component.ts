import { Component } from '@angular/core';
import {RouterModule, RouterOutlet} from '@angular/router';
import {BookListComponent} from './components/book-list/book-list.component';
import {HttpClientModule} from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HttpClientModule, RouterOutlet,RouterModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'IntefaceUI';
}
