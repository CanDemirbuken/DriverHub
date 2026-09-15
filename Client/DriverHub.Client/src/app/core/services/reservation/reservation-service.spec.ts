import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { ReservationService } from './reservation-service';
import { ReservationStatus } from './models/reservation-response';
import { ApiEndpoints } from '../../constants/api-endpoints';
import { environment } from '../../../../environments/environment';

describe('ReservationService', () => {
  let service: ReservationService;
  let http: HttpTestingController;
  beforeEach(() => {
    TestBed.configureTestingModule({ providers: [provideHttpClient(), provideHttpClientTesting()] });
    service = TestBed.inject(ReservationService);
    http = TestBed.inject(HttpTestingController);
  });
  afterEach(() => http.verify());

  it('sends pagination and filters through the central endpoint', () => {
    service.getReservations({ pageNumber: 2, pageSize: 10, status: ReservationStatus.Pending, search: 'Ada', oldestFirst: false }).subscribe();
    const request = http.expectOne(req => req.url === environment.apiUrl + ApiEndpoints.Reservations.List);
    expect(request.request.params.get('PageNumber')).toBe('2');
    expect(request.request.params.get('Status')).toBe('1');
    expect(request.request.params.get('Search')).toBe('Ada');
    request.flush({ isSuccess: true, data: { items: [] }, errors: [] });
  });

  it('approves without allowing a caller-supplied actor or target status', () => {
    service.approve('reservation-id').subscribe();
    const request = http.expectOne(environment.apiUrl + ApiEndpoints.Reservations.Approve('reservation-id'));
    expect(request.request.method).toBe('POST');
    expect(request.request.body).toEqual({});
    request.flush({ isSuccess: true, data: null, errors: [] });
  });

  it('cancels through the dedicated endpoint', () => {
    service.cancel('reservation-id').subscribe();
    const request = http.expectOne(environment.apiUrl + ApiEndpoints.Reservations.Cancel('reservation-id'));
    expect(request.request.method).toBe('POST');
    request.flush({ isSuccess: true, data: null, errors: [] });
  });
});
