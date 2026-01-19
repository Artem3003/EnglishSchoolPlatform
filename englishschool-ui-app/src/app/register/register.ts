import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterLink, FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class RegisterComponent {
  name = '';
  email = '';
  password = '';
  repeatPassword = '';
  showPassword = false;
  showRepeatPassword = false;

  onGoogleSignIn(): void {
    console.log('Google Sign In clicked');
  }

  onAppleSignIn(): void {
    console.log('Apple Sign In clicked');
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  toggleRepeatPasswordVisibility(): void {
    this.showRepeatPassword = !this.showRepeatPassword;
  }

  onSubmit(): void {
    console.log('Register submitted', { name: this.name, email: this.email, password: this.password, repeatPassword: this.repeatPassword });
  }
}
