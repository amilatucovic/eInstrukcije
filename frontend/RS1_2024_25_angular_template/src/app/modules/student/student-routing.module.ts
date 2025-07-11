import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentDashboardComponent } from './student-dashboard/student-dashboard.component';
import { TutorSearchComponent } from './tutor-search/tutor-search.component';
import {ChatComponent} from '../shared/chat/chat.component';

const routes: Routes = [
  { path: '', component: StudentDashboardComponent },
  { path: 'tutor-search', component: TutorSearchComponent },
  {
    path: 'chat',
    component: ChatComponent
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StudentRoutingModule { }
