import { Component, OnInit } from '@angular/core';
import { City } from '../../../models/city.model';
import { CitiesService } from '../../../services/auth-services/services/cities.service';
import { MyAppUserService } from '../../../services/auth-services/services/myappuser.service';
import { MyAuthService } from '../../../services/auth-services/my-auth.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { StudentService } from '../../../services/auth-services/services/students.service';
import { StudentUpdateDTO } from '../../../models/studentUpdate.model';
import { Student } from '../../../models/student.model';
import { AbstractControl, AsyncValidatorFn, ValidationErrors } from '@angular/forms';
import { map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

@Component({
  selector: 'app-student-acc-settings-tab',
  templateUrl: './student-acc-settings-tab.component.html',
  styleUrl: './student-acc-settings-tab.component.css',
})
export class StudentAccSettingsTabComponent implements OnInit {

  cities: City[] = [];
  user: Student | null = null;
  userCityName: string | null = null;
  profileImageUrl: string | ArrayBuffer | null = null;
  hovering = false;
  UserForm: FormGroup;

  gradeOptions: { label: string, value: string }[] = [];
  username: string = '';
  usernameTaken: boolean = false;
  usernameErrorMessage: string = '';
  originalUsername: string = '';

  successMessage: string = '';
  showSuccess: boolean = false;

  constructor(private cityService: CitiesService, private myAuth: MyAuthService, private myAppUserService: MyAppUserService, private http: HttpClient, private router: Router, private studentService: StudentService) {
    this.UserForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      username: new FormControl('', [Validators.required, Validators.minLength(5)], [this.usernameAvailabilityValidator()]),
      educationLevel: new FormControl('', Validators.required),
      preferredMode: new FormControl('', Validators.required),
      grade: new FormControl('', Validators.required),
      phoneNumber: new FormControl('', [Validators.required, Validators.minLength(9), Validators.maxLength(13), Validators.pattern('^[0-9]+$')]),
      city: new FormControl('', Validators.required)
    });
  }

  ngOnInit(): void {
    this.cityService.getCities().subscribe({
      next: (data) => this.cities = data,
      error: (err) => console.error("Greska pri ucitavanju gradova", err)
    });

    const savedImage = localStorage.getItem('studentProfileImage');
    if (savedImage) {
      this.profileImageUrl = savedImage;
    }
    this.user = this.myAuth.getLoggedInUser() as Student;

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

    const usernameControl = this.UserForm.get('username');
    if (usernameControl && this.originalUsername === usernameControl.value) {
      usernameControl.clearAsyncValidators(); // jer nije promijenjen
    } else {
      usernameControl?.setAsyncValidators(this.usernameAvailabilityValidator());
    }
    usernameControl?.updateValueAndValidity();


    if (this.user) {
      this.originalUsername = this.user.myAppUser.username;
      console.log("User na ucitavanju:", this.user);
      this.UserForm.patchValue({
        firstName: this.user.myAppUser.firstName,
        lastName: this.user.myAppUser.lastName,
        email: this.user.myAppUser.email,
        username: this.user.myAppUser.username,
        phoneNumber: this.user.myAppUser.phoneNumber,
        educationLevel: this.user.educationLevel,
        preferredMode: this.user.preferredMode,
        grade: this.user.grade,
        city: this.user.myAppUser.cityId
      });
    }

  }

  save() {
    if (this.usernameTaken) return;
    if (this.UserForm.valid && this.user) {
      const podaci: StudentUpdateDTO = {
        id: this.user.id,
        email: this.UserForm.value.email,
        cityId: this.UserForm.value.city,
        grade: this.UserForm.value.grade,
        educationLevel: this.UserForm.value.educationLevel,
        phoneNumber: this.UserForm.value.phoneNumber,
        username: this.UserForm.value.username,
        firstName: this.UserForm.value.firstName,
        lastName: this.UserForm.value.lastName,
        preferredMode: this.UserForm.value.preferredMode,
      };

      this.studentService.update(this.user.id, podaci).subscribe({
        next: (updatedUser) => {
          console.log("User na update:", updatedUser)
          this.myAuth.setLoggedInUser(updatedUser);
          this.UserForm.patchValue({
            firstName: updatedUser.myAppUser.firstName,
            lastName: updatedUser.myAppUser.lastName,
            email: updatedUser.myAppUser.email,
            username: updatedUser.myAppUser.username,
            phoneNumber: updatedUser.myAppUser.phoneNumber,
            educationLevel: updatedUser.educationLevel,
            preferredMode: updatedUser.preferredMode,
            grade: updatedUser.grade,
            city: updatedUser.myAppUser.cityId
          });
          this.successMessage = 'Podaci su uspješno sačuvani!';
          this.showSuccess = true;

          setTimeout(() => {
            this.showSuccess = false;
          }, 3000);
        },
        error: (error) => {
          console.error('Greška pri ažuriranju studenta', error);
        }
      });
    }
  }

  usernameAvailabilityValidator(): AsyncValidatorFn {
    return (control: AbstractControl): any => {
      const username = control.value;
      if (!username || username.length < 5) {
        return of(null); // Ako je prazno ili prekratko, ne validiramo
      }

      return this.myAppUserService.checkUsernameAvailability(username).pipe(
        map(response => {
          return response.available ? null : { usernameTaken: true };
        }),
        catchError(() => of({ usernameTaken: true })) // Ako API padne, tretiraj kao zauzeto
      );
    };
  }

  onFileSelected(event: Event): void {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        this.profileImageUrl = reader.result;
        localStorage.setItem('studentProfileImage', this.profileImageUrl as string);
      };
      reader.readAsDataURL(file);
    }
  }

  removeProfileImage(event: MouseEvent): void {
    event.stopPropagation();
    this.profileImageUrl = null;
    localStorage.removeItem('studentProfileImage');
  }
}

