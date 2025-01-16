import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { StudentRoutingModule } from './student-routing.module';
import { StudentDashboardComponent } from './student-dashboard/student-dashboard.component';
import { TutorSearchComponent } from './tutor-search/tutor-search.component';
<<<<<<< Updated upstream
import {SharedModule} from '../shared/shared.module';
=======
>>>>>>> Stashed changes


@NgModule({
  declarations: [
    StudentDashboardComponent,
    TutorSearchComponent
  ],
  imports: [
    CommonModule,
    StudentRoutingModule,
<<<<<<< Updated upstream
    ReactiveFormsModule,
    SharedModule
=======
    ReactiveFormsModule
>>>>>>> Stashed changes
  ]
})
export class StudentModule { }
