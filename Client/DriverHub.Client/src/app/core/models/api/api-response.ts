import { ApiError } from "./api-error";

export interface ApiResponse<T> {
  isSuccess: boolean;
  data: T | null;
  errors: ApiError[];
}