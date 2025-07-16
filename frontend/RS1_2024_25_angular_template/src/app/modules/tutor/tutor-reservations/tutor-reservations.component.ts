import { Component, OnInit } from '@angular/core';
import {ReservationService, ReservationDto} from '../../../services/auth-services/services/reservation.service';
import {ReservationStatus} from '../../../models/reservation-status.enum';

@Component({
  selector: 'app-tutor-reservations',
  templateUrl: './tutor-reservations.component.html',
  styleUrls: ['./tutor-reservations.component.css']
})
export class TutorReservationsComponent implements OnInit {
  reservations: ReservationDto[] = [];
  selectedStatus: string = '';
  selectedDate: string = '';
  searchTerm: string = '';
  tutorId: number = 0;

  ReservationStatus = ReservationStatus;

  ngOnInit(): void {
    const storedId = localStorage.getItem('tutorId');
    if (!storedId) {
      console.error('Tutor ID not found in localStorage.');
      return;
    }

    this.tutorId = +storedId;
    this.loadReservations();
  }

  constructor(private reservationService: ReservationService) {}

  loadReservations(): void {
    this.reservationService
      .getTutorReservations(this.tutorId, this.selectedStatus, this.selectedDate, this.searchTerm)
      .subscribe({
        next: (data) => (this.reservations = data),
        error: (err) => console.error('Failed to load reservations', err)
      });
  }

  approve(id: number): void {
    this.reservationService.approveReservation(id).subscribe(() => this.loadReservations());
  }

  reject(id: number): void {
    this.reservationService.rejectReservation(id).subscribe(() => this.loadReservations());
  }

  onFilterChange(): void {
    this.loadReservations();
  }

  getStatusLabel(status: number): string {
    switch (status) {
      case 0:
        return 'Na čekanju';
      case 1:
        return 'Odobrena';
      case 2:
        return 'Plaćena';
      case 3:
        return 'Odbijena';
      default:
        return 'Nepoznat';
    }
  }


}

