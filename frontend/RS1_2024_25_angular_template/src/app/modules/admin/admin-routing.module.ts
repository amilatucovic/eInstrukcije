import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import {TutorManagementComponent} from './tutor-management/tutor-management.component';


const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'tutors', component: TutorManagementComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule {
}
