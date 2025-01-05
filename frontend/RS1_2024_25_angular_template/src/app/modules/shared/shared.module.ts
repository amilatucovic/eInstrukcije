import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {UnauthorizedComponent} from './unauthorized/unauthorized.component';
import {RouterLink} from '@angular/router';
import { TutorSearchComponent } from './tutor-search/tutor-search.component';

@NgModule({
  declarations: [
    UnauthorizedComponent,
    TutorSearchComponent, // Dodajemo UnauthorizedComponent u deklaracije
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
    ReactiveFormsModule
  ]
})
export class SharedModule {
}
