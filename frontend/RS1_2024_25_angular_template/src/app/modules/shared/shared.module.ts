import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { RouterLink } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { TutorSearchComponent } from '../student/tutor-search/tutor-search.component';

@NgModule({
  declarations: [
    UnauthorizedComponent,
    NavbarComponent,
    FooterComponent, // Dodajemo UnauthorizedComponent u deklaracije,
    TutorSearchComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterLink
  ],
  exports: [
    UnauthorizedComponent, // Omogućavamo ponovno korištenje UnauthorizedComponent
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NavbarComponent,
    FooterComponent,
    TutorSearchComponent
  ]
})
export class SharedModule {
}
