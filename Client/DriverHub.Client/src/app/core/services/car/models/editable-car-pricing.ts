import { PricingType } from "./update-car-pricings-request";

export interface EditableCarPricing {
  type: PricingType;
  amount: number | null;
}