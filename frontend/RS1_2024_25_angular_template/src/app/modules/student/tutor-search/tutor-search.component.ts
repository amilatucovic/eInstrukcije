import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CitiesService } from '../../../services/auth-services/services/cities.service';
import { City } from '../../../models/city.model';
import { CalendarOptions } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import interactionPlugin from '@fullcalendar/interaction';

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
  selectedTutorForSchedule: any = null;

  constructor(private fb: FormBuilder, private http: HttpClient, private citiesService: CitiesService) {
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
    this.fetchSubjects();
    this.fetchCities();

    // Subscribe to form value changes for real-time filtering
    this.searchForm.valueChanges.subscribe((filters) => {
      this.fetchTutors(filters);
    });
  }

  fetchSubjects(): void {
    this.isLoading = true;
    this.http.get<any[]>('http://localhost:7000/api/SubjectEndpoint').subscribe(
      (data) => {
        this.subjects = data;
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

  onSubjectChange(event: any): void {
    const selectedSubjectId = event.target.value;
    console.log('Selected Subject ID:', selectedSubjectId);

    this.categories = [];
    this.searchForm.get('categoryId')?.setValue('');

    if (selectedSubjectId) {
      this.isLoading = true;
      this.http.get<any[]>(`http://localhost:7000/api/SubjectEndpoint/categories/${selectedSubjectId}`).subscribe(
        (data) => {
          console.log('Fetched categories:', data);
          this.categories = data;
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
        this.tutors = tutors;
        console.log(`Number of tutors found: ${this.tutors.length}`);
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching tutors', error);
        this.isLoading = false;
      }
    );
  }

  showReservationForm = false;
  showConfirmModal = false;

  calendarOptions: CalendarOptions = {
    plugins: [dayGridPlugin, timeGridPlugin, interactionPlugin],
    initialView: 'dayGridMonth',
    locale: 'bs',
    headerToolbar: {
      left: 'prev,next',
      center: 'title',
      right: 'dayGridMonth,timeGridWeek'
    },
    buttonText: {
      today: 'Danas',
      month: 'Mjesec',
      week: 'Sedmica',
      day: 'Dan'
    },
    dayHeaderFormat: { weekday: 'long' },
    titleFormat: { year: 'numeric', month: 'long' },
    eventTimeFormat: { // 24-satni format
      hour: '2-digit',
      minute: '2-digit',
      meridiem: false
    },
    slotLabelFormat: { // 24-satni format za time grid
      hour: '2-digit',
      minute: '2-digit',
      meridiem: false
    },
    firstDay: 1, // Ponedjeljak kao prvi dan u sedmici
    events: [
      {
        title: 'Zauzeto',
        start: '2025-07-07T10:00:00',
        end: '2025-07-07T12:00:00',
        color: '#ff6b6b',
        textColor: '#ffffff'
      },
      {
        title: 'Slobodan termin',
        start: '2025-07-08T14:00:00',
        end: '2025-07-08T16:00:00',
        color: '#51cf66',
        textColor: '#ffffff'
      }
    ],
    dateClick: this.handleDateClick.bind(this),
    selectable: true,
    selectMirror: true,
    dayMaxEvents: true,
    weekends: true,
    editable: true,
    height: 'auto'
  };

  showCloseConfirmation() {
    this.showConfirmModal = true;
  }

  confirmClose() {
    this.showConfirmModal = false;
    this.showReservationForm = false;
  }

  cancelClose() {
    this.showConfirmModal = false;
  }

  saveReservation() {
    console.log('Rezervacija spremljena!');
    this.showReservationForm = false;
  }

  onOverlayClick(event: Event) {
    if (event.target === event.currentTarget) {
      this.cancelClose();
    }
  }

  handleDateClick(arg: any) {
    alert('Kliknuli ste datum: ' + arg.dateStr);
  }
}