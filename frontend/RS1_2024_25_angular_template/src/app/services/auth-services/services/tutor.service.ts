import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface TutorProfileDto {
  tutorID: number;
  firstName: string;
  lastName: string;
  username: string;
  email: string;
  phoneNumber: string;
  gender: string;
  cityId: number | null;
  qualifications: string;
  yearsOfExperience: number;
  availability: string;
  policy: string;
  hourlyRate: string;
  isLiveAvailable: boolean | null;
  profileImageUrl?: string;
}

@Injectable({
  providedIn: 'root'
})
export class TutorService {
  private baseUrl = 'http://localhost:7000/api/TutorEndpoint';

  constructor(private http: HttpClient) {}

  getProfile(id: number): Observable<TutorProfileDto> {
    return this.http.get<TutorProfileDto>(`${this.baseUrl}/${id}`);
  }

  updateProfile(id: number, dto: TutorProfileDto): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, dto);
  }

  uploadProfileImage(tutorId: number, base64: string): Observable<{ imageUrl: string }> {
    return this.http.post<{ imageUrl: string }>(
      `${this.baseUrl}/${tutorId}/upload-profile-image`,
      { base64 }
    );
  }

  checkUsername(username: string, tutorId: number): Observable<{ isTaken: boolean }> {
    const url = `${this.baseUrl}/check-username?username=${encodeURIComponent(username)}&excludeTutorId=${tutorId}`;
    return this.http.get<{ isTaken: boolean }>(url);
  }



}
