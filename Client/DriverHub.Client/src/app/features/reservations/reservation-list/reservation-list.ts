import { Component, OnInit, signal } from '@angular/core';
import { DatePipe, DecimalPipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { ReservationService } from '../../../core/services/reservation/reservation-service';
import { ReservationResponse, ReservationStatus } from '../../../core/services/reservation/models/reservation-response';
import { PagedResponse } from '../../../core/models/api/paged-response';
import { ApiResponse } from '../../../core/models/api/api-response';
import { RouteLinks } from '../../../core/constants/route-paths';
import { ReservationStatusBadge } from '../reservation-status/reservation-status';
import { ReservationActions } from '../reservation-actions/reservation-actions';

@Component({
  selector: 'app-reservation-list',
  imports: [RouterLink, FormsModule, DatePipe, DecimalPipe, ReservationStatusBadge, ReservationActions],
  templateUrl: './reservation-list.html',
  styleUrl: '../reservation-management.scss'
})
export class ReservationList implements OnInit {
  readonly routeLinks = RouteLinks;
  readonly statuses = ReservationStatus;
  readonly data = signal<PagedResponse<ReservationResponse> | null>(null);
  readonly isLoading = signal(false);
  readonly isUpdating = signal(false);
  readonly errorMessage = signal('');
  pageNumber = 1;
  search = '';
  status: ReservationStatus | null = null;
  from = '';
  to = '';
  oldestFirst = false;

  constructor(private readonly service: ReservationService) {}
  ngOnInit(): void { this.load(); }

  applyFilters(): void {
    this.pageNumber = 1;
    this.load();
  }

  changePage(page: number): void {
    if (this.isLoading() || this.isUpdating() || page < 1 || page > (this.data()?.totalPages ?? 0)) return;
    this.pageNumber = page;
    this.load();
  }

  load(): void {
    if (this.isLoading()) return;
    this.errorMessage.set('');
    if (this.from && this.to && this.from >= this.to) {
      this.errorMessage.set('Filtre bitiş tarihi başlangıçtan sonra olmalıdır.');
      return;
    }
    this.isLoading.set(true);
    this.service.getReservations({
      pageNumber: this.pageNumber, pageSize: 10, search: this.search, status: this.status,
      from: this.from ? new Date(this.from).toISOString() : undefined,
      to: this.to ? new Date(this.to).toISOString() : undefined, oldestFirst: this.oldestFirst
    }).pipe(finalize(() => this.isLoading.set(false))).subscribe({
      next: response => {
        if (!response.isSuccess || !response.data) {
          this.errorMessage.set(response.errors?.[0]?.message ?? 'Rezervasyonlar alınamadı.');
          return;
        }
        this.data.set(response.data);
      },
      error: (error: HttpErrorResponse) => this.errorMessage.set((error.error as ApiResponse<unknown>)?.errors?.[0]?.message ?? 'Rezervasyonlar alınamadı.')
    });
  }
}
