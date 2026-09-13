export interface GetAvailableCarsResponse {
  id: string;
  coverImageUrl: string;
  brandName: string;
  model: string;
  modelYear: number;
  plate: string;
  categoryName: string;
  currentLocationName: string;
  km: number;
  transmission: string;
  fuel: string;
  status: number;
}
