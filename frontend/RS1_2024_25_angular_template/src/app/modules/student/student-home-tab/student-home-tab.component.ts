import { Component, OnInit } from '@angular/core';
import { MyAuthService } from '../../../services/auth-services/my-auth.service';
import { Student } from '../../../models/student.model';

@Component({
  selector: 'app-student-home-tab',
  templateUrl: './student-home-tab.component.html',
  styleUrl: './student-home-tab.component.css'
})
export class StudentHomeTabComponent implements OnInit {
  user: Student | null = null;

  constructor(private myAuth: MyAuthService) { }

  ngOnInit(): void {
    this.user = this.myAuth.getLoggedInUser() as Student;
  }
}
