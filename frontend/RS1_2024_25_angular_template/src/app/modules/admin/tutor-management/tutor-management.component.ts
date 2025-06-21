import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { debounceTime, distinctUntilChanged, map, filter } from 'rxjs/operators';
import { TutorAdmin, TutorAdminService } from '../../../services/auth-services/services/tutor-admin.service';
import { CitiesService } from '../../../services/auth-services/services/cities.service';
import { City } from '../../../models/city.model';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-tutor-management',
  templateUrl: './tutor-management.component.html',
  styleUrls: ['./tutor-management.component.css']
})
export class TutorManagementComponent implements OnInit {
  filterForm: FormGroup;
  editForm: FormGroup;

  tutors: TutorAdmin[] = [];
  filteredTutors: TutorAdmin[] = [];
  cities: City[] = [];

  tutorToDeleteId: number | null = null;
  selectedTutorId: number = 0;

  @ViewChild('editModal') editModalRef!: TemplateRef<any>;

  constructor(
    private fb: FormBuilder,
    private tutorService: TutorAdminService,
    private modalService: NgbModal,
    private cityService: CitiesService
  ) {

    this.filterForm = this.fb.group({
      searchName: [''],
      searchEmail: [''],
      cityId: [''],
      isLiveAvailable: [''],
      maxHourlyRate: ['']
    });


    this.editForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      cityId: [null, Validators.required],
      qualifications: ['', Validators.required],
      yearsOfExperience: [0, Validators.required],
      availability: [''],
      policy: [''],
      isLiveAvailable: [null]
    });
  }

  ngOnInit(): void {
    this.fetchTutors();
    this.fetchCities();

    this.filterForm.valueChanges
      .pipe(
        debounceTime(300),
        map((form: any) => ({
          ...form,
          searchName: form.searchName?.trim().toLowerCase() ?? '',
          searchEmail: form.searchEmail?.trim().toLowerCase() ?? ''
        })),
        filter((form: any) =>
          form.searchName.length > 0 ||
          form.searchEmail.length > 0 ||
          form.cityId ||
          form.isLiveAvailable !== '' ||
          form.maxHourlyRate
        ),
        distinctUntilChanged((prev, curr) => JSON.stringify(prev) === JSON.stringify(curr))
      )
      .subscribe(() => {
        this.applyFilters();
      });
  }

  fetchTutors(): void {
    this.tutorService.getAllTutors().subscribe({
      next: data => {
        this.tutors = data;
        this.applyFilters();
      },
      error: err => {
        console.error('Failed to fetch tutors', err);
      }
    });
  }

  fetchCities(): void {
    this.cityService.getCities().subscribe({
      next: data => this.cities = data,
      error: err => console.error('Failed to fetch cities', err)
    });
  }

  resetFilters(): void {
    this.filterForm.reset({
      searchName: '',
      searchEmail: '',
      cityId: '',
      isLiveAvailable: '',
      maxHourlyRate: ''
    });
    this.applyFilters();
  }

  applyFilters(): void {
    const { searchName, searchEmail, cityId, isLiveAvailable, maxHourlyRate } = this.filterForm.value;

    this.filteredTutors = this.tutors.filter(tutor => {
      const matchesName = searchName
        ? [tutor.firstName, tutor.lastName].some(field =>
          field?.toLowerCase().includes(searchName.toLowerCase()))
        : true;

      const matchesEmail = searchEmail
        ? tutor.email.toLowerCase().includes(searchEmail.toLowerCase())
        : true;

      const matchesCity = cityId
        ? tutor.cityName?.toLowerCase() === this.cities.find(c => c.id == cityId)?.name.toLowerCase()
        : true;

      const matchesLive = isLiveAvailable !== ''
        ? (tutor.isLiveAvailable ?? false) === isLiveAvailable
        : true;

      const matchesRate = maxHourlyRate
        ? parseFloat(tutor.hourlyRate) <= parseFloat(maxHourlyRate)
        : true;

      return matchesName && matchesEmail && matchesCity && matchesLive && matchesRate;
    });
  }

  openDeleteModal(tutorId: number, content: any): void {
    this.tutorToDeleteId = tutorId;
    this.modalService.open(content, { centered: true });
  }

  confirmDelete(modalRef: NgbModalRef): void {
    if (this.tutorToDeleteId !== null) {
      this.tutorService.deleteTutor(this.tutorToDeleteId).subscribe({
        next: () => {
          this.fetchTutors();
          modalRef.close();
        },
        error: err => {
          console.error('Delete failed', err);
          modalRef.dismiss();
        }
      });
    }
  }

  openEditModal(tutor: TutorAdmin): void {
    this.selectedTutorId = tutor.id;

    this.editForm.patchValue({
      firstName: tutor.firstName,
      lastName: tutor.lastName,
      phoneNumber: tutor.phoneNumber,
      email: tutor.email,
      cityId: this.cities.find(c => c.name === tutor.cityName)?.id ?? null,
      qualifications: tutor.qualifications,
      yearsOfExperience: tutor.yearsOfExperience,
      availability: tutor.availability,
      policy: tutor.policy,
      isLiveAvailable: tutor.isLiveAvailable
    });

    this.modalService.open(this.editModalRef, { size: 'lg', centered: true });
  }

  submitEdit(modal: NgbModalRef): void {
    if (this.editForm.invalid) return;

    const formValue = this.editForm.value;

    const payload = {
      firstName: formValue.firstName?.trim() || null,
      lastName: formValue.lastName?.trim() || null,
      phoneNumber: formValue.phoneNumber?.trim() || null,
      email: formValue.email?.trim() || null,
      cityId: formValue.cityId ?? null,
      qualifications: formValue.qualifications?.trim() || null,
      yearsOfExperience: formValue.yearsOfExperience ?? null,
      availability: formValue.availability?.trim() || null,
      policy: formValue.policy?.trim() || null,
      isLiveAvailable: formValue.isLiveAvailable
    };

    this.tutorService.updateTutor(this.selectedTutorId, payload).subscribe({
      next: () => {
        this.fetchTutors();
        modal.close();
      },
      error: err => {
        console.error("Greška pri editovanju tutora:", err);
      }
    });
  }


}
