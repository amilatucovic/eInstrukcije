import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-tutor-registration',
  templateUrl: './tutor-registration.component.html',
  styleUrls: ['./tutor-registration.component.css']
})
export class TutorRegistrationComponent implements OnInit {
  registrationForm: FormGroup;
  cities: string[] = []; // Popuniti iz baze podataka
  passwordStrength: string = '';
  passwordStrengthClass: string = '';
  activeStep: number = 1; // Aktivni korak (1 - Step 1, 2 - Step 2, 3 - Step 3)
  steps: string[] = ['Osnovne informacije', 'Dodatne informacije', 'Korisnički podaci'];
  preview: string | ArrayBuffer | null = null; // Pregled slike

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
    // Učitati gradove iz baze podataka
  }

  // Validacija za podudaranje lozinki
  passwordMatchValidator(form: FormGroup) {
    return form.get('password')!.value === form.get('confirmPassword')!.value
      ? null : { mismatch: true };
  }

  // Provjera jačine lozinke
  checkPasswordStrength() {
    const password = this.registrationForm.get('password')!.value;
    if (!password) {
      this.passwordStrength = '';
      return;
    }
    if (password.length < 8) {
      this.passwordStrength = 'Slaba lozinka';
      this.passwordStrengthClass = 'text-danger';
    } else if (password.length < 12) {
      this.passwordStrength = 'Srednje jaka lozinka';
      this.passwordStrengthClass = 'text-warning';
    } else {
      this.passwordStrength = 'Snažna lozinka';
      this.passwordStrengthClass = 'text-success';
    }
  }

  // Slanje podataka iz forme
  onSubmit() {
    if (this.registrationForm.valid) {
      console.log('Forma je validna:', this.registrationForm.value);
      // Logika za slanje na backend
    } else {
      console.log('Forma nije validna!');
    }
  }

  // Postavljanje aktivnog koraka
  setActiveStep(step: number) {
    this.activeStep = step;
  }

  // Obrada dodavanja datoteke
  onFileChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.handleFile(file);
    }
  }

  // Obrada drag-and-drop događaja
  onDragOver(event: DragEvent) {
    event.preventDefault();
  }

  onDrop(event: DragEvent) {
    event.preventDefault();
    if (event.dataTransfer && event.dataTransfer.files.length > 0) {
      const file = event.dataTransfer.files[0];
      this.handleFile(file);
      event.dataTransfer.clearData(); // Očistite podatke iz drag-and-drop
    }
  }

  removeImage(): void {
    this.preview = null;
    this.registrationForm.patchValue({ profilePicture: null });
  }

  // Pomoćna funkcija za obradu datoteke
  private handleFile(file: File) {
    if (!file.type.startsWith('image/')) {
      console.error('Neispravan tip datoteke');
      return;
    }
    this.registrationForm.patchValue({ profilePicture: file }); // Postavljanje datoteke u formu
    const reader = new FileReader();
    reader.onload = () => {
      this.preview = reader.result; // Generisanje pregleda slike
    };
    reader.readAsDataURL(file); // Čitanje datoteke kao Data URL
  }
}
