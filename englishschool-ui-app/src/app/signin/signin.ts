import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-signin',
  standalone: true,
  imports: [RouterLink, FormsModule],
  templateUrl: './signin.html',
  styleUrl: './signin.css',
})
export class SigninComponent {
  email = '';
  password = '';
  rememberMe = true;
  showPassword = false;

  onGoogleSignIn(): void {
    console.log('Google Sign In clicked');
  }

  onAppleSignIn(): void {
    console.log('Apple Sign In clicked');
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  onSubmit(): void {
    console.log('Sign In submitted', { email: this.email, password: this.password, rememberMe: this.rememberMe });
  }
}
