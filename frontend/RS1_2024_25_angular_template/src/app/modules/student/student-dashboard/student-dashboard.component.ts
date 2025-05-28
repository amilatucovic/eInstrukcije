import { Component, OnInit } from '@angular/core';
import { MyAuthService } from '../../../services/auth-services/my-auth.service';
import { MyAppUser } from '../../../models/myAppUser.model';

@Component({
  selector: 'app-student-dashboard',
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.css']
})
export class StudentDashboardComponent implements OnInit {
  profileImageUrl: string | ArrayBuffer | null = null;
  hovering = false;
  activeTab: string = 'home'; //početni aktivni tab
  user: MyAppUser | null = null;

  setActiveTab(tabName: string) {
    this.activeTab = tabName;
  }

  constructor(private myAuth: MyAuthService) { }

  ngOnInit(): void {
    const savedImage = localStorage.getItem('studentProfileImage');
    if (savedImage) {
      this.profileImageUrl = savedImage;
    }

    this.user = this.myAuth.getLoggedInUser();
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
