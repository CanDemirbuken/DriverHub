import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api/api-response';
import { GetCategoriesResponse } from './models/get-categories-response';
import { environment } from '../../../../environments/environment';
import { ApiEndpoints } from '../../constants/api-endpoints';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private readonly http: HttpClient){}

  getCategories(): Observable<ApiResponse<GetCategoriesResponse[]>>{
    const url = `${environment.apiUrl}${ApiEndpoints.Categories.GetCategories}`
    return this.http.get<ApiResponse<GetCategoriesResponse[]>>(url);
  }
}
