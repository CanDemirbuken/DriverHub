import { Component, input } from '@angular/core';
import { ReservationStatus, reservationStatusLabels } from '../../../core/services/reservation/models/reservation-response';

@Component({
  selector: 'app-reservation-status',
  template: `<span class="badge" [class.approved]="status() === statuses.Approved" [class.cancelled]="status() === statuses.Cancelled" [class.completed]="status() === statuses.Completed">{{ labels[status()] ?? 'Bilinmiyor' }}</span>`,
  styles: `.badge { display: inline-block; border-radius: 999px; padding: 7px 11px; font-size: 12px; font-weight: 700; background: #fef3c7; color: #92400e; } .approved { background: #dcfce7; color: #166534; } .cancelled { background: #fee2e2; color: #991b1b; } .completed { background: #e2e8f0; color: #334155; }`
})
export class ReservationStatusBadge {
  readonly status = input.required<ReservationStatus>();
  readonly statuses = ReservationStatus;
  readonly labels: Partial<Record<ReservationStatus, string>> = reservationStatusLabels;
}
