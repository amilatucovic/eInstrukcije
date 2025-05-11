import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-student-dashboard',
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.css']
})
export class StudentDashboardComponent implements OnInit {
  profileImageUrl: string | ArrayBuffer | null = null;
  hovering = false;

  constructor() { }

  ngOnInit(): void {
    const savedImage = localStorage.getItem('studentProfileImage');
    if (savedImage) {
      this.profileImageUrl = savedImage;
    }
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
}
