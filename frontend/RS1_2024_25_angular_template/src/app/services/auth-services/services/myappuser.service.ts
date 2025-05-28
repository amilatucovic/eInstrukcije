import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpParams } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class MyAppUserService {

    private apiUrl = 'http://localhost:7000/api/MyAppUserEndpoint';
    constructor(private http: HttpClient) { }

    checkUsernameAvailability(username: string): Observable<{ available: boolean }> {
        const params = new HttpParams().set('username', username);
        return this.http.get<{ available: boolean }>(`${this.apiUrl}/check-username-availability`, { params });
    }
}
