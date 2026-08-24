import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api/api-response';
import { GetLocationsResponse } from './models/get-locations-response';
import { environment } from '../../../../environments/environment';
import { ApiEndpoints } from '../../constants/api-endpoints';
import { GetLocationByIdResponse } from './models/get-location-by-id-response';
import { GetLocationByIdRequest } from './models/get-location-by-id-request';
import { CreateLocationRequest } from './models/create-location-request';
import { CreateLocationResponse } from './models/create-location-response';
import { UpdateLocationRequest } from './models/update-location-request';

@Injectable({
  providedIn: 'root',
})
export class LocationService {
  constructor(private readonly http: HttpClient){}

  getLocations(): Observable<ApiResponse<GetLocationsResponse[]>>{
    const url = `${environment.apiUrl}${ApiEndpoints.Locations.GetLocations}`
    return this.http.get<ApiResponse<GetLocationsResponse[]>>(url);
  }

  getLocationById(request: GetLocationByIdRequest): Observable<ApiResponse<GetLocationByIdResponse>>{
    const url = `${environment.apiUrl}${ApiEndpoints.Locations.GetLocationById(request.id)}`;
    return this.http.get<ApiResponse<GetLocationByIdResponse>>(url);
  }

  createLocation(request: CreateLocationRequest): Observable<ApiResponse<CreateLocationResponse>>{
    const url = `${environment.apiUrl}${ApiEndpoints.Locations.CreateLocation}`;
    return this.http.post<ApiResponse<CreateLocationResponse>>(url, request);
  }

  removeLocation(id: string): Observable<void>{
    const url = `${environment.apiUrl}${ApiEndpoints.Locations.RemoveLocation(id)}`;
    return this.http.delete<void>(url);
  }

  updateLocation(id: string, request: UpdateLocationRequest): Observable<void>{
    const url = `${environment.apiUrl}${ApiEndpoints.Locations.EditLocation(id)}`;
    return this.http.put<void>(url, request);
  }
}
