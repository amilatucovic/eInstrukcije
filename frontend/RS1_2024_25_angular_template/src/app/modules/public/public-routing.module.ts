import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { TutorRegistrationComponent } from './tutor-registration/tutor-registration.component';

const routes: Routes = [
  {
    path: '',
    component: LandingPageComponent,
    children: [
      { path: '', redirectTo: 'landing-page', pathMatch: 'full' }, // Redirekcija na landing page

    ]
  },
  { path: 'tutor-registration', component: TutorRegistrationComponent }, // Posebna ruta
  { path: '**', redirectTo: '', pathMatch: 'full' }
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule {}
