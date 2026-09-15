export enum ReservationStatus {
  Pending = 1,
  Approved = 2, // Backend's existing Confirmed value.
  Cancelled = 3,
  Completed = 4
}

export const reservationStatusLabels: Record<ReservationStatus, string> = {
  [ReservationStatus.Pending]: 'Onay Bekliyor',
  [ReservationStatus.Approved]: 'Onaylandı',
  [ReservationStatus.Cancelled]: 'İptal Edildi',
  [ReservationStatus.Completed]: 'Tamamlandı'
};

export interface ReservationResponse {
  id: string;
  carId: string;
  brandName: string;
  model: string;
  plate: string;
  customerFirstName: string | null;
  customerLastName: string | null;
  customerEmail: string | null;
  customerPhone: string | null;
  pickupLocationId: string;
  pickupLocationName: string;
  returnLocationId: string;
  returnLocationName: string;
  startDate: string;
  endDate: string;
  createdDate: string;
  status: ReservationStatus;
  processedAt: string | null;
  processedByName: string | null;
  rentalDays: number;
  basePrice: number;
  extraPrice: number;
  insurancePrice: number;
  totalPrice: number;
}
