import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { RouterLink } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { TutorSearchComponent } from '../student/tutor-search/tutor-search.component';
import { StudentHomeTabComponent } from '../student/student-home-tab/student-home-tab.component';
import { StudentClassesTabComponent } from '../student/student-classes-tab/student-classes-tab.component';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { StudentAccSettingsTabComponent } from '../student/student-acc-settings-tab/student-acc-settings-tab.component';
import { ChatComponent } from './chat/chat.component';

@NgModule({
  declarations: [
    UnauthorizedComponent,
    NavbarComponent,
    FooterComponent,
    TutorSearchComponent,
    StudentHomeTabComponent,
    StudentClassesTabComponent,
    StudentAccSettingsTabComponent,
    ChatComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterLink,
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    })
  ],
  exports: [
    UnauthorizedComponent,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NavbarComponent,
    FooterComponent,
    TutorSearchComponent,
    StudentHomeTabComponent,
    StudentClassesTabComponent,
    StudentAccSettingsTabComponent,
    ChatComponent
  ]
})
export class SharedModule {
}
