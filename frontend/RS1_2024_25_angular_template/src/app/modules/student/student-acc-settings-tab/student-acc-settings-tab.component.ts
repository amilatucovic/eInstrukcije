import { Component, OnInit } from '@angular/core';
import { City } from '../../../models/city.model';
import { CitiesService } from '../../../services/auth-services/services/cities.service';
import { MyAppUser } from '../../../models/myAppUser.model';
import { MyAuthService } from '../../../services/auth-services/my-auth.service';

@Component({
  selector: 'app-student-acc-settings-tab',
  templateUrl: './student-acc-settings-tab.component.html',
  styleUrl: './student-acc-settings-tab.component.css',
})
export class StudentAccSettingsTabComponent implements OnInit {

  cities: City[] = [];
  user: MyAppUser | null = null;
  userCityName: string | null = null;
  profileImageUrl: string | ArrayBuffer | null = null;
  hovering = false;

  constructor(private cityService: CitiesService, private myAuth: MyAuthService) { }

  ngOnInit(): void {
    this.cityService.getCities().subscribe({
      next: (data) => this.cities = data,
      error: (err) => console.error("Greska pri ucitavanju gradova", err)
    });

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

