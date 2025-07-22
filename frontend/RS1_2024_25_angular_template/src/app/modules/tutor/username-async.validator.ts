import { AbstractControl, AsyncValidatorFn } from '@angular/forms';
import {TutorService} from '../../services/auth-services/services/tutor.service';
import { map, catchError, debounceTime, switchMap, of } from 'rxjs';

export function usernameAsyncValidator(tutorService: TutorService, tutorId: number, originalUsername: string): AsyncValidatorFn {
  return (control: AbstractControl) => {
    const newValue = control.value?.trim();
    if (!newValue || newValue.toLowerCase() === originalUsername.toLowerCase()) {
      return of(null);
    }

    return of(newValue).pipe(
      debounceTime(500),
      switchMap(username => tutorService.checkUsername(username, tutorId)),
      map(response => response.isTaken ? { usernameTaken: true } : null),
      catchError(() => of(null))
    );
  };
}

