import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { MyAuthService } from '../services/auth-services/my-auth.service';

interface AuthGuardData {
  isAdmin?: boolean;
  isTutor?: boolean;
  isStudent?: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: MyAuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const guardData = route.data as AuthGuardData;

    if (guardData.isAdmin && !this.authService.hasRole('Admin')) {
      this.router.navigate(['/unauthorized']);
      return false;
    }

    if (guardData.isTutor && !this.authService.hasRole('Tutor')) {
      this.router.navigate(['/unauthorized']);
      return false;
    }

    if (guardData.isStudent && !this.authService.hasRole('Student')) {
      this.router.navigate(['/unauthorized']);
      return false;
    }

    return true;
  }
}
