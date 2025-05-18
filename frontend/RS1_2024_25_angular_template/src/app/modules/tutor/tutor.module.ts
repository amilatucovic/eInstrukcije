import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { TutorRoutingModule } from './tutor-routing.module';
import { TutorDashboardComponent } from './tutor-dashboard/tutor-dashboard.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import localeBs from '@angular/common/locales/bs';

registerLocaleData(localeBs);

@NgModule({
  declarations: [
    TutorDashboardComponent
  ],
    imports: [
        CommonModule,
        TutorRoutingModule,
      CalendarModule.forRoot({
        provide: DateAdapter,
        useFactory: adapterFactory
      }),
    ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class TutorModule { }
