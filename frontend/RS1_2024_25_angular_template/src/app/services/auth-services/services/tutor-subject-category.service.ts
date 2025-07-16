import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface TutorSubjectCategoryDto {
  tutorID: number;
  subjectID: number;
  categoryID?: number | null
}

export interface TutorSubjectCategoryEntry {
  SubjectID: number;
  SubjectName: string;
  CategoryID: number | null;
  CategoryName: string | null;
}

interface PagedResult<T> {
  totalCount: number;
  items: T[];
}


@Injectable({
  providedIn: 'root'
})
export class TutorSubjectCategoryService {
  private apiUrl = 'http://localhost:7000/api/TutorSubjectCategory';

  constructor(private http: HttpClient) {}


  // POST (add subject + category for tutor)
  add(dto: TutorSubjectCategoryDto): Observable<any> {
    return this.http.post(this.apiUrl, dto);
  }

  // DELETE (remove subject + category for tutor)
  delete(tutorID: number, subjectID: number, categoryID: number): Observable<any> {
    let params = new HttpParams()
      .set('tutorId', tutorID)
      .set('subjectId', subjectID)
      .set('categoryId', categoryID);

    return this.http.delete(this.apiUrl, { params });
  }

  getByTutorIdPaged(tutorId: number, page: number, pageSize: number): Observable<PagedResult<TutorSubjectCategoryEntry>> {
    return this.http.get<PagedResult<TutorSubjectCategoryEntry>>(
      `${this.apiUrl}/paged?tutorId=${tutorId}&page=${page}&pageSize=${pageSize}`
    );

  }

  getByTutorIdAll(tutorId: number) {
    return this.http.get<TutorSubjectCategoryEntry[]>(`${this.apiUrl}/${tutorId}`);
  }



}
