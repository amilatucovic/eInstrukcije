import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {TutorProfile} from '../../../modules/tutor/tutor-profile.model';

@Injectable({
  providedIn: 'root'
})
export class TutorService {
  private baseUrl = 'http://localhost:7000/api/TutorProfileEndpoint';

  constructor(private http: HttpClient) {}

  getTutorProfile(tutorId: number): Observable<TutorProfile> {
    return this.http.get<TutorProfile>(`${this.baseUrl}/${tutorId}`);
  }
}
