import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UnauthorizedComponent } from './modules/shared/unauthorized/unauthorized.component';
import { AuthGuard } from './auth-guards/auth-guard.service';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: '', component: LandingPageComponent },
  { path: 'unauthorized', component: UnauthorizedComponent },
  { path: 'login', component: LoginComponent },
  {
    path: 'admin',
    canActivate: [AuthGuard],
    data: { isAdmin: true },
    loadChildren: () => import('./modules/admin/admin.module').then(m => m.AdminModule)
  },
  {
    path: 'student-dashboard',
    canActivate: [AuthGuard],
    data: { isStudent: true }, // Add this data to restrict route to students if needed
    loadChildren: () => import('./modules/student/student.module').then(m => m.StudentModule)
  },
  {
    path: 'public',
    loadChildren: () => import('./modules/public/public.module').then(m => m.PublicModule)
  },
  {
    path: 'client',
    loadChildren: () => import('./modules/client/client.module').then(m => m.ClientModule)
  },
  {
    path: 'auth',
    loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: 'tutor-dashboard',
    canActivate: [AuthGuard],
    data: {isTutor: true},
    loadChildren: () => import('./modules/tutor/tutor.module').then(m => m.TutorModule)
  },
  { path: '**', redirectTo: 'public', pathMatch: 'full' } // Default route
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
