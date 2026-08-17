import { Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';

import { RefreshCoordinator } from './refresh-coordinator';

import { TokenStore } from './token-store';

@Injectable({
  providedIn: 'root'
})
export class SessionRestorer {

  constructor(
    private readonly refreshCoordinator: RefreshCoordinator,

    private readonly tokenStore: TokenStore
  ) {}

  restore(): Observable<void> {

    return this.refreshCoordinator
      .refreshAccessToken()
      .pipe(

        map(() => void 0),

        catchError(() => {

          this.tokenStore.clear();

          return of(void 0);
        })
      );
  }
}