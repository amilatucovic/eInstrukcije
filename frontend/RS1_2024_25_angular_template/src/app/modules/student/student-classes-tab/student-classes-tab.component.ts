import { Component, OnInit } from '@angular/core';
import { LessonService, LessonSchedule } from '../../../services/auth-services/services/lesson.service';
import { format } from 'date-fns';

@Component({
  selector: 'app-student-classes-tab',
  templateUrl: './student-classes-tab.component.html',
  styleUrl: './student-classes-tab.component.css'
})
export class StudentClassesTabComponent implements OnInit {
  viewDate: Date = new Date();
  events: any[] = [];

  constructor(private lessonService: LessonService) { }

  ngOnInit(): void {
    const studentId = Number(localStorage.getItem('studentId'));
    console.log(studentId)
    this.lessonService.getLessonsForStudent(studentId).subscribe((lessons: LessonSchedule[]) => {
      this.events = lessons.map(lesson => ({
        start: new Date(lesson.start),
        end: new Date(lesson.end),
        title: `${lesson.subjectName} (${lesson.lessonMode})<br> Prof: ${lesson.tutorName}
        <br> Duration: ${format(new Date(lesson.start), 'HH:mm')} - ${format(new Date(lesson.end), 'HH:mm')} `,
        meta: lesson
      }));
    });
  }
}
