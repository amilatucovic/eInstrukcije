import { Component, OnInit } from '@angular/core';
import { LessonService, LessonToday } from '../../../services/auth-services/services/lesson.service';
import { TutorProfile } from '../tutor-profile.model';
import { TutorService } from '../../../services/auth-services/services/tutor-profile.service';
import { CalendarEvent, CalendarView } from 'angular-calendar';
import { Subject } from 'rxjs';
import { format } from 'date-fns';


@Component({
  selector: 'app-tutor-dashboard',
  templateUrl: './tutor-dashboard.component.html',
  styleUrls: ['./tutor-dashboard.component.css']
})
export class TutorDashboardComponent implements OnInit {
  lessons: LessonToday[] = [];
  tutorName: string = '';
  tutorProfile?: TutorProfile;
  view: CalendarView = CalendarView.Week;
  viewDate: Date = new Date();
  events: CalendarEvent[] = [];
  refresh: Subject<any> = new Subject();
  locale: string = 'bs';

  constructor(
    private lessonService: LessonService,
    private tutorService: TutorService
  ) {}

  ngOnInit(): void {
    const tutorId = Number(localStorage.getItem('tutorId'));

    if (!tutorId) {
      console.error('TutorId nije pronađen u localStorage.');
      return;
    }

    this.lessonService.getTodayLessons(tutorId).subscribe({
      next: (data) => this.lessons = data,
      error: (err) => console.error('Error fetching lessons', err)
    });

    this.tutorService.getTutorProfile(tutorId).subscribe({
      next: (profile) => {
        this.tutorProfile = profile;
        this.tutorName = `${profile.firstName} ${profile.lastName}`;
      },
      error: (err) => console.error('Error fetching tutor profile', err)
    });

    this.lessonService.getWeeklyLessons(tutorId).subscribe({
      next: (data) => {
        console.log('Dohvaćeni časovi:', data);

        const uniqueLessons = new Map();
        data.forEach(lesson => {
          const key = `${lesson.subjectName}-${lesson.start}`;
          if (!uniqueLessons.has(key)) {
            uniqueLessons.set(key, lesson);
          }
        });

        this.events = data.map(lesson => {
          const isOnline = lesson.lessonMode === 'Online';

          return {
            title: `${lesson.studentName} - ${lesson.subjectName}<br>
        ${lesson.lessonMode} -
        ${format(new Date(lesson.start), 'HH:mm')} :
        ${format(new Date(lesson.end), 'HH:mm')}`,
            start: new Date(lesson.start),
            end: new Date(lesson.end),
            color: isOnline
              ? { primary: '#ff6347', secondary: '#FFB6C1' }
              : { primary: '#1e90ff', secondary: '#D1E8FF' },
            cssClass: isOnline ? 'event-online' : 'event-inperson',
            allDay: false,
            meta: {
              lessonMode: lesson.lessonMode
            }
          };
        });
        this.refresh.next(null);
      },
      error: (err) => console.error('Error fetching weekly lessons', err)
    });
  }

  get profileImageUrl(): string {
    return this.tutorProfile?.profileImageUrl
      ? `http://localhost:7000${this.tutorProfile.profileImageUrl}`
      : 'assets/profile-picture.jpg';
  }
}
