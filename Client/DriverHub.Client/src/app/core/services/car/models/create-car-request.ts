export interface CreateCarRequest {
  brandId: string;
  categoryId: string;
  currentLocationId: string;
  model: string;
  modelYear: number;
  plate: string;
  vin: string;
  km: number;
  transmission: string;
  seat: number;
  luggage: number;
  fuel: string;
  color: string;
  coverImageUrl: string;
  bigImageUrl: string;
}
