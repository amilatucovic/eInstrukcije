import {Component, OnInit} from '@angular/core';
import {LessonService, LessonToday} from '../../../services/auth-services/services/lesson.service';
import {TutorProfile} from '../tutor-profile.model';
import {TutorService} from '../../../services/auth-services/services/tutor-profile.service';

@Component({
  selector: 'app-tutor-dashboard',
  templateUrl: './tutor-dashboard.component.html',
  styleUrls: ['./tutor-dashboard.component.css']
})
export class TutorDashboardComponent implements OnInit {
  lessons: LessonToday[] = [];
  tutorName: string = '';
  tutorProfile?: TutorProfile;

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
        this.tutorName = profile.firstName + ' ' + profile.lastName;
      },
      error: (err) => console.error('Error fetching tutor profile', err)
    });
  }

  get profileImageUrl(): string {
    return this.tutorProfile?.profileImageUrl
      ? `http://localhost:7000${this.tutorProfile.profileImageUrl}`
      : 'assets/profile-picture.jpg';
  }

}

