import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TutorDashboardComponent } from './tutor-dashboard/tutor-dashboard.component';
import { ChatComponent } from '../shared/chat/chat.component';
import { TutorSidebarComponent } from './tutor-sidebar/tutor-sidebar.component';
import {TutorReservationsComponent} from './tutor-reservations/tutor-reservations.component';
import {TutorSubjectsComponent} from './tutor-subjects/tutor-subjects.component';
import {TutorProfileComponent} from './tutor-profile/tutor-profile.component';

const routes: Routes = [
  {
    path: '',
    component: TutorSidebarComponent,
    children: [
      { path: '', component: TutorDashboardComponent },
      { path: 'tutor-messages', component: ChatComponent },
      { path: 'reservations', component: TutorReservationsComponent },
      { path: 'tutor-lessons', component: TutorSubjectsComponent },
      { path: 'tutor-profile', component: TutorProfileComponent },

    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TutorRoutingModule {}

