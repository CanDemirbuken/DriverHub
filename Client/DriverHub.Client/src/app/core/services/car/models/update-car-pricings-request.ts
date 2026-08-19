export interface UpdateCarPricingsRequest {
  pricings: UpdateCarPricingItemRequest[];
}

export interface UpdateCarPricingItemRequest {
  type: PricingType;
  amount: number;
}

export enum PricingType {
  Daily = 1,
  Weekly = 2,
  Monthly = 3
}