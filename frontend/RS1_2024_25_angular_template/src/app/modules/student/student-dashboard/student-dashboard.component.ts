import { Component, OnInit } from '@angular/core';
import { MyAuthService } from '../../../services/auth-services/my-auth.service';
import { Router } from '@angular/router';
import { Student } from '../../../models/student.model';

@Component({
  selector: 'app-student-dashboard',
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.css']
})
export class StudentDashboardComponent implements OnInit {
  profileImageUrl: string | ArrayBuffer | null = null;
  hovering = false;
  user: Student | null = null;
  showConfirmModal = false;

  constructor(private myAuth: MyAuthService, private router: Router) { }

  ngOnInit(): void {
    const savedImage = localStorage.getItem('studentProfileImage');
    if (savedImage) {
      this.profileImageUrl = savedImage;
    }

    this.user = this.myAuth.getLoggedInUser() as Student;
  }

  onFileSelected(event: Event): void {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        this.profileImageUrl = reader.result;
        localStorage.setItem('studentProfileImage', this.profileImageUrl as string);
      };
      reader.readAsDataURL(file);
    }
  }

  removeProfileImage(event: MouseEvent): void {
    event.stopPropagation();
    this.profileImageUrl = null;
    localStorage.removeItem('studentProfileImage');
  }

  showCloseConfirmation() {
    this.showConfirmModal = true;
  }

  confirmClose() {
    this.showConfirmModal = false;
    this.router.navigate(['/']);
  }

  cancelClose() {
    this.showConfirmModal = false;
  }

  onOverlayClick(event: Event) {
    if (event.target === event.currentTarget) {
      this.cancelClose();
    }
  }
}
