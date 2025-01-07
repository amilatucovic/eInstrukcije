import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-tutor-registration',
  templateUrl: './tutor-registration.component.html',
  styleUrls: ['./tutor-registration.component.css']
})
export class TutorRegistrationComponent implements OnInit {
  registrationForm: FormGroup;
  cities: string[] = []; // Popuni iz baze podataka
  passwordStrength: string = '';
  passwordStrengthClass: string = '';
  activeStep: number = 1; // Aktivni korak (1 - Step 1, 2 - Step 2, 3 - Step 3)
  steps: string[] = ['Osnovne informacije', 'Dodatne informacije', 'Korisnički podaci'];

  constructor(private fb: FormBuilder) {
    this.registrationForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: ['', Validators.required],
      city: ['', Validators.required],
      age: ['', [Validators.required, Validators.min(18)]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.required],
      profilePicture: [null, Validators.required],
      qualifications: ['', Validators.required],
      experience: ['', Validators.required],
      availability: ['', Validators.required],
      cancellationPolicy: ['', Validators.required],
      rate: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    }, { validator: this.passwordMatchValidator });
  }

  ngOnInit(): void {
    // Učitaj gradove iz baze podataka
    // this.cities = this.loadCities();
  }

  onFileChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.registrationForm.patchValue({ profilePicture: file });
    }
  }

  passwordMatchValidator(form: FormGroup) {
    return form.get('password')!.value === form.get('confirmPassword')!.value
      ? null : { mismatch: true };
  }

  checkPasswordStrength() {
    const password = this.registrationForm.get('password')!.value;
    if (!password) {
      this.passwordStrength = '';
      return;
    }
    if (password.length < 8) {
      this.passwordStrength = 'Slaba lozinka';
      this.passwordStrengthClass = 'text-danger';
    } else if (password.length < 10) {
      this.passwordStrength = 'Srednje jaka lozinka';
      this.passwordStrengthClass = 'text-warning';
    } else {
      this.passwordStrength = 'Snažna lozinka';
      this.passwordStrengthClass = 'text-success';
    }
  }

  onSubmit() {
    if (this.registrationForm.valid) {
      console.log('Forma je validna:', this.registrationForm.value);
      // Pošaljite podatke na backend
    } else {
      console.log('Forma nije validna!');
    }
  }

  setActiveStep(step: number) {
    this.activeStep = step;
  }
}
