import { Component, OnInit, signal } from '@angular/core';
import { DatePipe, DecimalPipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { ReservationService } from '../../../core/services/reservation/reservation-service';
import { ReservationResponse } from '../../../core/services/reservation/models/reservation-response';
import { ApiResponse } from '../../../core/models/api/api-response';
import { RouteLinks } from '../../../core/constants/route-paths';
import { ReservationStatusBadge } from '../reservation-status/reservation-status';
import { ReservationActions } from '../reservation-actions/reservation-actions';

@Component({
  selector: 'app-reservation-detail',
  imports: [RouterLink, DatePipe, DecimalPipe, ReservationStatusBadge, ReservationActions],
  templateUrl: './reservation-detail.html',
  styleUrl: '../reservation-management.scss'
})
export class ReservationDetail implements OnInit {
  readonly routeLinks = RouteLinks;
  readonly reservation = signal<ReservationResponse | null>(null);
  readonly isLoading = signal(false);
  readonly errorMessage = signal('');

  constructor(private readonly service: ReservationService, private readonly route: ActivatedRoute) {}
  ngOnInit(): void { this.load(); }

  load(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id || this.isLoading()) return;
    this.isLoading.set(true);
    this.errorMessage.set('');
    this.service.getById(id).pipe(finalize(() => this.isLoading.set(false))).subscribe({
      next: response => {
        if (!response.isSuccess || !response.data) { this.errorMessage.set('Rezervasyon alınamadı.'); return; }
        this.reservation.set(response.data);
      },
      error: (error: HttpErrorResponse) => this.errorMessage.set((error.error as ApiResponse<unknown>)?.errors?.[0]?.message ?? 'Rezervasyon alınamadı.')
    });
  }
}
