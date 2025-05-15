import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

export interface LessonToday {
  lessonID: number;
  studentName: string;
  subjectName: string;
  startTime: string;
  endTime: string;
  mode: string;
}

@Injectable({
  providedIn: 'root'
})
export class LessonService {
  private apiUrl = 'http://localhost:7000/api/LessonGetTodayByTutorIdEndpoint';


  constructor(private http: HttpClient) { }

  getTodayLessons(tutorId: number) {
    return this.http.get<LessonToday[]>(`${this.apiUrl}/tutor/${tutorId}`);
  }
}
