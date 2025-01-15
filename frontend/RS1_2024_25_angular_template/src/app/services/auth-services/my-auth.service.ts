import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MyAuthService {

  getUserRole(): string {
    const token = localStorage.getItem('accessToken');
    if (!token) return '';

    const payload = this.parseToken(token);
    return payload ? payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] : '';
  }

  hasRole(requiredRole: string): boolean {
    return this.getUserRole() === requiredRole;
  }

  private parseToken(token: string): any {
    try {
      const payload = token.split('.')[1];
      return JSON.parse(atob(payload));
    } catch (error) {
      console.error('Error parsing token:', error);
      return null;
    }
  }

  setLoggedInUser(user: any): void {
    if (user) {
      localStorage.setItem('loggedInUser', JSON.stringify(user));
    } else {
      localStorage.removeItem('loggedInUser');
    }
  }

  getLoginToken(): { token: string } | null {
    const token = localStorage.getItem('accessToken');
    return token ? { token } : null;
  }
}
