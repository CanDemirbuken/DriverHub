import { TestBed } from '@angular/core/testing';
import { provideRouter, ActivatedRoute, convertToParamMap } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { Subject, of } from 'rxjs';
import { ReservationActions } from './reservation-actions/reservation-actions';
import { ReservationList } from './reservation-list/reservation-list';
import { ReservationDetail } from './reservation-detail/reservation-detail';
import { ReservationService } from '../../core/services/reservation/reservation-service';
import { ReservationResponse, ReservationStatus } from '../../core/services/reservation/models/reservation-response';
import { ApiResponse } from '../../core/models/api/api-response';
import { ToastService } from '../../shared/services/toast-service';

const reservation: ReservationResponse = {
  id: 'reservation-id', carId: 'car-id', brandName: 'Brand', model: 'Model', plate: '34 TEST 1',
  customerFirstName: 'Ada', customerLastName: 'Lovelace', customerEmail: 'ada@example.test', customerPhone: null,
  pickupLocationId: 'location-id', pickupLocationName: 'Airport', returnLocationId: 'location-id', returnLocationName: 'Airport',
  startDate: '2030-01-01T00:00:00Z', endDate: '2030-01-02T00:00:00Z', createdDate: '2029-12-01T00:00:00Z',
  status: ReservationStatus.Pending, processedAt: null, processedByName: null,
  rentalDays: 9, basePrice: 100, extraPrice: 10, insurancePrice: 20, totalPrice: 130
};

describe('Reservation management', () => {
  const toast = { showSuccessMessage: vi.fn(), showErrorMessage: vi.fn() };
  let result: Subject<ApiResponse<unknown>>;
  let service: { approve: ReturnType<typeof vi.fn>; cancel: ReturnType<typeof vi.fn>; getReservations: ReturnType<typeof vi.fn>; getById: ReturnType<typeof vi.fn> };

  beforeEach(() => {
    result = new Subject<ApiResponse<unknown>>();
    service = {
      approve: vi.fn(() => result), cancel: vi.fn(() => result),
      getReservations: vi.fn(() => of({ isSuccess: true, errors: [], data: { items: [reservation], pageNumber: 1, pageSize: 10, totalCount: 1, totalPages: 1, hasPreviousPage: false, hasNextPage: false } })),
      getById: vi.fn(() => of({ isSuccess: true, errors: [], data: reservation }))
    };
    vi.clearAllMocks();
    TestBed.configureTestingModule({ providers: [provideRouter([]),
      { provide: ReservationService, useValue: service }, { provide: ToastService, useValue: toast },
      { provide: ActivatedRoute, useValue: { snapshot: { paramMap: convertToParamMap({ id: reservation.id }) } } }
    ] });
  });

  it('requires confirmation and prevents duplicate approval requests', () => {
    const fixture = TestBed.createComponent(ReservationActions);
    fixture.componentRef.setInput('reservation', reservation);
    fixture.detectChanges();
    fixture.componentInstance.confirm();
    expect(service.approve).not.toHaveBeenCalled();
    fixture.componentInstance.action.set('approve');
    fixture.componentInstance.confirm();
    fixture.componentInstance.confirm();
    expect(service.approve).toHaveBeenCalledTimes(1);
    expect(fixture.componentInstance.isSaving()).toBe(true);
    result.next({ isSuccess: true, data: null, errors: [] });
    result.complete();
    expect(fixture.componentInstance.isSaving()).toBe(false);
    expect(toast.showSuccessMessage).toHaveBeenCalled();
  });

  it('resets saving state and refreshes after a conflict', () => {
    const fixture = TestBed.createComponent(ReservationActions);
    fixture.componentRef.setInput('reservation', reservation);
    const changed = vi.fn();
    fixture.componentInstance.changed.subscribe(changed);
    fixture.componentInstance.action.set('cancel');
    fixture.componentInstance.confirm();
    result.error(new HttpErrorResponse({ status: 409, error: { errors: [{ message: 'Already processed' }] } }));
    expect(fixture.componentInstance.isSaving()).toBe(false);
    expect(changed).toHaveBeenCalled();
    expect(toast.showErrorMessage).toHaveBeenCalledWith('Already processed');
  });

  it.each([ReservationStatus.Approved, ReservationStatus.Cancelled, ReservationStatus.Completed])('hides operations for status %s', status => {
    const fixture = TestBed.createComponent(ReservationActions);
    fixture.componentRef.setInput('reservation', { ...reservation, status });
    fixture.detectChanges();
    expect(fixture.nativeElement.querySelector('button')).toBeNull();
    fixture.componentInstance.action.set('approve');
    fixture.componentInstance.confirm();
    expect(service.approve).not.toHaveBeenCalled();
  });

  it('lists customer data and displays the backend rental day value', () => {
    const fixture = TestBed.createComponent(ReservationList);
    fixture.detectChanges();
    const text = fixture.nativeElement.textContent as string;
    expect(text).toContain('Ada');
    expect(text).toContain('9 gün');
    expect(service.getReservations).toHaveBeenCalledWith(expect.objectContaining({ pageNumber: 1, pageSize: 10 }));
  });

  it('loads a detail with pricing and customer information', () => {
    const fixture = TestBed.createComponent(ReservationDetail);
    fixture.detectChanges();
    expect(service.getById).toHaveBeenCalledWith(reservation.id);
    expect(fixture.nativeElement.textContent).toContain('ada@example.test');
    expect(fixture.nativeElement.textContent).toContain('9 gün');
    expect(fixture.nativeElement.textContent).toContain('Sigorta');
  });
});
