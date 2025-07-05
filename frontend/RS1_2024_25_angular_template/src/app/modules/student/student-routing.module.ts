import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentDashboardComponent } from './student-dashboard/student-dashboard.component';
import { TutorSearchComponent } from './tutor-search/tutor-search.component';
import { ChatComponent } from '../shared/chat/chat.component';
import { StudentClassesTabComponent } from './student-classes-tab/student-classes-tab.component';
import { StudentHomeTabComponent } from './student-home-tab/student-home-tab.component';
import { StudentAccSettingsTabComponent } from './student-acc-settings-tab/student-acc-settings-tab.component';
import { StudentPaymentsTabComponent } from './student-payments-tab/student-payments-tab.component';
const routes: Routes = [
  {
    path: '',
    component: StudentDashboardComponent,
    children: [
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: StudentHomeTabComponent },
      { path: 'classes', component: StudentClassesTabComponent },
      { path: 'tutors', component: TutorSearchComponent },
      { path: 'account', component: StudentAccSettingsTabComponent },
      { path: 'chat', component: ChatComponent },
      { path: 'payments', component: StudentPaymentsTabComponent }

    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StudentRoutingModule { }
