import { Component, OnInit } from '@angular/core';
import { TutorService } from '../../../services/auth-services/services/tutor-profile.service';
import { TutorProfile } from '../tutor-profile.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tutor-sidebar',
  templateUrl: './tutor-sidebar.component.html',
  styleUrls: ['./tutor-sidebar.component.css']
})
export class TutorSidebarComponent implements OnInit {
  tutorProfile?: TutorProfile;

  constructor(
    private tutorService: TutorService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const tutorId = Number(localStorage.getItem('tutorId'));
    if (!tutorId) return;

    this.tutorService.getTutorProfile(tutorId).subscribe({
      next: (profile) => this.tutorProfile = profile,
      error: (err) => console.error('Greška pri dohvaćanju profila', err)
    });
  }

  get profileImageUrl(): string {
    return this.tutorProfile?.profileImageUrl
      ? `http://localhost:7000${this.tutorProfile.profileImageUrl}`
      : '/assets/user.webp';
  }

  logout(): void {
    localStorage.clear();
    this.router.navigate(['/auth/login']);
  }


}
