import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SharedModule} from '../shared/shared.module';
import {PublicRoutingModule} from './public-routing.module';
import {ContactUsComponent} from './contact-us/contact-us.component';
import {FormsModule} from '@angular/forms';
import {LandingPageComponent} from './landing-page/landing-page.component';
import { TutorRegistrationComponent } from './tutor-registration/tutor-registration.component';

@NgModule({
  declarations: [
    ContactUsComponent,
    LandingPageComponent,
    TutorRegistrationComponent
  ],
  imports: [
    CommonModule,
    PublicRoutingModule,
    FormsModule,
    SharedModule
  ],

})
export class PublicModule {
}
