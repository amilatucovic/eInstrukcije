import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {

  constructor(private router: Router) { }

  showConfirmModal = false;

  openModal() {
    this.showConfirmModal = true;
  }

  confirmClose() {
    this.showConfirmModal = false;
    this.router.navigate(['/']);
  }

  cancelClose() {
    this.showConfirmModal = false;
  }

  onOverlayClick(event: Event) {
    if (event.target === event.currentTarget) {
      this.cancelClose();
    }
  }
}
