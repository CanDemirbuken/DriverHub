import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api/api-response';
import { GetBrandsResponse } from './models/get-brands-response';
import { environment } from '../../../../environments/environment';
import { ApiEndpoints } from '../../constants/api-endpoints';
import { GetBrandByIdResponse } from './models/get-brand-by-id-response';
import { CreateBrandRequest } from './models/create-brand-request';
import { CreateBrandResponse } from './models/create-brand-response';

@Injectable({
  providedIn: 'root',
})
export class BrandService {
  constructor(private readonly http: HttpClient){}

  getBrands(): Observable<ApiResponse<GetBrandsResponse[]>>{
    const url = `${environment.apiUrl}${ApiEndpoints.Brands.GetBrands}`;
    return this.http.get<ApiResponse<GetBrandsResponse[]>>(url);
  }

  getBrandById(id: string): Observable<ApiResponse<GetBrandByIdResponse>>{
    const url = `${environment.apiUrl}${ApiEndpoints.Brands.GetById(id)}`;
    return this.http.get<ApiResponse<GetBrandByIdResponse>>(url);
  }

  createBrand(request: CreateBrandRequest): Observable<ApiResponse<CreateBrandResponse>>{
    const url = `${environment.apiUrl}${ApiEndpoints.Brands.CreateBrand}`;
    return this.http.post<ApiResponse<CreateBrandResponse>>(url, request);
  }

  removeBrand(id: string): Observable<void>{
    const url = `${environment.apiUrl}${ApiEndpoints.Brands.RemoveBrand(id)}`
    return this.http.delete<void>(url);
  }
}
