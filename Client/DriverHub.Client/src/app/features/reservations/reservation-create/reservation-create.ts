import { Component, OnInit, signal } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { DecimalPipe } from '@angular/common';
import { minimumReservationStart } from '../../../core/services/reservation/reservation-time-policy';

import { LocationService } from '../../../core/services/location/location-service';
import { GetLocationsResponse } from '../../../core/services/location/models/get-locations-response';
import { ReservationService } from '../../../core/services/reservation/reservation-service';
import { GetAvailableCarsResponse } from '../../../core/services/reservation/models/get-available-cars-response';
import { ApiResponse } from '../../../core/models/api/api-response';
import { RouteLinks } from '../../../core/constants/route-paths';
import { ToastService } from '../../../shared/services/toast-service';
import { ImageUrlHelper } from '../../../shared/helpers/image-url-helper';
import { GetReservationQuoteResponse } from '../../../core/services/reservation/models/get-reservation-quote-response';

@Component({
  selector: 'app-reservation-create',
  imports: [FormsModule, RouterLink, DecimalPipe],
  templateUrl: './reservation-create.html',
  styleUrl: './reservation-create.scss',
})
export class ReservationCreate implements OnInit {
  readonly routeLinks = RouteLinks;
  readonly imageUrlHelper = ImageUrlHelper;

  locations = signal<GetLocationsResponse[]>([]);
  availableCars = signal<GetAvailableCarsResponse[]>([]);
  selectedCar = signal<GetAvailableCarsResponse | null>(null);
  quote = signal<GetReservationQuoteResponse | null>(null);
  selectedExtraIds = new Set<string>();
  selectedInsuranceId: string | null = null;
  isLoading = signal(false);
  isSearching = signal(false);
  isCreating = signal(false);
  errorMessage = signal('');
  createdReservationId = signal<string | null>(null);

  pickupLocationId = '';
  startDate = '';
  endDate = '';
  startDateMin = '';

  constructor(
    private readonly locationService: LocationService,
    private readonly reservationService: ReservationService,
    private readonly toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.refreshStartDateMin();
    this.isLoading.set(true);
    this.locationService.getLocations().subscribe({
      next: response => {
        if (!response.isSuccess || !response.data) this.showError('Lokasyon bilgisi alınamadı.');
        else this.locations.set(response.data);
        this.isLoading.set(false);
      },
      error: (error: HttpErrorResponse) => {
        this.setRequestError(error, 'Lokasyon listesi alınırken bir hata oluştu.');
        this.isLoading.set(false);
      }
    });
  }

  searchAvailability(): void {
    if (this.isCreating() || this.isSearching() || this.createdReservationId()) return;
    this.errorMessage.set('');
    this.selectedCar.set(null);
    this.quote.set(null);
    this.availableCars.set([]);
    const validation = this.validateDates();
    if (validation) { this.showError(validation); return; }

    this.isSearching.set(true);
    this.reservationService.getAvailableCars({
      pickupLocationId: this.pickupLocationId,
      startDate: new Date(this.startDate).toISOString(),
      endDate: new Date(this.endDate).toISOString()
    }).subscribe({
      next: response => {
        if (!response.isSuccess || !response.data) this.showError('Müsait araçlar alınamadı.');
        else this.availableCars.set(response.data);
        this.isSearching.set(false);
      },
      error: (error: HttpErrorResponse) => { this.isSearching.set(false); this.setRequestError(error, 'Müsait araçlar alınırken bir hata oluştu.'); }
    });
  }

  selectCar(car: GetAvailableCarsResponse): void {
    if (this.isCreating() || this.isSearching() || this.createdReservationId()) return;
    this.selectedCar.set(car);
    this.selectedExtraIds = new Set();
    this.selectedInsuranceId = null;
    this.loadQuote();
  }

  toggleExtra(extraId: string): void {
    if (this.isCreating() || this.isSearching() || this.createdReservationId()) return;
    if (this.selectedExtraIds.has(extraId)) this.selectedExtraIds.delete(extraId);
    else this.selectedExtraIds.add(extraId);
    this.loadQuote();
  }

  selectInsurance(insuranceId: string | null): void {
    if (this.isCreating() || this.isSearching() || this.createdReservationId()) return;
    this.selectedInsuranceId = insuranceId;
    this.loadQuote();
  }

  backToCars(): void {
    if (this.isCreating() || this.isSearching()) return;
    this.quote.set(null);
  }

  criteriaChanged(): void {
    this.selectedCar.set(null);
    this.quote.set(null);
    this.availableCars.set([]);
  }

  createReservation(): void {
    if (this.isCreating() || this.isSearching() || this.createdReservationId() || !this.quote()) return;
    const car = this.selectedCar();
    const validation = this.validateDates();
    if (!car) { this.showError('Önce bir araç seçmelisiniz.'); return; }
    if (validation) { this.showError(validation); return; }

    this.isCreating.set(true);
    this.reservationService.createReservation({
      carId: car.id,
      pickupLocationId: this.pickupLocationId,
      startDate: new Date(this.startDate).toISOString(),
      endDate: new Date(this.endDate).toISOString(),
      extraIds: [...this.selectedExtraIds],
      insurancePackageId: this.selectedInsuranceId
    }).subscribe({
      next: response => {
        this.isCreating.set(false);
        if (!response.isSuccess || !response.data) { this.showError('Rezervasyon oluşturulamadı.'); return; }
        this.toastService.showSuccessMessage('Rezervasyon başarıyla oluşturuldu.');
        this.resetPreparation();
        this.createdReservationId.set(response.data.id);
      },
      error: (error: HttpErrorResponse) => { this.isCreating.set(false); this.setRequestError(error, 'Rezervasyon oluşturulurken bir hata oluştu.'); }
    });
  }

  private validateDates(): string {
    this.refreshStartDateMin();
    if (!this.pickupLocationId) return 'Teslim alma lokasyonu seçilmelidir.';
    if (!this.startDate || !this.endDate) return 'Başlangıç ve bitiş tarihi zorunludur.';
    const start = new Date(this.startDate).getTime();
    const end = new Date(this.endDate).getTime();
    if (Number.isNaN(start) || Number.isNaN(end)) return 'Geçerli bir tarih aralığı girilmelidir.';
    if (start < new Date(this.startDateMin).getTime()) return 'Başlangıç tarihi geçmişte olamaz.';
    if (end <= start) return 'Bitiş tarihi başlangıç tarihinden sonra olmalıdır.';
    return '';
  }

  private loadQuote(): void {
    const car = this.selectedCar();
    const validation = this.validateDates();
    if (!car) return;
    if (validation) { this.showError(validation); return; }
    this.isSearching.set(true);
    this.reservationService.getQuote({
      carId: car.id,
      pickupLocationId: this.pickupLocationId,
      startDate: new Date(this.startDate).toISOString(),
      endDate: new Date(this.endDate).toISOString(),
      extraIds: [...this.selectedExtraIds],
      insurancePackageId: this.selectedInsuranceId
    }).subscribe({
      next: response => {
        this.isSearching.set(false);
        if (!response.isSuccess || !response.data) { this.showError('Rezervasyon özeti alınamadı.'); return; }
        this.quote.set(response.data);
      },
      error: (error: HttpErrorResponse) => { this.isSearching.set(false); this.setRequestError(error, 'Rezervasyon özeti alınırken bir hata oluştu.'); }
    });
  }

  refreshStartDateMin(): void {
    this.startDateMin = this.toDateTimeLocal(minimumReservationStart());
  }

  startNewReservation(): void {
    this.resetPreparation();
    this.createdReservationId.set(null);
    this.refreshStartDateMin();
  }

  private resetPreparation(): void {
    this.selectedCar.set(null);
    this.quote.set(null);
    this.availableCars.set([]);
    this.selectedExtraIds = new Set();
    this.selectedInsuranceId = null;
    this.pickupLocationId = '';
    this.startDate = '';
    this.endDate = '';
    this.isCreating.set(false);
    this.isSearching.set(false);
    this.errorMessage.set('');
  }

  private toDateTimeLocal(date: Date): string {
    const pad = (value: number) => value.toString().padStart(2, '0');
    return `${date.getFullYear()}-${pad(date.getMonth() + 1)}-${pad(date.getDate())}T${pad(date.getHours())}:${pad(date.getMinutes())}`;
  }

  private setRequestError(error: HttpErrorResponse, fallback: string): void { this.showError((error.error as ApiResponse<unknown>)?.errors?.[0]?.message ?? fallback); }
  private showError(message: string): void { this.errorMessage.set(message); this.toastService.showErrorMessage(message); }
}
