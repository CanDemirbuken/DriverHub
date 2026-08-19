import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GetPagedCarRequest } from './models/get-paged-car-request';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api/api-response';
import { PagedResponse } from '../../models/api/paged-response';
import { GetPagedCarResponse } from './models/get-paged-car-response';
import { environment } from '../../../../environments/environment';
import { ApiEndpoints } from '../../constants/api-endpoints';
import { GetCarByIdResponse } from './models/get-car-by-id-response';
import { GetCarByIdRequest } from './models/get-car-by-id-request';
import { UpdateCarRequest } from './models/update-car-request';
import { UpdateCarStatusRequest } from './models/update-car-status-request';
import { UpdateCarLocationRequest } from './models/update-car-location-request';
import { UpdateCarPricingsRequest } from './models/update-car-pricings-request';
import { UpdateCarFeaturesRequest } from './models/update-car-features-request';

@Injectable({
  providedIn: 'root',
})
export class CarService {
  constructor(private readonly http: HttpClient){}

  getCars(request: GetPagedCarRequest): Observable<ApiResponse<PagedResponse<GetPagedCarResponse>>>{
    const url = `${environment.apiUrl}${ApiEndpoints.Cars.GetPaged(request.pageNumber, request.pageSize)}`;
    return this.http.get<ApiResponse<PagedResponse<GetPagedCarResponse>>>(url);
  }

  getCarById(request: GetCarByIdRequest): Observable<ApiResponse<GetCarByIdResponse>>{
    const url = `${environment.apiUrl}${ApiEndpoints.Cars.GetById(request.carId)}`;
    return this.http.get<ApiResponse<GetCarByIdResponse>>(url);
  }

  updateCar(id: string, request: UpdateCarRequest): Observable<void>{
    const url = `${environment.apiUrl}${ApiEndpoints.Cars.Update(id)}`;
    return this.http.put<void>(url, request);
  }

  updateCarStatus(id: string, request: UpdateCarStatusRequest): Observable<void>{
    const url = `${environment.apiUrl}${ApiEndpoints.Cars.UpdateStatus(id)}`;
    return this.http.patch<void>(url, request);
  }

  updateCarLocation(id: string, request: UpdateCarLocationRequest): Observable<void>{
    const url = `${environment.apiUrl}${ApiEndpoints.Cars.UpdateLocation(id)}`;
    return this.http.patch<void>(url,request);
  }

  updateCarPricings(id: string, request: UpdateCarPricingsRequest): Observable<void>{
    const url = `${environment.apiUrl}${ApiEndpoints.Cars.UpdatePricings(id)}`;
    return this.http.put<void>(url,request);
  }

  updateCarFeatures(id: string, request: UpdateCarFeaturesRequest): Observable<void>{
    const url = `${environment.apiUrl}${ApiEndpoints.Cars.UpdateFeatures(id)}`;
    return this.http.put<void>(url,request);
  }
}
