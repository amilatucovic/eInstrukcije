import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { MyAuthService } from '../services/auth-services/my-auth.service';
import { MyAppUser } from '../models/myAppUser.model';
import { Student } from '../models/student.model';
import {ChatService} from '../services/auth-services/services/chat.service';


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
    private router: Router,
    private myAuth: MyAuthService,
    private chatService: ChatService
  ) {
    // Initialize the login form with validation
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]], // Username field
      password: ['', [Validators.required, Validators.minLength(6)]] // Password field
    });
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      this.errorMessage = 'Popunite sva polja ispravno.';
      return;
    }

    const loginRequest = {
      Username: this.loginForm.value.username,
      Password: this.loginForm.value.password
    };

    this.http.post('http://localhost:7000/api/Login/login', loginRequest)
      .subscribe({
        next: (response: any) => {
          // Čuvanje tokena i tutorId-a u localStorage
          localStorage.setItem('accessToken', response.token);
          this.chatService.startConnection(response.token);
          localStorage.setItem('refreshToken', response.refreshToken);

          const user = new MyAppUser(
            response.id,
            response.city,
            response.cityId,
            response.firstName,
            response.lastName,
            response.email,
            response.username,
            response.phoneNumber,
            response.educationLevel,
            response.preferredMode,
            response.grade,
            response.role
          );

          this.myAuth.setLoggedInUser(user);
          console.log('Logged in user:', user);
          console.log('From authService:', this.myAuth.getLoggedInUser());
          if (response.role === 'Tutor' && response.tutorId) {
            localStorage.setItem('tutorId', response.tutorId.toString());
          }

          if (response.role === 'Student' && response.studentId) {
            localStorage.setItem('studentId', response.studentId.toString());
          }

          // Navigacija na odgovarajući dashboard
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
          this.errorMessage = error.error || 'Neispravni podaci za prijavu.';
        }
      });
  }


}
