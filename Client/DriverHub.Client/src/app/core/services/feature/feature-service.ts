import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiResponse } from '../../models/api/api-response';
import { environment } from '../../../../environments/environment';
import { ApiEndpoints } from '../../constants/api-endpoints';

import { GetFeaturesResponse } from './models/get-features-response';
import { GetFeatureByIdRequest } from './models/get-feature-by-id-request';
import { GetFeatureByIdResponse } from './models/get-feature-by-id-response';
import { CreateFeatureRequest } from './models/create-feature-request';
import { CreateFeatureResponse } from './models/create-feature-response';
import { UpdateFeatureRequest } from './models/update-feature-request';

@Injectable({
  providedIn: 'root',
})
export class FeatureService {
  constructor(private readonly http: HttpClient){}

  getFeatures(): Observable<ApiResponse<GetFeaturesResponse[]>>{
    const url = `${environment.apiUrl}${ApiEndpoints.Features.GetFeatures}`;
    return this.http.get<ApiResponse<GetFeaturesResponse[]>>(url);
  }

  getFeatureById(request: GetFeatureByIdRequest): Observable<ApiResponse<GetFeatureByIdResponse>>{
    const url = `${environment.apiUrl}${ApiEndpoints.Features.GetFeatureById(request.id)}`;
    return this.http.get<ApiResponse<GetFeatureByIdResponse>>(url);
  }

  createFeature(request: CreateFeatureRequest): Observable<ApiResponse<CreateFeatureResponse>>{
    const url = `${environment.apiUrl}${ApiEndpoints.Features.CreateFeature}`;
    return this.http.post<ApiResponse<CreateFeatureResponse>>(url, request);
  }

  updateFeature(id: string, request: UpdateFeatureRequest): Observable<void>{
    const url = `${environment.apiUrl}${ApiEndpoints.Features.EditFeature(id)}`;
    return this.http.put<void>(url, request);
  }

  removeFeature(id: string): Observable<void>{
    const url = `${environment.apiUrl}${ApiEndpoints.Features.RemoveFeature(id)}`;
    return this.http.delete<void>(url);
  }
}