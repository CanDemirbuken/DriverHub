import { Injectable } from '@angular/core';
import { JwtDecoder } from './jwt-decoder';
import { AuthUser } from './models/auth-user';

@Injectable({
  providedIn: 'root'
})
export class TokenStore {
  private accessToken: string | null = null;

  constructor(private readonly jwtDecoder: JwtDecoder){}

  setAccessToken(token: string): void {
    this.accessToken = token;
  }

  getAccessToken(): string | null {
    return this.accessToken;
  }

  clear(): void {
    this.accessToken = null;
  }

  hasAccessToken(): boolean {
    return !!this.accessToken;
  }

  getUser(): AuthUser | null{
    if (!this.accessToken){
      return null;
    }

    const payload = this.jwtDecoder.decode(this.accessToken);

    if(!payload){
      return null;
    }

    const userId =
      payload.sub ??
      payload[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
      ];

    const email =
      payload.email ??
      payload[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'
      ];

    const rawRoles =
      payload.role ??
      payload[
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
      ];

    const roles =
      Array.isArray(rawRoles)
        ? rawRoles
        : rawRoles
          ? [rawRoles]
          : [];

    if (!userId || !email){
      return null;
    }

    return {
      userId,
      email,
      roles
    };
  }
}