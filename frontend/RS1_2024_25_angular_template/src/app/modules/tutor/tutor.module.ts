import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { TutorRoutingModule } from './tutor-routing.module';
import { TutorDashboardComponent } from './tutor-dashboard/tutor-dashboard.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import localeBs from '@angular/common/locales/bs';
import { TutorSidebarComponent } from './tutor-sidebar/tutor-sidebar.component';
import { TutorReservationsComponent } from './tutor-reservations/tutor-reservations.component';
import { FormsModule } from '@angular/forms';
import { TutorSubjectsComponent } from './tutor-subjects/tutor-subjects.component';
import {MatPaginatorModule} from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatPaginatorIntl } from '@angular/material/paginator';
import {getBosnianPaginatorIntl} from './bosnian-paginator-intl';
import { TutorProfileComponent } from './tutor-profile/tutor-profile.component';
import { ReactiveFormsModule } from '@angular/forms';
import {SharedModule} from '../shared/shared.module';

registerLocaleData(localeBs);

@NgModule({
  declarations: [
    TutorDashboardComponent,
    TutorSidebarComponent,
    TutorReservationsComponent,
    TutorSubjectsComponent,
    TutorProfileComponent
  ],
  exports : [TutorSidebarComponent],
    imports: [
        CommonModule,
      ReactiveFormsModule,
      MatFormFieldModule,
      SharedModule,
      MatSelectModule,
      MatPaginatorModule,
      FormsModule,
        TutorRoutingModule,
      CalendarModule.forRoot({
        provide: DateAdapter,
        useFactory: adapterFactory
      }),
    ],
  providers: [
    {
      provide: MatPaginatorIntl,
      useFactory: getBosnianPaginatorIntl
    }
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class TutorModule { }
