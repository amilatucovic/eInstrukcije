import { AbstractControl, ValidationErrors } from '@angular/forms';

export class CustomValidators {
  static nonWhitespace(control: AbstractControl): ValidationErrors | null {
    const isValid = control.value && control.value.trim().length > 0;
    return isValid ? null : { whitespace: true };
  }

  static phoneNumber(control: AbstractControl): ValidationErrors | null {
    const value = control.value;
    if (!value) return null;

    const isValid = /^[0-9]{9,}$/.test(value);
    return isValid ? null : { invalidPhone: true };
  }

  static email(control: AbstractControl): ValidationErrors | null {
    const value = control.value;
    if (!value) return null;

    const isValid = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value);
    return isValid ? null : { invalidEmail: true };
  }

  static numericValue(control: AbstractControl): ValidationErrors | null {
    const value = control.value;

    if (value === null || value === undefined || value === '') {
      return { numeric: true };
    }


    const valueStr = value.toString().trim();


    if (valueStr.length === 0) {
      return { numeric: true };
    }


    const isNumeric = /^[0-9]+(\.[0-9]{1,2})?$/.test(valueStr);
    return isNumeric ? null : { numeric: true };
  }

}
