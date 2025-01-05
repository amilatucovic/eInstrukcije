import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TutorRoutingModule } from './tutor-routing.module';
import { TutorDashboardComponent } from './tutor-dashboard/tutor-dashboard.component';


@NgModule({
  declarations: [
    TutorDashboardComponent
  ],
  imports: [
    CommonModule,
    TutorRoutingModule
  ]
})
export class TutorModule { }
