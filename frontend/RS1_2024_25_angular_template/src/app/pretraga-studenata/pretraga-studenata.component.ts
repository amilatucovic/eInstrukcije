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
  isEditing: boolean = false;

  searchTerm: Subject<string> = new Subject<string>();
  searchValue: string = '';

  constructor(private citiesService: CitiesService, private studentService: StudentService) { }

  grades: string[] = ['Prvi', 'Drugi', 'Treći', 'Četvrti', 'Peti', 'Šesti', 'Sedmi', 'Osmi', 'Deveti'];
  schoolTypes: { name: string, value: number }[] = [{ name: 'Osnovna škola', value: 0 }, { name: 'Srednja škola', value: 1 }];

  ngOnInit(): void {

    this.searchTerm.pipe(
      debounceTime(300),
      map(term => term.trim().toLowerCase()), // mapiranje unosa na trimovani i lowercase string
      filter(term => term.length >= 3 || term.length === 0),         // filtriranje praznih stringova
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

  // Filtriranje studenata prema pretrazi
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
    // Provjeravamo da li student sadrži podatke
    if (student && student.myAppUser) {
      this.editingStudent = { ...student, city: student.myAppUser.city };
      this.isEditing = true;
    } else {
      console.error("Student nije validan ili nema podatke", student);
    }
  }

  cancelEdit() {
    this.isEditing = false;
    this.editingStudent = null;
    location.reload;
  }

  deleteStudent(studentId: number) {
    this.students = this.students.filter((student: Student) => student.id != studentId);
    this.studentService.delete(studentId).subscribe();
  }

  saveStudent() {
    if (this.editingStudent) {
      const podaci: StudentUpdateDTO = {
        id: this.editingStudent.id,
        searchTerm: this.editingStudent.myAppUser.firstName,
        email: this.editingStudent.myAppUser.email,
        cityId: this.editingStudent.myAppUser.city.id,
        grade: this.editingStudent.grade,
        educationLevel: this.editingStudent.educationLevel
      }
      this.studentService.update(this.editingStudent.id, podaci).subscribe(
        () => location.reload(),
        (error) => console.error('Greška pri ažuriranju studenta', error)
      );
    }
  }

  formatEducationlevel(level: number) { return level == 0 ? "Osnovna škola" : "Srednja škola" }

  // Resetovanje filtera
  clearSearch() {
    this.searchValue = '';
    this.searchTerm.next('');
    this.selectedCity = undefined;
    this.selectedGrade = '';
    this.selectedSchoolType = '';
    this.filterStudents();
  }
}