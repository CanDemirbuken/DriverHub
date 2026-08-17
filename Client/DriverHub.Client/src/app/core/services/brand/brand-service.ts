import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api/api-response';
import { GetBrandsResponse } from './models/get-brands-response';
import { environment } from '../../../../environments/environment';
import { ApiEndpoints } from '../../constants/api-endpoints';

@Injectable({
  providedIn: 'root',
})
export class BrandService {
  constructor(private readonly http: HttpClient){}

  getBrands(): Observable<ApiResponse<GetBrandsResponse[]>>{
    const url = `${environment.apiUrl}${ApiEndpoints.Brands.GetBrands}`;
    return this.http.get<ApiResponse<GetBrandsResponse[]>>(url);
  }
}
