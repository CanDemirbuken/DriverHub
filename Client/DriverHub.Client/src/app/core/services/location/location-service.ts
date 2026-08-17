import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api/api-response';
import { GetLocationsResponse } from './models/get-locations-response';
import { environment } from '../../../../environments/environment';
import { ApiEndpoints } from '../../constants/api-endpoints';

@Injectable({
  providedIn: 'root',
})
export class LocationService {
  constructor(private readonly http: HttpClient){}

  getLocations(): Observable<ApiResponse<GetLocationsResponse[]>>{
    const url = `${environment.apiUrl}${ApiEndpoints.Locations.GetLocations}`
    return this.http.get<ApiResponse<GetLocationsResponse[]>>(url);
  }
}
