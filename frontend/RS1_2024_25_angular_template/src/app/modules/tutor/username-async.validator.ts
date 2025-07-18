import { AbstractControl, AsyncValidatorFn } from '@angular/forms';
import {TutorService} from '../../services/auth-services/services/tutor.service';
import { map, catchError, debounceTime, switchMap, of } from 'rxjs';

export function usernameAsyncValidator(tutorService: TutorService, tutorId: number): AsyncValidatorFn {
  return (control: AbstractControl) => {
    if (!control.value) return of(null);

    return of(control.value).pipe(
      debounceTime(500),
      switchMap(username => tutorService.checkUsername(username, tutorId)),
      map(response => response.isTaken ? { usernameTaken: true } : null),
      catchError(() => of(null))
    );
  };
}
