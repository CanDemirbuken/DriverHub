export interface GetReservationQuoteResponse {
  car: { id: string; brandName: string; model: string; modelYear: number; plate: string; coverImageUrl: string; status: number };
  pickupLocationId: string;
  pickupLocationName: string;
  returnLocationId: string;
  returnLocationName: string;
  startDate: string;
  endDate: string;
  rentalDays: number;
  basePrice: number;
  selectedExtras: ReservationExtraInfo[];
  availableExtras: ReservationExtraInfo[];
  selectedInsurance: InsuranceInfo | null;
  availableInsurancePackages: InsuranceInfo[];
  extrasTotal: number;
  insurancePrice: number;
  totalPrice: number;
}

export interface ReservationExtraInfo { id: string; name: string; dailyPrice: number; }
export interface InsuranceInfo { id: string; name: string; dailyPrice: number; }
