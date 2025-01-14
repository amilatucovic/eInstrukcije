import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

declare var window: any; //za pristup globalnim objektima jer koristimo bootstrap

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule]
})
export class RegistrationComponent {

  UserForm: FormGroup;
  passwordStrength: string = '';
  passwordStrengthClass: string = '';
  passwordMatch: string = '';
  passwordMatchClass: string = '';

  constructor(private http: HttpClient, private router: Router) {
    this.UserForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(8)]),
      city: new FormControl('', Validators.required),
      educationalLevel: new FormControl('', Validators.required),
      passwordConfirmation: new FormControl('', [Validators.required, Validators.minLength(8)]),
      preferredMode: new FormControl('', Validators.required),
      grade: new FormControl('', [Validators.required, Validators.pattern('^[A-Za-z]+$')]),
      age: new FormControl('', [Validators.required, Validators.min(6), Validators.max(20), Validators.pattern('^[0-9]+$')]),
      gender: new FormControl('', Validators.required),
      phoneNumber: new FormControl('', [Validators.required, Validators.minLength(9), Validators.maxLength(13), Validators.pattern('^[0-9]+$')]),
    })
  }

  checkPasswordMatch() {
    return this.UserForm.get("password")?.value === this.UserForm.get("passwordConfirmation")?.value;
  }

  checkPasswordStrength() {
    const password = this.UserForm.get('password')?.value;
    if (!password) {
      this.passwordStrength = '';
      this.passwordStrengthClass = '';
      return;
    }

    const lowerCasePattern = /[a-z]/;
    const upperCasePattern = /[A-Z]/;
    const numberPattern = /\d/;
    const specialCharPattern = /[!@#$%^&*(),.?":{}|<>]/;

    if (password.length < 8) {
      this.passwordStrength = 'Slaba lozinka';
      this.passwordStrengthClass = 'text-danger small';
    } else if (
      lowerCasePattern.test(password) &&
      upperCasePattern.test(password) &&
      numberPattern.test(password) &&
      specialCharPattern.test(password)
    ) {
      this.passwordStrength = 'Jaka lozinka';
      this.passwordStrengthClass = 'text-success small';
    } else {
      this.passwordStrength = 'Srednje jaka lozinka';
      this.passwordStrengthClass = 'text-warning small';
    }
  }

  registriraj() {
    if (!this.checkPasswordMatch()) {
      //console.log("Šifre se ne poklapaju");
      this.passwordMatch = 'Šifre se ne poklapaju!';
      this.passwordMatchClass = 'text-danger small';
      return;
    }
    if (this.UserForm.valid) {
      const formValues = this.UserForm.value;
      // console.log('Forma je validna:', this.UserForm.value);
      this.http.post('http://localhost:7000/api/StudentEndpoints', formValues)
        .subscribe({
          next: (response: any) => {
            // const successModal = new window.bootstrap.Modal(document.getElementById('successModal'));
            // successModal.show();
            this.router.navigate(['/student-dashboard']);
          },
          error: (error) => {
            //const errorModal = new window.bootstrap.Modal(document.getElementById('errorModal'));
            //errorModal.show();
          }
        });
    }
    else {
      console.log('Forma nije validna!');
      return
    }
  }
}