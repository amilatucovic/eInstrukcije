import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-contact-us',
  templateUrl: './contact-us.component.html',
  styleUrl: './contact-us.component.css'
})
export class ContactUsComponent {
  UserForm: FormGroup;

  constructor(private snackBar: MatSnackBar) {
    this.UserForm = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.pattern(/^\p{L}+(?:[ '\-]?\p{L}+)*$/u)]),
      email: new FormControl('', [Validators.required, Validators.email]),
      message: new FormControl('', [Validators.required, Validators.minLength(10)])
    });
  }
  submit() {
    this.UserForm.markAllAsTouched();
    if (this.UserForm.valid) {
      this.snackBar.open('Slanje poruke uspješno!', '', {
        duration: 4000,
        horizontalPosition: 'center',
        verticalPosition: 'top',
        panelClass: ['custom-snackbar-success']
      })
    }
    else {
      this.snackBar.open('Forma nije validna!', '', {
        duration: 4000,
        horizontalPosition: 'center',
        verticalPosition: 'top',
        panelClass: ['custom-snackbar-error']
      });
    }
  }
}
