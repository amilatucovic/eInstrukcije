import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface TutorAdmin {
  id: number;
  qualifications: string;
  yearsOfExperience: number;
  availability: string;
  policy: string;
  hourlyRate: string;
  isLiveAvailable: boolean;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  cityName: string;
}

export interface TutorUpdateRequest {
  firstName?: string;
  lastName?: string;
  phoneNumber?: string;
  email?: string;
  cityId?: number;
  qualifications?: string;
  yearsOfExperience?: number;
  availability?: string;
  policy?: string;
  hourlyRate?: string;
  isLiveAvailable?: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class TutorAdminService {
  private baseUrl = 'http://localhost:7000/api/TutorAdminEndpoint';

  constructor(private http: HttpClient) {}

  getAllTutors(search?: {
    firstName?: string;
    lastName?: string;
    cityId?: number;
    isLiveAvailable?: boolean;
    maxHourlyRate?: string;
  }): Observable<TutorAdmin[]> {
    let params = new HttpParams();
    if (search) {
      if (search.firstName) params = params.set('FirstName', search.firstName);
      if (search.lastName) params = params.set('LastName', search.lastName);
      if (search.cityId != null) params = params.set('CityId', search.cityId.toString());
      if (search.isLiveAvailable != null) params = params.set('IsLiveAvailable', search.isLiveAvailable.toString());
      if (search.maxHourlyRate) params = params.set('MaxHourlyRate', search.maxHourlyRate);
    }

    return this.http.get<TutorAdmin[]>(`${this.baseUrl}/get-all`, { params });
  }

  updateTutor(id: number, data: TutorUpdateRequest): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, data);
  }

  deleteTutor(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
