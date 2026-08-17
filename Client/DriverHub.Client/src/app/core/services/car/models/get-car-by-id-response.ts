export interface GetCarByIdResponse {
  id: string;

  brandId: string;
  brandName: string;

  categoryId: string;
  categoryName: string;

  currentLocationId: string;
  currentLocationName: string;

  model: string;
  modelYear: number;

  plate: string;
  vin: string;

  coverImageUrl: string;
  bigImageUrl: string;

  km: number;
  transmission: string;

  seat: number;
  luggage: number;

  fuel: string;
  color: string;

  status: number;

  features: GetCarByIdFeatureResponse[];
  pricings: GetCarByIdPricingResponse[];
}

export interface GetCarByIdFeatureResponse {
  featureId: string;
  featureName: string;
}

export interface GetCarByIdPricingResponse {
  type: number;
  amount: number;
}