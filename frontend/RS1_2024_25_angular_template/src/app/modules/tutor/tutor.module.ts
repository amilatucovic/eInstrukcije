import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TutorRoutingModule } from './tutor-routing.module';
import { TutorDashboardComponent } from './tutor-dashboard/tutor-dashboard.component';
import {CalendarWeekModule} from "angular-calendar";


@NgModule({
  declarations: [
    TutorDashboardComponent
  ],
    imports: [
        CommonModule,
        TutorRoutingModule,
        CalendarWeekModule
    ]
})
export class TutorModule { }
