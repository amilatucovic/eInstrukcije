import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TutorDashboardComponent } from './tutor-dashboard/tutor-dashboard.component';
import { ChatComponent } from '../shared/chat/chat.component';
import { TutorSidebarComponent } from './tutor-sidebar/tutor-sidebar.component';

const routes: Routes = [
  {
    path: '',
    component: TutorSidebarComponent,
    children: [
      { path: '', component: TutorDashboardComponent },
      { path: 'tutor-messages', component: ChatComponent }

    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TutorRoutingModule {}

