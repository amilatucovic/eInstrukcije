import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {
    // Initialize the login form with validation
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]], // Username field
      password: ['', [Validators.required, Validators.minLength(6)]] // Password field
    });
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      // If the form is invalid, show an error message
      this.errorMessage = 'Popunite sva polja ispravno.';
      return;
    }

    // Construct the loginRequest object with the required properties for the backend
    const loginRequest = {
      Username: this.loginForm.value.username,  // Matches backend DTO property
      Password: this.loginForm.value.password   // Matches backend DTO property
    };

    // Send the loginRequest to the backend API
    this.http.post('http://localhost:7000/api/Login/login', loginRequest)
      .subscribe({
        next: (response: any) => {
          // Store the token and refresh token in localStorage upon successful login
          localStorage.setItem('accessToken', response.token);
          localStorage.setItem('refreshToken', response.refreshToken);

          // Navigate based on the role of the user
          if (response.role === 'Admin') {
            this.router.navigate(['/admin']);
          } else if (response.role === 'Tutor') {
            this.router.navigate(['/tutor-dashboard']);
          } else if (response.role === 'Student') {
            this.router.navigate(['/student-dashboard']);
          } else {
            this.router.navigate(['/']);
          }
        },
        error: (error) => {
          // If an error occurs, display the error message
          this.errorMessage = error.error || 'Neispravni podaci za prijavu.';
        }
      });
  }
}
