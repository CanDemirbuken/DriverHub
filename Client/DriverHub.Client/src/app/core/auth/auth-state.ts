import { Injectable } from '@angular/core';

import { TokenStore } from './token-store';
import { AuthUser } from './models/auth-user';

@Injectable({
  providedIn: 'root'
})
export class AuthState {
  constructor(private readonly tokenStore: TokenStore) {}

  get isAuthenticated(): boolean {
    return this.tokenStore.hasAccessToken();
  }

  get currentUser(): AuthUser | null {
    return this.tokenStore.getUser();
  }

  logout(): void {
    this.tokenStore.clear();
  }
}