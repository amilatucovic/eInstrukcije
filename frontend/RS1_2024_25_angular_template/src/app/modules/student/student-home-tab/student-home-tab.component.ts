import { Component, OnInit } from '@angular/core';
import { MyAppUser } from '../../../models/myAppUser.model';
import { MyAuthService } from '../../../services/auth-services/my-auth.service';

@Component({
  selector: 'app-student-home-tab',
  templateUrl: './student-home-tab.component.html',
  styleUrl: './student-home-tab.component.css'
})
export class StudentHomeTabComponent implements OnInit {
  user: MyAppUser | null = null;

  constructor(private myAuth: MyAuthService) { }

  ngOnInit(): void {
    this.user = this.myAuth.getLoggedInUser();
  }
}
