import { Component, OnInit } from '@angular/core';
import {SubjectService, Subject, Category} from '../../../services/auth-services/services/subject.service';
import { TutorSubjectCategoryService} from '../../../services/auth-services/services/tutor-subject-category.service';
import { forkJoin } from 'rxjs';
import {TutorSubjectCategoryEntry} from '../../../services/auth-services/services/tutor-subject-category.service';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import {ConfirmDialogComponent} from '../../shared/confirm-dialog/confirm-dialog.component';


@Component({
  selector: 'app-tutor-subjects',
  templateUrl: './tutor-subjects.component.html',
  styleUrls: ['./tutor-subjects.component.css']
})
export class TutorSubjectsComponent implements OnInit {
  subjects: Subject[] = [];
  categories: Category[] = [];
  selectedSubjectId: number | null = null;
  tutorId:number = 0;
  page: number = 1;
  pageSize: number = 5;
  totalCount: number = 0;
  currentSelections: any[] = [];
  categorySelection: { [key: number]: boolean } = {};
  subjectsByDifficulty: { [key: string]: Subject[] } = {};
  showConfirmModal: boolean = false;
  confirmMessage: string = '';
  pendingDeleteEntry: any = null;
  confirmTitle: string = '';
  confirmIcon: string = 'bi-trash-fill';
  confirmColorClass: string = 'bg-danger-light';



  constructor(private subjectService: SubjectService, private tutorService: TutorSubjectCategoryService, private dialog: MatDialog) {}

  ngOnInit(): void {
    const storedId = localStorage.getItem('tutorId');
    if (storedId) {
      this.tutorId = +storedId;
    } else {
      console.error("Tutor ID not found in localStorage!");
      return;
    }

    this.subjectService.getAllSubjects().subscribe(subjects => {
      this.subjects = subjects;
      this.subjectsByDifficulty = subjects.reduce((acc, subject) => {
        const level = subject.difficultyLevel;
        if (!acc[level]) {
          acc[level] = [];
        }
        acc[level].push(subject);
        return acc;
      }, {} as { [key: string]: Subject[] });
      this.loadCurrentSelections();
    });

  }

  onSubjectChange(): void {
    if (!this.selectedSubjectId) return;

    this.subjectService.getCategoriesForSubject(this.selectedSubjectId).subscribe({
      next: (data) => {
        this.categories = data;
        this.categorySelection = {};

        const existing = this.currentSelections.filter(sel => sel.SubjectID === this.selectedSubjectId);

        for (let cat of data) {
          const alreadySelected = existing.some(sel => sel.CategoryID === cat.id);
          if (!alreadySelected) {
            this.categorySelection[cat.id] = false;
          }
        }

        if (data.length > 0 && Object.keys(this.categorySelection).length === 0) {
          alert("Sve kategorije za ovaj predmet su već dodane.");
          this.selectedSubjectId = null;
          this.categories = [];
        }

      },
      error: () => this.categories = []
    });

  }

  saveSelections(): void {
    if (!this.selectedSubjectId) return;

    const selectedIds = Object.keys(this.categorySelection)
      .filter(id => this.categorySelection[+id])
      .map(id => +id);

    const tasks = this.categories.length > 0
      ? selectedIds.map(cid => this.tutorService.add({
        tutorID: this.tutorId,
        subjectID: this.selectedSubjectId!,
        categoryID: cid
      }))
      : [this.tutorService.add({
        tutorID: this.tutorId,
        subjectID: this.selectedSubjectId!,
        categoryID: null
      })];

    forkJoin(tasks).subscribe(() => {
      this.loadCurrentSelections();


      this.selectedSubjectId = null;
      this.categories = [];
      this.categorySelection = {};
    });
  }




  loadCurrentSelections(): void {
    this.tutorService.getByTutorIdPaged(this.tutorId, this.page, this.pageSize).subscribe((response) => {
      this.totalCount = response.totalCount;
      this.currentSelections = response.items.map((entry: TutorSubjectCategoryEntry) => {
        const subject = this.subjects.find(s => s.id === entry.SubjectID);
        return {
          ...entry,
          difficultyLevel: subject?.difficultyLevel || ''
        };
      });
    });
  }



  removeSelection(entry: any): void {
    this.confirmMessage = `Da li ste sigurni da želite ukloniti predmet ${entry.subjectName}${entry.categoryName ? ' - ' + entry.categoryName : ''}?`;
    this.confirmTitle = 'Potvrda uklanjanja';
    this.confirmIcon = 'bi-trash-fill';
    this.confirmColorClass = 'bg-danger-light';
    this.pendingDeleteEntry = entry;
    this.showConfirmModal = true;
  }


  handleCancelDelete(): void {
    this.showConfirmModal = false;
    this.pendingDeleteEntry = null;
  }

  handleConfirmDelete(): void {
    if (this.pendingDeleteEntry) {
      const categoryId = this.pendingDeleteEntry.categoryID === null ? 0 : this.pendingDeleteEntry.categoryID;
      this.tutorService.delete(this.pendingDeleteEntry.tutorID, this.pendingDeleteEntry.subjectID, categoryId).subscribe(() => {
        this.loadCurrentSelections();
        this.showConfirmModal = false;
        this.pendingDeleteEntry = null;
      });
    }
  }


  getDifficultyLevels(): string[] {
    return Object.keys(this.subjectsByDifficulty);
  }

  getDifficultyLabel(level: string): string {
    switch (level.toLowerCase()) {
      case 'osnovna skola':
        return 'Osnovna škola';
      case 'srednja skola':
        return 'Srednja škola';
      default:
        return level;
    }
  }
  getDifficultyLevelBySubjectId(subjectId: number): string {
    const subject = this.subjects.find(s => s.id === subjectId);
    return subject ? this.getDifficultyLabel(subject.difficultyLevel) : '';
  }


  onPageChange(event: PageEvent): void {
    this.pageSize = event.pageSize;
    this.page = event.pageIndex + 1; // Angular paginator koristi 0-based index
    this.loadCurrentSelections();
  }



}
