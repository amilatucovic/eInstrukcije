import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserImageService {
  private imageUrlSubject = new BehaviorSubject<string>('');
  private fullNameSubject = new BehaviorSubject<string>(''); // Dodano

  imageUrl$ = this.imageUrlSubject.asObservable();
  fullName$ = this.fullNameSubject.asObservable(); // Dodano

  setImageUrl(url: string): void {
    this.imageUrlSubject.next(url);
  }

  setFullName(firstName: string, lastName: string): void {
    this.fullNameSubject.next(`${firstName} ${lastName}`);
  }
}
