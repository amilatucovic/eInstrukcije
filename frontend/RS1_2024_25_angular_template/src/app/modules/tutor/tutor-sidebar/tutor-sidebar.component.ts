import { Component, OnInit } from '@angular/core';
import { TutorService } from '../../../services/auth-services/services/tutor-profile.service';
import { TutorProfile } from '../tutor-profile.model';
import { Router } from '@angular/router';
import {UserImageService} from '../../../services/auth-services/services/user-image.service';

@Component({
  selector: 'app-tutor-sidebar',
  templateUrl: './tutor-sidebar.component.html',
  styleUrls: ['./tutor-sidebar.component.css']
})
export class TutorSidebarComponent implements OnInit {
  tutorProfile?: TutorProfile;
  fullName: string = 'Instruktor';
  profileImageUrl: string = '/assets/user.webp';

  constructor(
    private tutorService: TutorService,
    private router: Router,
    private userImageService: UserImageService
  ) {}

  ngOnInit(): void {
    const tutorId = Number(localStorage.getItem('tutorId'));
    if (!tutorId) return;


    this.tutorService.getTutorProfile(tutorId).subscribe({
      next: (profile) => {
        this.tutorProfile = profile;
        this.userImageService.setFullName(profile.firstName, profile.lastName);
        if (profile.profileImageUrl)
          this.profileImageUrl = `http://localhost:7000${profile.profileImageUrl}`;
      },
      error: (err) => console.error('Greška pri dohvaćanju profila', err)
    });

    this.userImageService.fullName$.subscribe(name => {
      if (name) this.fullName = name;
    });

    this.userImageService.imageUrl$.subscribe(url => {
      if (url) this.profileImageUrl = url;
    });
  }




  logout(): void {
    localStorage.clear();
    this.router.navigate(['/auth/login']);
  }


}
