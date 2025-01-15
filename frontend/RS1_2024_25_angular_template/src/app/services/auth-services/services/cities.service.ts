import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { City } from '../../../models/city.model';

@Injectable({
  providedIn: 'root'
})
export class CitiesService {

  constructor(private http: HttpClient) { }

  getCities(): Observable<City[]> {
    var apiUrl = 'http://localhost:7000/api/CityEndpoint'; // API endpoint za gradove    
    return this.http.get<City[]>(apiUrl);
  }

}
