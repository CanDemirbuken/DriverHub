export interface UpdateCarRequest {
  brandId: string;
  categoryId: string;
  currentLocationId: string;

  model: string;
  modelYear: number;

  plate: string;
  vin: string;

  coverImageUrl: string;
  km: number;

  transmission: string;
  seat: number;
  luggage: number;

  fuel: string;
  color: string;

  bigImageUrl: string;
}