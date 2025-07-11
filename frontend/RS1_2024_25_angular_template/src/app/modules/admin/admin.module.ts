import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AdminRoutingModule} from './admin-routing.module';
import {DashboardComponent} from './dashboard/dashboard.component';
import {AdminLayoutComponent} from './admin-layout/admin-layout.component';
import {FormsModule} from '@angular/forms';
import {SharedModule} from '../shared/shared.module';
import { TutorManagementComponent } from './tutor-management/tutor-management.component';


@NgModule({
  declarations: [
    DashboardComponent,
    AdminLayoutComponent,
    TutorManagementComponent,
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    FormsModule,
    SharedModule // Omogućava pristup svemu što je eksportovano iz SharedModule
  ],
  providers: []
})
export class AdminModule {
}
