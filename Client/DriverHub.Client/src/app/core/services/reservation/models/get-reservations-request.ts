import { ReservationStatus } from './reservation-response';

export interface GetReservationsRequest {
  pageNumber: number;
  pageSize: number;
  status: ReservationStatus | null;
  search: string;
  carId?: string;
  from?: string;
  to?: string;
  oldestFirst: boolean;
}
