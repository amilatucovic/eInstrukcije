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
  showModal: boolean = false;
  modalMessage: string = '';
  pendingAction: 'approve' | 'reject' | null = null;
  pendingReservationId: number | null = null;
  modalTitle = '';
  modalIcon = 'bi-trash-fill';
  modalColor = 'bg-danger';

  selectedReservationId: number = 0;
  actionType: 'approve' | 'reject' | 'delete' = 'delete';

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

  openConfirmation(id: number, action: 'approve' | 'reject'): void {
    this.pendingReservationId = id;
    this.pendingAction = action;

    if (action === 'approve') {
      this.modalTitle = 'Potvrda odobravanja';
      this.modalMessage = 'Da li ste sigurni da želite ODOBRITI ovu rezervaciju?';
      this.modalIcon = 'bi-check-circle';
      this.modalColor = 'bg-success-light';
    } else {
      this.modalTitle = 'Potvrda odbijanja';
      this.modalMessage = 'Da li ste sigurni da želite ODBITI ovu rezervaciju?';
      this.modalIcon = 'bi-exclamation-circle';
      this.modalColor = 'bg-warning-light';
    }

    this.showModal = true;
  }


  onModalConfirm(): void {
    if (!this.pendingReservationId || !this.pendingAction) return;

    const action = this.pendingAction;
    const id = this.pendingReservationId;

    const call$ = action === 'approve'
      ? this.reservationService.approveReservation(id)
      : this.reservationService.rejectReservation(id);

    call$.subscribe(() => {
      this.loadReservations();
      this.resetModal();
    });
  }

  onModalCancel(): void {
    this.resetModal();
  }

  resetModal(): void {
    this.showModal = false;
    this.pendingAction = null;
    this.pendingReservationId = null;
    this.modalMessage = '';
    this.modalTitle = '';
    this.modalIcon = 'bi-trash-fill';
    this.modalColor = 'bg-danger';
  }




}

