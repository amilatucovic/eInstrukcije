import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CitiesService } from '../services/auth-services/services/cities.service';
import { City } from '../models/city.model';
import { TranslateService } from '@ngx-translate/core';
import { TranslateModule } from '@ngx-translate/core';
import { MyAppUserService } from '../services/auth-services/services/myappuser.service';
import { HttpErrorResponse } from '@angular/common/http';
import { debounceTime, distinctUntilChanged, filter } from 'rxjs/operators';
import { Subscription } from 'rxjs'; // Dodano za upravljanje pretplatama

declare var window: any; //za pristup globalnim objektima jer koristimo bootstrap

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, TranslateModule]
})
export class RegistrationComponent implements OnInit, OnDestroy {

  UserForm: FormGroup;
  passwordStrength: string = '';
  passwordStrengthClass: string = '';
  passwordMatch: string = '';
  passwordMatchClass: string = '';
  cities: City[] = [];
  gradeOptions: { label: string, value: string }[] = [];
  username: string = '';
  usernameTaken: boolean = false;
  usernameErrorMessage: string = '';

  private langChangeSubscription: Subscription | undefined;
  private usernameCheckSubscription: Subscription | undefined;

  constructor(private http: HttpClient, private router: Router, private citiesService: CitiesService, private translate: TranslateService, private myAppUserService: MyAppUserService) {
    this.UserForm = new FormGroup({
      firstName: new FormControl('', [Validators.required, Validators.pattern(/^\p{L}(?:['\-]?\p{L})*$/u)]),
      lastName: new FormControl('', [Validators.required, Validators.pattern(/^\p{L}(?:['\-]?\p{L})*$/u)]),
      email: new FormControl('', [Validators.required, Validators.email]),
      username: new FormControl('', [Validators.required, Validators.minLength(5)]),
      password: new FormControl('', [Validators.required, Validators.minLength(8)]),
      cityId: new FormControl('', Validators.required),
      educationLevel: new FormControl('', Validators.required),
      passwordConfirmation: new FormControl('', [Validators.required, Validators.minLength(8)]),
      preferredMode: new FormControl('', Validators.required),
      grade: new FormControl({ value: '', disabled: true }, Validators.required),
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

    this.UserForm.get('educationLevel')?.valueChanges.subscribe(level => {
      if (level === 0) {  // Osnovna škola
        this.gradeOptions = [
          { label: 'grades.first', value: 'Prvi' },
          { label: 'grades.second', value: 'Drugi' },
          { label: 'grades.third', value: 'Treći' },
          { label: 'grades.fourth', value: 'Četvrti' },
          { label: 'grades.fifth', value: 'Peti' },
          { label: 'grades.sixth', value: 'Šesti' },
          { label: 'grades.seventh', value: 'Sedmi' },
          { label: 'grades.eight', value: 'Osmi' },
          { label: 'grades.ninth', value: 'Deveti' }
        ];
        this.UserForm.get('grade')?.enable();
      } else if (level === 1) { // Srednja škola
        this.gradeOptions = [
          { label: 'grades.first', value: 'Prvi' },
          { label: 'grades.second', value: 'Drugi' },
          { label: 'grades.third', value: 'Treći' },
          { label: 'grades.fourth', value: 'Četvrti' }
        ];
        this.UserForm.get('grade')?.enable();
      } else {
        this.gradeOptions = [];
        this.UserForm.get('grade')?.disable();
      }
      this.UserForm.get('grade')?.reset('');
    });

    this.UserForm.get('grade')?.disable();


    this.UserForm.get('username')?.valueChanges.pipe(
      debounceTime(500),
      distinctUntilChanged(),
      filter(username => {
        const usernameControl = this.UserForm.get('username');
        return username && username.length >= 5 && usernameControl?.valid;
      })
    ).subscribe(username => {
      this.usernameTaken = false;
      this.usernameErrorMessage = '';

      this.myAppUserService.checkUsernameAvailability(username).subscribe({
        next: (res) => {
          this.usernameTaken = !res.available;
          if (this.usernameTaken) {
            this.usernameErrorMessage = this.translate.instant("errors.username");
          }
        },
        error: (err: HttpErrorResponse) => {
          console.error('Greška pri provjeri korisničkog imena (backend je vratio grešku):', err);
          this.usernameTaken = true;
          this.usernameErrorMessage = this.translate.instant("errors.username");
        }
      });
    });

    this.langChangeSubscription = this.translate.onLangChange.subscribe(() => {
      if (this.usernameTaken) {
        this.usernameErrorMessage = this.translate.instant("errors.username");
      }
    });
  }

  ngOnDestroy(): void {
    if (this.langChangeSubscription) {
      this.langChangeSubscription.unsubscribe();
    }
    if (this.usernameCheckSubscription) {
      this.usernameCheckSubscription.unsubscribe();
    }
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
    if (this.usernameTaken) {
      return;
    }
    if (this.UserForm.valid) {
      const formValues = this.UserForm.value;
      this.http.post('http://localhost:7000/api/StudentEndpoints', formValues).subscribe({
        next: (response: any) => {
          this.router.navigate(['/student-dashboard']);
        },
        error: (error) => {
          console.log(error);
        }
      });
    }
    else {
      console.log('Forma nije validna!');
      return
    }
  }
}