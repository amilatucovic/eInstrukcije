import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { StudentRoutingModule } from './student-routing.module';
import { StudentDashboardComponent } from './student-dashboard/student-dashboard.component';
import { TutorSearchComponent } from './tutor-search/tutor-search.component';
import {SharedModule} from '../shared/shared.module';


@NgModule({
  declarations: [
    StudentDashboardComponent,
    TutorSearchComponent
  ],
  imports: [
    CommonModule,
    StudentRoutingModule,
    ReactiveFormsModule,
    SharedModule
  ]
})
export class StudentModule { }
