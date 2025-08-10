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
import { MatSnackBar } from '@angular/material/snack-bar';
import { Student } from '../../../models/student.model';
import { MyAuthService } from '../../../services/auth-services/my-auth.service';

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
  user: Student | null = null;
  isLoading = false;

  selectedTutorForSchedule: any = null;
  selectedSlots: Set<string> = new Set();
  calendarApi: any;


  constructor(private fb: FormBuilder, private http: HttpClient, private citiesService: CitiesService, private lessonService: LessonService, private snackBar: MatSnackBar, private myAuth: MyAuthService) {
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
      this.calendarApi = info.view.calendar;
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
      if (arg.event.classNames.includes('selected-slot')) {
        const start = new Date(arg.event.start!);
        const end = new Date(arg.event.end!);
        const format = (d: Date) =>
          `${d.getHours().toString().padStart(2, '0')}:${d.getMinutes().toString().padStart(2, '0')}`;

        return {
          html: ` 
          <div style="
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100%;
            font-size: 0.9em;
            color: #333;">
          ${format(start)} - ${format(end)}`
        };
      }
      return {
        html: `
        <div style=" display: flex;
         flex-direction: column;
         align-items: center;
         justify-content: center;
         height: 100%;
         width: 100%;
         text-align: center;
         cursor: not-allowed;">
        <div style="margin-bottom: 2px;">Zauzeto</div>
        <div style="font-size: 0.8em;">${arg.timeText}</div>
        </div>`
      };
    },
    firstDay: 1,
    selectable: true,
    selectMirror: false,
    dayMaxEvents: true,
    weekends: true,
    editable: true,
    height: 'auto',
    select: (info) => {
      const startTime = info.start.toISOString();

      if (this.selectedSlots.has(startTime)) {
        this.selectedSlots.delete(startTime);
      } else {
        this.selectedSlots.add(startTime);
      }
      const calendarApi = info.view.calendar;
      calendarApi.unselect();
      calendarApi.refetchEvents();
    },
    eventSources:
      [{
        events: (fetchInfo, successCallback) => {
          const groupedSlots = this.groupContinuousSlots(Array.from(this.selectedSlots));
          const events = groupedSlots.map(slot => ({
            start: slot.start,
            end: slot.end,
            title: '',
            className: 'selected-slot'
          }));
          successCallback(events);
        }
      },
      {
        events: (fetchInfo, successCallback, failureCallback) => {
          const tutorId = this.selectedTutorForSchedule?.id;
          if (!tutorId) {
            successCallback([]);
            return;
          }
          this.lessonService.getLessonsForTutor(tutorId).subscribe(
            (lessons: LessonSchedule[]) => {
              const events = lessons.map(lesson => ({
                title: 'Zauzeto',
                start: new Date(lesson.start),
                end: new Date(lesson.end),
                display: 'block',
                color: '#e28585',
                borderColor: '#990000',
                textColor: '#990000',
                meta: lesson
              }));
              successCallback(events);
            },
            (error) => {
              console.error('Greška pri učitavanju rasporeda', error);
              failureCallback(error);
            }
          );
        }
      }
      ],
    eventClick: (info) => {
      const event = info.event;
      if (event.classNames.includes('selected-slot')) {
        const startStr = event.start?.toISOString();
        if (startStr && this.selectedSlots.has(startStr)) {
          this.selectedSlots.delete(startStr);
          info.view.calendar.refetchEvents();
        }
      }
    }
  };

  formatTime(date: Date): string {
    const h = date.getHours().toString().padStart(2, '0');
    const m = date.getMinutes().toString().padStart(2, '0');
    return `${h}:${m}`;
  }

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
    this.selectedTutorForSchedule = tutor;
    if (this.calendarApi) {
      this.calendarApi.refetchEvents();
    }
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

  // Funkcija za spajanje slotova
  groupContinuousSlots(slots: string[]): { start: Date; end: Date }[] {
    if (!slots || slots.length === 0) return [];

    const sortedSlots = slots
      .map(s => new Date(s))
      .sort((a, b) => a.getTime() - b.getTime());

    const groups: { start: Date; end: Date }[] = [];
    let start = sortedSlots[0];
    let end = new Date(start.getTime() + 30 * 60000);

    for (let i = 1; i < sortedSlots.length; i++) {
      const current = sortedSlots[i];

      if (current.getTime() === end.getTime()) {
        end = new Date(end.getTime() + 30 * 60000);
      } else {
        groups.push({ start, end });
        start = current;
        end = new Date(start.getTime() + 30 * 60000);
      }
    }

    groups.push({ start, end });

    return groups;
  }

  saveReservation() {
    if (this.selectedSlots.size === 0) {
      this.snackBar.open('Niste odabrali nijedan termin!', '', {
        duration: 4000,
        horizontalPosition: 'center',
        verticalPosition: 'top',
        data: { hasBackdrop: false },
        panelClass: ['custom-snackbar-error']
      });
      return;
    }
    this.user = this.myAuth.getLoggedInUser() as Student;
    const subjectId = this.searchForm.get('subjectId')?.value;
    const subjectIdNumber = Number(subjectId);
    const studentId = this.user.id;

    const grouped = this.groupContinuousSlots(Array.from(this.selectedSlots));

    grouped.forEach(group => {
      const date = group.start.toISOString().split('T')[0];
      const startTime = this.formatTime(group.start);
      const endTime = this.formatTime(group.end);

      const lessonDto = {
        TutorID: this.selectedTutorForSchedule.id,
        StudentID: studentId,
        SubjectID: subjectIdNumber,
        Date: date,
        Start: startTime,
        End: endTime,
        LessonMode: 1
      };

      this.lessonService.addLesson(lessonDto).subscribe({
        next: (res) => {
          this.snackBar.open('Rezervacija uspješna!', '', {
            duration: 4000,
            horizontalPosition: 'center',
            verticalPosition: 'top',
            panelClass: ['custom-snackbar-success']
          });
        },
        error: (err) => {
          if (err.status === 409) {
            this.snackBar.open('U odabranom periodu već imate zakazane instrukcije!', '', {
              duration: 4000,
              horizontalPosition: 'center',
              verticalPosition: 'top',
              panelClass: ['custom-snackbar-error']
            });
          } else if (err.status === 400) {
            this.snackBar.open('Termin ne može biti zakazan u prošlosti!', '', {
              duration: 4000,
              horizontalPosition: 'center',
              verticalPosition: 'top',
              panelClass: ['custom-snackbar-error']
            });
          } else {
            this.snackBar.open('Došlo je do greške prilikom rezervacije.', '', {
              duration: 4000,
              horizontalPosition: 'center',
              verticalPosition: 'top',
              panelClass: ['custom-snackbar-error']
            });
          }
          this.selectedSlots.clear();
          if (this.calendarApi) {
            this.calendarApi.refetchEvents();
          }
        }
      });
    });
  }

  onOverlayClick(event: Event) {
    if (event.target === event.currentTarget) {
      this.cancelClose();
    }
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
