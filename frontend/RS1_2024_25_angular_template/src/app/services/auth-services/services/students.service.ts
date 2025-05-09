import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Student } from '../../../models/student.model';
import { buildHttpParams } from '../../../helper/http-params.helper';
import { StudentUpdateDTO } from '../../../models/studentUpdate.model';

@Injectable({
    providedIn: 'root'
})
export class StudentService {

    constructor(private http: HttpClient) { }

    getStudents(filter?: any): Observable<Student[]> {
        var queryString = buildHttpParams(filter);
        var apiUrl = `http://localhost:7000/api/StudentEndpoints?${queryString}`;
        return this.http.get<Student[]>(apiUrl);
    }

    delete(studentId: number) {
        var apiUrl = `http://localhost:7000/api/StudentEndpoints/${studentId}`;
        return this.http.delete<Student>(apiUrl);
    }

    update(studentId: number, podaci: StudentUpdateDTO) {
        console.log(podaci);
        const apiUrl = `http://localhost:7000/api/StudentEndpoints/${studentId}`;
        return this.http.put<Student>(apiUrl, podaci);
    }
}
