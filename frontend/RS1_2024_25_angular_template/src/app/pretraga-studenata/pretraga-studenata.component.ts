import { Component, OnInit } from '@angular/core';
import { CitiesService } from '../services/auth-services/services/cities.service';
import { City } from '../models/city.model';
import { Student } from '../models/student.model';
import { StudentService } from '../services/auth-services/services/students.service';
import { StudentUpdateDTO } from '../models/studentUpdate.model';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, filter } from 'rxjs/operators';

@Component({
  selector: 'app-pretraga-studenata',
  templateUrl: './pretraga-studenata.component.html',
  styleUrls: ['./pretraga-studenata.component.css']
})

export class PretragaStudenataComponent implements OnInit {
  firstName: string = '';
  lastName: string = ''
  cities: City[] = [];
  selectedGrade: string = '';
  selectedSchoolType: string = '';
  selectedCity?: City;
  students: Student[] = [];
  editingStudent: Student | null = null;
  originalStudent: Student | null = null;
  isEditing: boolean = false;

  searchTerm: Subject<string> = new Subject<string>();
  searchValue: string = '';

  showConfirmModal = false;
  showConfirmModalDeletion = false;
  studentToDeleteId: number | null = null;

  constructor(private citiesService: CitiesService, private studentService: StudentService) { }

  grades: string[] = ['Prvi', 'Drugi', 'Treći', 'Četvrti', 'Peti', 'Šesti', 'Sedmi', 'Osmi', 'Deveti'];
  schoolTypes: { name: string, value: number }[] = [{ name: 'Osnovna škola', value: 0 }, { name: 'Srednja škola', value: 1 }];

  ngOnInit(): void {
    this.searchTerm.pipe(
      debounceTime(300),
      map(term => term.trim().toLowerCase()),
      filter(term => term.length >= 3 || term.length === 0),
      distinctUntilChanged()
    ).subscribe(term => {
      this.searchValue = term;
      this.filterStudents();
    });
    this.citiesService.getCities().subscribe(
      (data) => {
        this.cities = data.map(cityData => new City(cityData.id, cityData.name, cityData.postalCode));
      },
      (error) => {
        console.error('Greška pri učitavanju gradova', error);
      }
    );

    this.studentService.getStudents({ IsUserIncluded: true }).subscribe({
      next: (students: Student[]) => {
        this.students = students;
      },
      error: (error: any) => console.error("Greška pri učitavanju studenta: ", error)
    });
  }

  filterStudents() {
    if (!this.searchValue && this.selectedCity?.id == 0 && this.selectedGrade == "" && this.selectedSchoolType == null) return;
    var filterObject = {
      IsUserIncluded: true,
      searchTerm: this.searchValue,
      CityId: this.selectedCity,
      Grade: this.selectedGrade,
      EducationLevel: this.selectedSchoolType,
    }
    this.studentService.getStudents(filterObject).subscribe({
      next: (students: Student[]) => this.students = students,
      error: (error: any) => console.error("Greška pri učitavanju studenta: ", error)
    });
  }

  editStudent(student: any) {
    if (student && student.myAppUser) {
      // Kreiraj duboku kopiju originalnog studenta
      this.originalStudent = JSON.parse(JSON.stringify(student));
      // Kreiraj radnu kopiju za editovanje
      this.editingStudent = JSON.parse(JSON.stringify(student));
      this.isEditing = true;
    } else {
      console.error("Student nije validan ili nema podatke", student);
    }
  }

  cancelEdit() {
    this.showConfirmModal = true;
  }

  deleteStudent(studentId: number) {
    this.studentToDeleteId = studentId;
    this.showConfirmModalDeletion = true;
  }

  confirmDeletion() {
    if (this.studentToDeleteId !== null) {
      this.students = this.students.filter(student => student.id !== this.studentToDeleteId);
      this.studentService.delete(this.studentToDeleteId).subscribe();
      this.studentToDeleteId = null;
      this.showConfirmModalDeletion = false;
    }
  }

  cancelDelete() {
    this.studentToDeleteId = null;
    this.showConfirmModalDeletion = false;
  }

  confirmClose() {
    if (this.originalStudent) {
      const index = this.students.findIndex(s => s.id === this.originalStudent!.id);
      if (index !== -1) {
        this.students[index] = this.originalStudent;
      }
    }

    this.showConfirmModal = false;
    this.isEditing = false;
    this.editingStudent = null;
    this.originalStudent = null;
  }

  cancelClose() {
    this.showConfirmModal = false;
  }

  onOverlayClick(event: Event) {
    if (event.target === event.currentTarget) {
      this.cancelClose();
    }
  }

  saveStudent() {
    if (this.editingStudent) {
      const selectedCity = this.cities.find(city => city.id == this.editingStudent!.myAppUser.city.id);

      const podaci: StudentUpdateDTO = {
        id: this.editingStudent.id,
        email: this.editingStudent.myAppUser.email,
        cityId: this.editingStudent.myAppUser.city.id,
        grade: this.editingStudent.grade,
        educationLevel: this.editingStudent.educationLevel,
        phoneNumber: this.editingStudent.myAppUser.phoneNumber,
        username: this.editingStudent.myAppUser.username,
        firstName: this.editingStudent.myAppUser.firstName,
        lastName: this.editingStudent.myAppUser.lastName,
        preferredMode: this.editingStudent.preferredMode,
      }

      this.studentService.update(this.editingStudent.id, podaci).subscribe(
        (updatedStudent) => {
          const index = this.students.findIndex(s => s.id === this.editingStudent!.id);
          if (index !== -1) {
            if (selectedCity) {
              this.editingStudent!.myAppUser.city = selectedCity;
            }
            this.students[index] = { ...this.editingStudent! };
          }

          this.isEditing = false;
          this.editingStudent = null;
          this.originalStudent = null;
        },
        (error) => console.error('Greška pri ažuriranju studenta', error)
      );
    }
  }

  formatEducationlevel(level: number) { return level == 0 ? "Osnovna škola" : "Srednja škola" }

  clearSearch() {
    this.searchValue = '';
    this.searchTerm.next('');
    this.selectedCity = undefined;
    this.selectedGrade = '';
    this.selectedSchoolType = '';
    this.filterStudents();
  }
}