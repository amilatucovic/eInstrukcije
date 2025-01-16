import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-tutor-search',
  templateUrl: './tutor-search.component.html',
  styleUrls: ['./tutor-search.component.css'],
})
export class TutorSearchComponent implements OnInit {
  searchForm: FormGroup;
  subjects: any[] = [];
  categories: any[] = [];
  tutors: any[] = [];
  cities: any[] = [];
  isLoading = false;

  constructor(private fb: FormBuilder, private http: HttpClient) {
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
    this.http.get<any[]>('http://localhost:7000/api/SubjectGetEndpoint').subscribe(
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
    this.isLoading = true;
    this.http.get<any>('http://localhost:7000/api/CityEndpoint').subscribe(
      (response) => {
        this.cities = response?.$values || []; // Extract cities from response
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching cities:', error);
        this.isLoading = false;
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
      this.http.get<any[]>(`http://localhost:7000/api/SubjectGetEndpoint/categories/${selectedSubjectId}`).subscribe(
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
    this.http.post<any>('http://localhost:7000/api/SearchEndpoint/search-tutors', filters).subscribe(
      (data) => {
        console.log('Response from API:', data);  // Log the entire response from the API for debugging
        this.tutors = data.$values;  // Access the array of tutors from the $values property
        console.log(`Number of tutors found: ${this.tutors.length}`); // Log the number of tutors fetched
        this.isLoading = false;  // Set loading to false once the data is fetched
      },
      (error) => {
        console.error('Error fetching tutors', error); // Log any errors
        this.isLoading = false;  // Set loading to false if there is an error
      }
    );
  }
}
