import { Component, OnInit } from '@angular/core';
import {TutorService, TutorProfileDto} from '../../../services/auth-services/services/tutor.service';
import {CitiesService} from '../../../services/auth-services/services/cities.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import {UserImageService} from '../../../services/auth-services/services/user-image.service';
import {usernameAsyncValidator} from '../username-async.validator';
import {CustomValidators} from '../custom-validators';

@Component({
  selector: 'app-tutor-profile',
  templateUrl: './tutor-profile.component.html',
  styleUrls: ['./tutor-profile.component.css']
})
export class TutorProfileComponent implements OnInit {
  profileForm!: FormGroup;
  tutorId: number = 0;
  cities: any[] = [];
  profileImageUrl: string = '';
  selectedImageBase64: string | null = null;


  constructor(
    private fb: FormBuilder,
    private tutorService: TutorService,
    private cityService: CitiesService,
    private userImageService: UserImageService
  ) {}

  ngOnInit(): void {
    const storedId = localStorage.getItem("tutorId");
    if (storedId) this.tutorId = +storedId;
    console.log(this.tutorId)

    this.profileForm = this.fb.group({
      firstName: ['', [CustomValidators.nonWhitespace]],
      lastName: ['', [CustomValidators.nonWhitespace]],
      username: ['', [CustomValidators.nonWhitespace], [usernameAsyncValidator(this.tutorService, this.tutorId)]],
      email: ['', [CustomValidators.email]],
      phoneNumber: ['', [CustomValidators.phoneNumber]],
      gender: [''],
      cityId: [null],
      qualifications: ['', [CustomValidators.nonWhitespace]],
      yearsOfExperience: [0],
      availability: ['', [CustomValidators.nonWhitespace]],
      policy: ['', [CustomValidators.nonWhitespace]],
      hourlyRate: [''],
      isLiveAvailable: [false]
    });



    this.tutorService.getProfile(this.tutorId).subscribe((data) => {
      console.log("Dohvaćeni profil:", data);
      const hourlyRate = data.hourlyRate?.replace(/[^\d.,]/g, '') || '';
      this.profileForm.patchValue({
        ...data,
        hourlyRate: hourlyRate
      });
      this.profileImageUrl = data.profileImageUrl
        ? `http://localhost:7000${data.profileImageUrl}`
        : '';
    });


    this.cityService.getCities().subscribe(c => this.cities = c);
  }

  save(): void {
     if (this.profileForm.invalid) {
      this.profileForm.markAllAsTouched();
      return;
    }

    const formValue = this.profileForm.value;
    let rawRate = formValue.hourlyRate?.toString().trim();

    if (rawRate === '' || isNaN(Number(rawRate))) {
      alert("Molimo unesite ispravnu cijenu.");
      return;
    }

    formValue.hourlyRate = `${parseFloat(rawRate).toFixed(2)}KM/h`;

    this.tutorService.updateProfile(this.tutorId, {
      tutorID: this.tutorId,
      ...formValue
    }).subscribe({
      next: () => {
        this.userImageService.setFullName(formValue.firstName, formValue.lastName);
        if (this.selectedImageBase64) {
          this.tutorService.uploadProfileImage(this.tutorId, this.selectedImageBase64).subscribe(res => {
            this.userImageService.setImageUrl(`http://localhost:7000${res.imageUrl}`);
            this.selectedImageBase64 = null;
          });
        }
        alert("Uspješno sačuvano.");
      },
      error: () => alert("Greška prilikom snimanja.")
    });
  }



  onImageSelected(event: any): void {
    const file = event.target.files[0];
    if (!file) return;

    const reader = new FileReader();
    reader.onload = () => {
      this.selectedImageBase64 = (reader.result as string).split(',')[1];
      this.profileImageUrl = reader.result as string;
    };
    reader.readAsDataURL(file);
  }

}

