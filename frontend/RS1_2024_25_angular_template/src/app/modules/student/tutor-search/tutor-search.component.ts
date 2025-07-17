import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CitiesService } from '../../../services/auth-services/services/cities.service';
import { City } from '../../../models/city.model';


@Component({
  selector: 'app-tutor-search',
  templateUrl: './tutor-search.component.html',
  styleUrls: ['./tutor-search.component.css']
})
export class TutorSearchComponent implements OnInit {
  searchForm: FormGroup;
  subjects: any[] = [];
  categories: any[] = [];
  tutors: any[] = [];
  cities: City[] = [];
  isLoading = false;

  constructor(private fb: FormBuilder, private http: HttpClient,  private citiesService: CitiesService) {
    this.searchForm = this.fb.group({
      subjectId: [''],
      categoryId: [''],
      minPrice: [''],
      maxPrice: [''],
      cityId: [''],
      inPersonAvailable: [null],
    });
  }

  ngOnInit(): void {
    this.fetchSubjects(); // Load subjects initially
    this.fetchCities();

    // Subscribe to form value changes for real-time filtering
    this.searchForm.valueChanges.subscribe((filters) => {
      this.fetchTutors(filters); // Trigger filtering on form change
    });
  }

  fetchSubjects(): void {
    this.isLoading = true;
    this.http.get<any[]>('http://localhost:7000/api/SubjectEndpoint').subscribe(
      (data) => {
        this.subjects = data;  // Populate subjects dropdown
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching subjects', error);
        this.isLoading = false;
      }
    );
  }

  fetchCities(): void {
    this.citiesService.getCities().subscribe(
      (data) => {
        this.cities = data.map(cityData => new City(cityData.id, cityData.name, cityData.postalCode));
        console.log(this.cities);
      },
      (error) => {
        console.error('Greška pri učitavanju gradova', error);
      }
    );
  }

  // Fetch categories based on selected subject
  onSubjectChange(event: any): void {
    const selectedSubjectId = event.target.value;
    console.log('Selected Subject ID:', selectedSubjectId); // Debugging

    // Reset categories and clear the selected category
    this.categories = [];
    this.searchForm.get('categoryId')?.setValue(''); // Clear category selection

    if (selectedSubjectId) {
      this.isLoading = true;
      this.http.get<any[]>(`http://localhost:7000/api/SubjectEndpoint/categories/${selectedSubjectId}`).subscribe(
        (data) => {
          console.log('Fetched categories:', data); // Debugging
          this.categories = data; // Populate categories dropdown with the new data
          this.isLoading = false;
        },
        (error) => {
          console.error('Error fetching categories', error);
          this.isLoading = false;
        }
      );
    }
  }

  fetchTutors(filters: any): void {
    this.isLoading = true;

    this.http.post<any[]>('http://localhost:7000/api/SearchEndpoint/search-tutors', filters).subscribe(
      (tutors) => {
        console.log('Tutors received:', tutors);
        this.tutors = tutors; // now the response is just the array itself
        console.log(`Number of tutors found: ${this.tutors.length}`);
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching tutors', error);
        this.isLoading = false;
      }
    );
  }
}
