import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Subject {
  id: number;
  name: string;
  description: string;
  difficultyLevel: string;
  categories: Category[];
}

export interface Category {
  id: number;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class SubjectService {
  private baseUrl = 'http://localhost:7000/api/SubjectEndpoint';

  constructor(private http: HttpClient) {}

  getAllSubjects(): Observable<Subject[]> {
    return this.http.get<Subject[]>(this.baseUrl);
  }


  getCategoriesForSubject(subjectId: number): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.baseUrl}/categories/${subjectId}`);
  }


}
