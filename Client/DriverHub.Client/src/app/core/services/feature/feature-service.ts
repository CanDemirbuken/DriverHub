import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api/api-response';
import { GetFeaturesResponse } from './models/get-features-response';
import { environment } from '../../../../environments/environment';
import { ApiEndpoints } from '../../constants/api-endpoints';

@Injectable({
  providedIn: 'root',
})
export class FeatureService {
  constructor(private readonly http: HttpClient){}

  getFeatures(): Observable<ApiResponse<GetFeaturesResponse[]>>{
    const url = `${environment.apiUrl}${ApiEndpoints.Features.GetFeatures}`;
    return this.http.get<ApiResponse<GetFeaturesResponse[]>>(url);
  }
}