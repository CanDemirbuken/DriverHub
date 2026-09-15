import { Component, input, output, signal } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { finalize } from 'rxjs';
import { ReservationResponse, ReservationStatus } from '../../../core/services/reservation/models/reservation-response';
import { ReservationService } from '../../../core/services/reservation/reservation-service';
import { ApiResponse } from '../../../core/models/api/api-response';
import { ToastService } from '../../../shared/services/toast-service';

@Component({
  selector: 'app-reservation-actions',
  templateUrl: './reservation-actions.html',
  styleUrl: '../reservation-management.scss'
})
export class ReservationActions {
  readonly reservation = input.required<ReservationResponse>();
  readonly disabled = input(false);
  readonly changed = output<void>();
  readonly busyChange = output<boolean>();
  readonly pending = ReservationStatus.Pending;
  readonly action = signal<'approve' | 'cancel' | null>(null);
  readonly isSaving = signal(false);

  constructor(private readonly service: ReservationService, private readonly toast: ToastService) {}

  confirm(): void {
    const action = this.action();
    if (!action || this.isSaving() || this.disabled() || this.reservation().status !== this.pending) return;
    this.isSaving.set(true);
    this.busyChange.emit(true);
    const request = action === 'approve' ? this.service.approve(this.reservation().id) : this.service.cancel(this.reservation().id);
    request.pipe(finalize(() => { this.isSaving.set(false); this.busyChange.emit(false); })).subscribe({
      next: response => {
        if (!response.isSuccess) {
          this.toast.showErrorMessage(response.errors?.[0]?.message ?? 'İşlem tamamlanamadı.');
          return;
        }
        this.action.set(null);
        this.toast.showSuccessMessage(action === 'approve' ? 'Rezervasyon onaylandı.' : 'Rezervasyon iptal edildi.');
        this.changed.emit();
      },
      error: (error: HttpErrorResponse) => {
        this.toast.showErrorMessage((error.error as ApiResponse<unknown>)?.errors?.[0]?.message ?? 'İşlem tamamlanamadı.');
        this.action.set(null);
        // Refresh stale state after another admin's change or an uncertain network result.
        this.changed.emit();
      }
    });
  }
}
