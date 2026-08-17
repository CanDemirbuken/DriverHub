import {
  HttpClient,
  HttpContext
} from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { LoginRequest } from './models/login-request';
import { LoginResponse } from './models/login-response';

import { ApiResponse } from '../../models/api/api-response';
import { ApiEndpoints } from '../../constants/api-endpoints';

import { environment } from '../../../../environments/environment';
import { SKIP_AUTH } from '../../auth/auth-context';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private readonly http: HttpClient) {}

  login(request: LoginRequest): Observable<ApiResponse<LoginResponse>> {
    const url =`${environment.apiUrl}${ApiEndpoints.Authentication.Login}`;

    return this.http.post<ApiResponse<LoginResponse>>(url,request,
      {
        withCredentials: true,

        context: new HttpContext()
          .set(SKIP_AUTH, true)
      }
    );
  }
}