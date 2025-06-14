import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface LessonToday {
  lessonID: number;
  studentName: string;
  subjectName: string;
  startTime: string;
  endTime: string;
  lessonDate: string; // OVO TI TREBA za kalendar
  mode: string;
}
export interface LessonSchedule {
  lessonID: number;
  studentName: string;
  subjectName: string;
  start: string; // ISO string
  end: string;   // ISO string
  lessonMode: string;
  status: string;
  tutorName: string
}


@Injectable({
  providedIn: 'root'
})
export class LessonService {
  private todayUrl = 'http://localhost:7000/api/LessonGetTodayByTutorIdEndpoint';
  private weeklyUrl = 'http://localhost:7000/api/LessonEndpoint/schedule';
  private baseUrl = 'http://localhost:7000/api/LessonEndpoint/lessons';

  constructor(private http: HttpClient) { }

  getTodayLessons(tutorId: number) {
    return this.http.get<LessonToday[]>(`${this.todayUrl}/tutor/${tutorId}`);
  }
  getWeeklyLessons(tutorId: number) {
    return this.http.get<LessonSchedule[]>(`${this.weeklyUrl}/${tutorId}`);
  }

  getLessonsForStudent(studentId: number): Observable<LessonSchedule[]> {
    return this.http.get<LessonSchedule[]>(`${this.baseUrl}/${studentId}`);
  }
}
