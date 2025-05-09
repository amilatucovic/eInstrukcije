import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CitiesService } from '../services/auth-services/services/cities.service';
import { City } from '../models/city.model';
import { TranslateService } from '@ngx-translate/core';
import { TranslateModule } from '@ngx-translate/core';

declare var window: any; //za pristup globalnim objektima jer koristimo bootstrap

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, TranslateModule]
})
export class RegistrationComponent implements OnInit {

  UserForm: FormGroup;
  passwordStrength: string = '';
  passwordStrengthClass: string = '';
  passwordMatch: string = '';
  passwordMatchClass: string = '';
  cities: City[] = [];

  constructor(private http: HttpClient, private router: Router, private citiesService: CitiesService, private translate: TranslateService) {
    this.UserForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      username: new FormControl('', [Validators.required, Validators.minLength(5)]),
      password: new FormControl('', [Validators.required, Validators.minLength(8)]),
      cityId: new FormControl('', Validators.required),
      educationalLevel: new FormControl('', Validators.required),
      passwordConfirmation: new FormControl('', [Validators.required, Validators.minLength(8)]),
      preferredMode: new FormControl('', Validators.required),
      grade: new FormControl('', [Validators.required, Validators.pattern('^[A-Za-z]+$')]),
      age: new FormControl('', [Validators.required, Validators.min(6), Validators.max(20), Validators.pattern('^[0-9]+$')]),
      gender: new FormControl('', Validators.required),
      phoneNumber: new FormControl('', [Validators.required, Validators.minLength(9), Validators.maxLength(13), Validators.pattern('^[0-9]+$')]),
    });

    this.translate.setDefaultLang('bs');
  }

  switchLanguage(lang: string) {
    this.translate.use(lang);
  }

  ngOnInit(): void {
    this.citiesService.getCities().subscribe(
      (data) => {
        this.cities = data.map(cityData => new City(cityData.id, cityData.name, cityData.postalCode));
        console.log(this.cities);
      },
      (error) => {
        console.error('Greška pri učitavanju gradova', error);
      }
    );

    this.UserForm.get('password')?.valueChanges.subscribe(() => {
      this.checkPasswordMatch();
    });

    this.UserForm.get('passwordConfirmation')?.valueChanges.subscribe(() => {
      this.checkPasswordMatch();
    });
  }

  checkPasswordMatch(): boolean {
    const matches = this.UserForm.get("password")?.value === this.UserForm.get("passwordConfirmation")?.value;
    if (matches) {
      this.passwordMatch = '';
      this.passwordMatchClass = '';
    }
    return matches;
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
      this.passwordStrength = this.translate.instant('form.passwordStrength.weak');
      this.passwordStrengthClass = 'text-danger';
    } else if (
      lowerCasePattern.test(password) &&
      upperCasePattern.test(password) &&
      numberPattern.test(password) &&
      specialCharPattern.test(password)
    ) {
      this.passwordStrength = this.translate.instant('form.passwordStrength.strong');
      this.passwordStrengthClass = 'text-success';
    } else {
      this.passwordStrength = this.translate.instant('form.passwordStrength.medium');
      this.passwordStrengthClass = 'text-warning';
    }
  }

  registriraj() {
    this.UserForm.markAllAsTouched();

    console.log(this.checkPasswordMatch())
    if (!this.checkPasswordMatch()) {
      this.passwordMatch = this.translate.instant('form.passwordMatch');
      this.passwordMatchClass = 'text-danger';
      return;
    }
    if (this.UserForm.valid) {
      const formValues = this.UserForm.value;
      console.log('Forma je validna:', this.UserForm.value);
      this.http.post('http://localhost:7000/api/StudentEndpoints', formValues).subscribe({
        next: (response: any) => {
          this.router.navigate(['/student-dashboard']);
        },
        error: (error) => {
          console.log(error.error)
        }
      });
    }
    else {
      console.log('Forma nije validna!');
      return
    }
  }
}