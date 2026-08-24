import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api/api-response';
import { GetCategoriesResponse } from './models/get-categories-response';
import { environment } from '../../../../environments/environment';
import { ApiEndpoints } from '../../constants/api-endpoints';
import { CreateCategoryRequest } from './models/create-category-request';
import { CreateCategoryResponse } from './models/create-category-response';
import { GetCategoryByIdRequest } from './models/get-category-by-id-request';
import { GetCategoryByIdResponse } from './models/get-category-by-id-response';
import { UpdateCategoryRequest } from './models/update-category-request';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private readonly http: HttpClient){}

  getCategories(): Observable<ApiResponse<GetCategoriesResponse[]>>{
    const url = `${environment.apiUrl}${ApiEndpoints.Categories.GetCategories}`
    return this.http.get<ApiResponse<GetCategoriesResponse[]>>(url);
  }

  removeCategory(id: string): Observable<void>{
    const url = `${environment.apiUrl}${ApiEndpoints.Categories.RemoveCategory(id)}`;
    return this.http.delete<void>(url);
  }

  createCategory(request: CreateCategoryRequest): Observable<ApiResponse<CreateCategoryResponse>>{
    const url = `${environment.apiUrl}${ApiEndpoints.Categories.CreateCategory}`;
    return this.http.post<ApiResponse<CreateCategoryResponse>>(url, request);
  }

  getCategoryById(request: GetCategoryByIdRequest): Observable<ApiResponse<GetCategoryByIdResponse>>{
    const url = `${environment.apiUrl}${ApiEndpoints.Categories.GetCategoryById(request.id)}`;
    return this.http.get<ApiResponse<GetCategoryByIdResponse>>(url);
  }

  updateCategory(id: string, request: UpdateCategoryRequest): Observable<void>{
    const url = `${environment.apiUrl}${ApiEndpoints.Categories.EditCategory(id)}`;
    return this.http.put<void>(url, request);
  }
}
