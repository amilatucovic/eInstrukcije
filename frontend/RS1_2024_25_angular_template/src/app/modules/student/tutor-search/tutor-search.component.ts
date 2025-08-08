import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CitiesService } from '../../../services/auth-services/services/cities.service';
import { City } from '../../../models/city.model';
import { CalendarOptions } from '@fullcalendar/core';
import timeGridPlugin from '@fullcalendar/timegrid';
import interactionPlugin from '@fullcalendar/interaction';
import hrLocale from '@fullcalendar/core/locales/hr';
import { LessonService } from '../../../services/auth-services/services/lesson.service';
import { LessonSchedule } from '../../../services/auth-services/services/lesson.service';

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

  constructor(private fb: FormBuilder, private http: HttpClient, private citiesService: CitiesService, private lessonService: LessonService) {
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

  showConfirmModal = false;

  private bosnianMonths = [
    'Januar', 'Februar', 'Mart', 'April', 'Maj', 'Juni',
    'Juli', 'August', 'Septembar', 'Oktobar', 'Novembar', 'Decembar'
  ];

  calendarOptions: CalendarOptions = {
    plugins: [timeGridPlugin, interactionPlugin],
    initialView: 'timeGridWeek',
    locale: hrLocale,
    headerToolbar: {
      left: '',
      center: 'prev title next',
      right: ''
    },
    viewDidMount: (info) => {
      this.updateCalendarTitle(info);
    },
    datesSet: (info) => {
      this.updateCalendarTitle(info);
    },
    buttonText: {
      prev: 'Prethodna sedmica',
      next: 'Sljedeća sedmica'
    },
    slotMinTime: '08:00:00',
    slotMaxTime: '21:00:00',
    allDaySlot: false,
    titleFormat: { month: 'long', year: 'numeric' },
    dayHeaderFormat: { weekday: 'long' },
    eventTimeFormat: {
      hour: '2-digit',
      minute: '2-digit',
      meridiem: false
    },
    slotLabelFormat: {
      hour: '2-digit',
      minute: '2-digit',
      meridiem: false
    },
    eventContent: function (arg) {
      return {
        html: `
      <div style="text-align:center;">
        <div style="margin-bottom: 2px;">Zauzeto</div>
        <div style="font-size: 0.8em;">${arg.timeText}</div>
      </div>
    `
      };
    },
    firstDay: 1,
    dateClick: this.handleDateClick.bind(this),
    selectable: true,
    selectMirror: true,
    dayMaxEvents: true,
    weekends: true,
    editable: true,
    height: 'auto'
  };

  private updateCalendarTitle(info: any): void {
    setTimeout(() => {
      const titleElement = document.querySelector('.fc-toolbar-title');
      if (!titleElement) return;

      const currentDate = info.start || info.view.currentStart;
      const month = currentDate.getMonth();
      const year = currentDate.getFullYear();

      const mainTitle = `${this.bosnianMonths[month]} ${year}`;
      const weekRange = this.getWeekRange(info);

      titleElement.innerHTML = `
      <div style="display: flex; flex-direction: column; align-items: center; line-height: 1.2;">
      <div style="font-size: 1.2em; font-weight: bold;">${mainTitle}</div>
      <div style="font-size: 0.85em; color: #666; margin-top: 4px;">${weekRange}</div>
      </div>`;
    }, 10);
  }

  selectTutor(tutor: any): void {
    console.log('Selektovani tutor:', tutor);
    this.selectedTutorForSchedule = tutor;
    this.loadTutorSchedule(tutor.id);
  }

  loadTutorSchedule(tutorId: number): void {
    this.lessonService.getLessonsForTutor(tutorId).subscribe(
      (lessons: LessonSchedule[]) => {
        console.log('Lekcije za tutora:', lessons);
        const events = lessons.map(lesson => {
          return {
            start: new Date(lesson.start),
            end: new Date(lesson.end),
            display: 'block',
            color: '#e28585',
            borderColor: '#990000',
            textColor: '#990000',
            meta: lesson
          };
        });
        this.calendarOptions = {
          ...this.calendarOptions,
          events: events
        };
      },
      (error) => {
        console.error('Greška pri učitavanju rasporeda', error);
      }
    );
  }

  showCloseConfirmation() {
    this.showConfirmModal = true;
  }

  confirmClose() {
    this.showConfirmModal = false;
    this.selectedTutorForSchedule = null;
  }

  cancelClose() {
    this.showConfirmModal = false;
  }

  saveReservation() {
    console.log('Rezervacija spremljena!');
  }

  onOverlayClick(event: Event) {
    if (event.target === event.currentTarget) {
      this.cancelClose();
    }
  }

  handleDateClick(arg: any) {
    alert('Kliknuli ste datum: ' + arg.dateStr);
  }

  private getWeekRange(info: any): string {
    const startDate = new Date(info.start || info.view.currentStart);
    const endDate = new Date(info.end || info.view.currentEnd);
    endDate.setDate(endDate.getDate() - 1);

    const startDay = startDate.getDate();
    const startMonth = startDate.getMonth();
    const endDay = endDate.getDate();
    const endMonth = endDate.getMonth();

    if (startMonth === endMonth) {
      return `${startDay}. - ${endDay}. ${this.bosnianMonths[startMonth]}`;
    } else {
      return `${startDay}. ${this.bosnianMonths[startMonth]} - ${endDay}. ${this.bosnianMonths[endMonth]}`;
    }
  }
}
