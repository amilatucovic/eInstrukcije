import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl, AbstractControl, ValidationErrors } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';



@Component({
  selector: 'app-tutor-registration',
  templateUrl: './tutor-registration.component.html',
  styleUrls: ['./tutor-registration.component.css']
})
export class TutorRegistrationComponent implements OnInit {
  registrationForm: FormGroup;
  cities: any[] = []; // Popuniti iz baze podataka
  passwordStrength: string = '';
  passwordStrengthClass: string = '';
  activeStep: number = 1; // Aktivni korak (1 - Step 1, 2 - Step 2, 3 - Step 3)
  steps: string[] = ['Osnovne informacije', 'Dodatne informacije', 'Korisnički podaci'];
  preview: string | ArrayBuffer | null = null; // Pregled slike
  showPassword: boolean=false;
  showConfirmPassword: boolean = false;
  constructor(private fb: FormBuilder, private http: HttpClient, private cdr: ChangeDetectorRef, private router: Router) {
    this.registrationForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: ['', Validators.required],
      city: ['', Validators.required],
      age: ['', [Validators.required, Validators.min(14)]],
      email: ['', [Validators.required, this.emailValidator]],
      phoneNumber: ['', [Validators.required, this.phoneNumberValidator]],
      profilePicture: [null, Validators.required],
      profilePictureBase64: ['', Validators.required], // Polje za Base64 sliku
      qualifications: ['', Validators.required],
      experience: ['', Validators.required],
      availability: ['', Validators.required],
      cancellationPolicy: ['', Validators.required],
      liveAvailability: [false],
      rate: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    }, { validator: this.passwordMatchValidator });
  }

  ngOnInit(): void {
    this.loadCities();
    this.registrationForm.get('username')?.valueChanges.subscribe(() => {
      this.checkUsernameAvailability();
    });
    this.registrationForm.get('password')?.valueChanges.subscribe(() => {
      this.checkPasswordStrengthOnBackend();
    });
    this.registrationForm.get('confirmPassword')?.valueChanges.subscribe(() => {
      this.passwordMatchValidator(this.registrationForm);
    });
  }




  phoneNumberValidator(control: AbstractControl): ValidationErrors | null {
    const phoneNumber = control.value;

    if (!phoneNumber) {
      return null;  // Ako nema vrijednosti, validacija nije potrebna
    }

    // Provjera da broj sadrži samo cifre
    const digitsOnly = /^[0-9]+$/.test(phoneNumber.replace(/\s+/g, ''));  // Uklanja razmake i provjerava samo cifre
    if (!digitsOnly) {
      return { invalidPhoneType: 'Broj telefona može sadržavati samo cifre.' };
    }

    // Regex za dozvoljene formate: "0xx xxx xxx", "0xx xxx xxxx", "+387 xx xxx xxx", "+387 xx xxx xxxx"
    const phoneRegex = /^(0\d{2}\s?\d{3}\s?\d{3,4})$|^(\+387\s?\d{2}\s?\d{3}\s?\d{3,4})$/;

    if (!phoneRegex.test(phoneNumber)) {
      return { invalidPhoneNumber: 'Broj telefona nije u ispravnom formatu.' };
    }

    return null;
  }



  loadCities(): void {
    this.http.get<any>('http://localhost:7000/api/CityEndpoint').subscribe(
      (response) => {
        this.cities = response?.$values || []; // Extract cities from response
      },
      (error) => {
        console.error('Error fetching cities:', error);
      }
    );
  }


  passwordMatchValidator(form: FormGroup) {
    const password = form.get('password')?.value;
    const confirmPasswordControl = form.get('confirmPassword');

    if (!confirmPasswordControl?.dirty) {
      // Ne postavljamo grešku dok korisnik ne započne unos u polju "Potvrdite lozinku"
      return;
    }

    if (password !== confirmPasswordControl?.value) {
      confirmPasswordControl?.setErrors({ passwordMismatch: true });
    } else {
      confirmPasswordControl?.setErrors(null);
    }
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
  isSubmitting=false;
  // Slanje podataka na backend
  onSubmit() {
    if (this.registrationForm.valid) {
      this.isSubmitting = true; // Onemogućavamo dugme
      const registrationData = {
        firstName: this.registrationForm.get('firstName')!.value,
        lastName: this.registrationForm.get('lastName')!.value,
        gender: this.registrationForm.get('gender')!.value,
        cityId: +this.registrationForm.get('city')!.value,
        age: +this.registrationForm.get('age')!.value,
        email: this.registrationForm.get('email')!.value,
        phoneNumber: this.registrationForm.get('phoneNumber')!.value,
        profileImageUrl: this.registrationForm.get('profilePictureBase64')!.value,
        username: this.registrationForm.get('username')!.value,
        password: this.registrationForm.get('password')!.value,
        yearsOfExperience: +this.registrationForm.get('experience')!.value,
        hourlyRate: `${this.registrationForm.get('rate')!.value}KM/h`,
        qualifications: this.registrationForm.get('qualifications')!.value,
        availability: this.registrationForm.get('availability')!.value,
        policy: this.registrationForm.get('cancellationPolicy')!.value,
        isLiveAvailable: this.registrationForm.get('liveAvailability')!.value,
      };

      console.log('Podaci za slanje:', registrationData);

      this.http.post('http://localhost:7000/api/RegistrationEndpoint/register-tutor', registrationData).subscribe(
        (response) => {
          console.log('Registracija uspješna:', response);
          alert('Registracija uspješna!');
          this.isSubmitting = false;
          this.router.navigate(['/tutor-dashboard']); // Preusmeravanje na login
        },
        (error) => {
          console.error('Greška pri registraciji:', error);

          alert('Došlo je do greške prilikom registracije. Pokušajte ponovo.');
          this.isSubmitting = false;
          if (error.status === 401) {
            alert('Nemate dozvolu za ovu radnju.');
          } else {
            alert('Došlo je do greške prilikom registracije. Pokušajte ponovo.');
          }
        }
      );
    } else {
      console.log('Forma nije validna!');
      alert('Molimo vas da popunite sva obavezna polja.');
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
      this.registrationForm.get('profilePicture')?.markAsTouched();
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
    this.registrationForm.patchValue({ profilePicture: null, profilePictureBase64: null });
  }

// Pomoćna funkcija za obradu datoteke
  private handleFile(file: File) {
    if (!file.type.startsWith('image/')) {
      console.error('Neispravan tip datoteke');
      return;
    }
    // Prvo postavite datoteku u formu (ako želite koristiti datoteku)
    this.registrationForm.patchValue({ profilePicture: file });
    const reader = new FileReader();
    reader.onload = () => {
      // Postavite preview slike za prikaz
      this.preview = reader.result;

      // Konverzija datoteke u Base64 string
      const base64Image = reader.result as string;
      const cleanBase64 = base64Image.split(",")[1];
      console.log(cleanBase64);  // Provjeri da li je base64 string ispravan

      // Pohranjivanje Base64 stringa u formu
      this.registrationForm.patchValue({
        profilePictureBase64: cleanBase64
      });
    };
    reader.readAsDataURL(file); // Čitanje datoteke kao Data URL
  }

  emailValidator(control: FormControl): ValidationErrors | null {
    const email = control.value;

    if (!email) {
      return null;  // Ako nije unesena vrijednost, validacija nije potrebna
    }

    // Poboljšani regex koji provjerava email koji mora završiti s .com ili .ba
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.(com|ba)$/;

    if (!emailRegex.test(email)) {
      return { invalidEmail: true };
    }

    return null;
  }


  checkUsernameAvailability() {
    const username = this.registrationForm.get('username')?.value;
    if (!username) return;
    this.http.get(
      `http://localhost:7000/api/MyAppUserEndpoint/check-username-availability?username=${username}`
    ).subscribe(
      (response: any) => {
        if (response && !response.available) {
          this.registrationForm.get('username')?.setErrors({ usernameTaken: true });
        }
      },
      (error) => {
        console.error('Error checking username availability:', error);
      }
    );
  }


  checkPasswordStrengthOnBackend() {
    const password = this.registrationForm.get('password')?.value;
    if (!password) return;
    this.http.get(
      `http://localhost:7000/api/MyAppUserEndpoint/check-password-availability?password=${password}`
    ).subscribe(
      (response: any) => {
        if (response && !response.available) {
          this.registrationForm.get('password')?.setErrors({ passwordTaken: true });
        }
      },
      (error) => {
        console.error('Error checking password availability:', error);
      }
    );
  }


  isStepValid(step: number) {
    switch (step) {
      case 1:
        return this.registrationForm.get('firstName')!.valid &&
          this.registrationForm.get('lastName')!.valid &&
          this.registrationForm.get('gender')!.valid &&
          this.registrationForm.get('city')!.valid &&
          this.registrationForm.get('age')!.valid &&
          this.registrationForm.get('email')!.valid &&
          this.registrationForm.get('phoneNumber')!.valid;
      case 2:
        return this.registrationForm.get('profilePicture')!.valid &&
          this.registrationForm.get('experience')!.valid &&
          this.registrationForm.get('rate')!.valid &&
          this.registrationForm.get('qualifications')!.valid &&
          this.registrationForm.get('availability')!.valid &&
          this.registrationForm.get('cancellationPolicy')!.valid;
      case 3:
        return this.registrationForm.get('username')!.valid &&
          this.registrationForm.get('password')!.valid &&
          this.registrationForm.get('confirmPassword')!.valid &&
          !this.registrationForm.errors; // Provjera da nema grešaka (mismatch)
      default:
        return false;
    }
  }

  togglePasswordVisibility(field: 'password' | 'confirmPassword'): void {
    if (field === 'password') {
      this.showPassword = !this.showPassword;
    } else if (field === 'confirmPassword') {
      this.showConfirmPassword = !this.showConfirmPassword;
    }
  }
}
