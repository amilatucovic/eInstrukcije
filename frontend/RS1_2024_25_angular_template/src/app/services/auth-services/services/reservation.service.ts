import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {ReservationStatus} from '../../../models/reservation-status.enum';

export interface ReservationDto {
  id: number;
  studentFullName: string;
  lessonName: string;
  reservationDate: string;
  status: ReservationStatus;
}

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  private apiUrl = 'http://localhost:7000/api/ReservationEndpoint';

  constructor(private http: HttpClient) {}

  getTutorReservations(tutorId: number, status?: string, date?: string, search?: string): Observable<ReservationDto[]> {
    let params = new HttpParams();
    if (status) params = params.set('status', status);
    if (date) params = params.set('date', date);
    if (search) params = params.set('search', search);

    return this.http.get<ReservationDto[]>(`${this.apiUrl}/tutor/${tutorId}`, { params });
  }

  approveReservation(id: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}/approve`, null);
  }

  rejectReservation(id: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}/reject`, null);
  }
}
