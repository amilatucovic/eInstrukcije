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

@NgModule({
  declarations: [
    UnauthorizedComponent,
    NavbarComponent,
    FooterComponent, // Dodajemo UnauthorizedComponent u deklaracije,
    TutorSearchComponent,
    StudentHomeTabComponent,
    StudentClassesTabComponent
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
    UnauthorizedComponent, // Omogućavamo ponovno korištenje UnauthorizedComponent
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NavbarComponent,
    FooterComponent,
    TutorSearchComponent,
    StudentHomeTabComponent,
    StudentClassesTabComponent
  ]
})
export class SharedModule {
}
