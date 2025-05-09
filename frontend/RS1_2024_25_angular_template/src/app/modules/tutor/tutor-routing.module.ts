import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {TutorDashboardComponent} from './tutor-dashboard/tutor-dashboard.component';

const routes: Routes = [
  { path: '', component: TutorDashboardComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TutorRoutingModule { }
