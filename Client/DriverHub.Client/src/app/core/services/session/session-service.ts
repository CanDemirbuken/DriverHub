import { Injectable } from '@angular/core';
import { HttpClient, HttpContext } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';
import { ApiEndpoints } from '../../constants/api-endpoints';
import { ApiResponse } from '../../models/api/api-response';
import { SKIP_AUTH } from '../../auth/auth-context';

export interface RefreshSessionResponse {
  accessToken: string;
  accessTokenExpiresAt: string;
}

@Injectable({
  providedIn: 'root'
})

export class SessionService {
  constructor(private readonly http: HttpClient) {}

  refresh(): Observable<ApiResponse<RefreshSessionResponse>> {
    const url = `${environment.apiUrl}${ApiEndpoints.Sessions.RefreshToken}`;

    return this.http.post<ApiResponse<RefreshSessionResponse>>(url, {},
      {
        withCredentials: true,

        context: new HttpContext()
          .set(SKIP_AUTH, true)
      }
    );
  }

  logout(): Observable<void> {
    const url = `${environment.apiUrl}${ApiEndpoints.Sessions.Logout}`;

    return this.http.post<void>(url,{},
      {
        withCredentials: true,

        context: new HttpContext()
          .set(SKIP_AUTH, true)
      }
    );
  }

  logoutAll(): Observable<void> {
    const url = `${environment.apiUrl}${ApiEndpoints.Sessions.LogoutAll}`;

    return this.http.post<void>(url, {},
      {
        withCredentials: true
      }
    );
  }
}