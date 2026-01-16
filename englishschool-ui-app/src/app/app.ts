import { Component, signal } from '@angular/core';
import { RouterOutlet, RouterModule } from '@angular/router';
import { CartIcon } from './cart-icon/cart-icon';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterModule, CartIcon],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('englishschool-ui-app');
}
