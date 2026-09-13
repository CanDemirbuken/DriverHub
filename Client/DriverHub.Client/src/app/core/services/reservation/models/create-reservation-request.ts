export interface CreateReservationRequest {
  carId: string;
  pickupLocationId: string;
  startDate: string;
  endDate: string;
  extraIds: string[];
  insurancePackageId: string | null;
}
