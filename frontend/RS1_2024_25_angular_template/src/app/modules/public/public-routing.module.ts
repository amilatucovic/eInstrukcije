import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { TutorRegistrationComponent } from './tutor-registration/tutor-registration.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { TutorSearchComponent } from './tutor-search/tutor-search.component';
import { ContactUsComponent } from './contact-us/contact-us.component';

const routes: Routes = [
  {
    path: '',
    component: LandingPageComponent,
    children: [
      { path: '', redirectTo: 'landing-page', pathMatch: 'full' }, // Redirekcija na landing page


    ]
  },
  { path: 'tutor-registration', component: TutorRegistrationComponent },
  { path: 'about-us', component: AboutUsComponent },
  { path: 'tutor-search', component: TutorSearchComponent },
  { path: 'contact-us', component: ContactUsComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule { }
