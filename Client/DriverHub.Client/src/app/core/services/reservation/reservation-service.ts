import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { ApiEndpoints } from '../../constants/api-endpoints';
import { ApiResponse } from '../../models/api/api-response';
import { GetAvailableCarsRequest } from './models/get-available-cars-request';
import { GetAvailableCarsResponse } from './models/get-available-cars-response';
import { CreateReservationRequest } from './models/create-reservation-request';
import { CreateReservationResponse } from './models/create-reservation-response';
import { GetReservationQuoteResponse } from './models/get-reservation-quote-response';
import { PagedResponse } from '../../models/api/paged-response';
import { ReservationResponse } from './models/reservation-response';
import { GetReservationsRequest } from './models/get-reservations-request';

@Injectable({ providedIn: 'root' })
export class ReservationService {
  constructor(private readonly http: HttpClient) {}

  getReservations(request: GetReservationsRequest): Observable<ApiResponse<PagedResponse<ReservationResponse>>> {
    let params = new HttpParams().set('PageNumber', request.pageNumber).set('PageSize', request.pageSize)
      .set('Search', request.search).set('OldestFirst', request.oldestFirst);
    if (request.status !== null) params = params.set('Status', request.status);
    if (request.carId) params = params.set('CarId', request.carId);
    if (request.from) params = params.set('From', request.from);
    if (request.to) params = params.set('To', request.to);
    return this.http.get<ApiResponse<PagedResponse<ReservationResponse>>>(`${environment.apiUrl}${ApiEndpoints.Reservations.List}`, { params });
  }

  getById(id: string): Observable<ApiResponse<ReservationResponse>> {
    return this.http.get<ApiResponse<ReservationResponse>>(`${environment.apiUrl}${ApiEndpoints.Reservations.Detail(id)}`);
  }

  approve(id: string): Observable<ApiResponse<unknown>> {
    return this.http.post<ApiResponse<unknown>>(`${environment.apiUrl}${ApiEndpoints.Reservations.Approve(id)}`, {});
  }

  cancel(id: string): Observable<ApiResponse<unknown>> {
    return this.http.post<ApiResponse<unknown>>(`${environment.apiUrl}${ApiEndpoints.Reservations.Cancel(id)}`, {});
  }

  getAvailableCars(request: GetAvailableCarsRequest): Observable<ApiResponse<GetAvailableCarsResponse[]>> {
    const url = `${environment.apiUrl}${ApiEndpoints.Reservations.Availability}`;
    const params = new HttpParams()
      .set('PickupLocationId', request.pickupLocationId)
      .set('StartDate', request.startDate)
      .set('EndDate', request.endDate);
    return this.http.get<ApiResponse<GetAvailableCarsResponse[]>>(url, { params });
  }

  createReservation(request: CreateReservationRequest): Observable<ApiResponse<CreateReservationResponse>> {
    const url = `${environment.apiUrl}${ApiEndpoints.Reservations.Create}`;
    return this.http.post<ApiResponse<CreateReservationResponse>>(url, request);
  }

  getQuote(request: { carId: string; pickupLocationId: string; startDate: string; endDate: string; extraIds: string[]; insurancePackageId: string | null }): Observable<ApiResponse<GetReservationQuoteResponse>> {
    let params = new HttpParams()
      .set('CarId', request.carId)
      .set('PickupLocationId', request.pickupLocationId)
      .set('StartDate', request.startDate)
      .set('EndDate', request.endDate)
      .set('InsurancePackageId', request.insurancePackageId ?? '');
    request.extraIds.forEach(id => { params = params.append('ExtraIds', id); });
    return this.http.get<ApiResponse<GetReservationQuoteResponse>>(`${environment.apiUrl}${ApiEndpoints.Reservations.Quote}`, { params });
  }
}
