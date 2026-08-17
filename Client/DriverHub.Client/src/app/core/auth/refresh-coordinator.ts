import { Injectable } from '@angular/core';
import { finalize, map, Observable, shareReplay, tap} from 'rxjs';
import { TokenStore } from './token-store';

import { SessionService } from '../services/session/session-service';

@Injectable({
  providedIn: 'root'
})
export class RefreshCoordinator {
  private refreshRequest$: Observable<string> | null = null;

  constructor(
    private readonly sessionService: SessionService,
    private readonly tokenStore: TokenStore
  ) {}

  refreshAccessToken(): Observable<string> {

    /*
     * Refresh zaten devam ediyorsa yeni HTTP
     * request oluşturma.
     *
     * Mevcut Observable'ı bütün bekleyen
     * request'lerle paylaş.
     */
    if (this.refreshRequest$) {
      return this.refreshRequest$;
    }

    this.refreshRequest$ = this.sessionService.refresh().pipe(

        map(response => {

          const accessToken =
            response.data?.accessToken;

          if (!accessToken) {
            throw new Error(
              'Access token yenilenemedi.'
            );
          }

          return accessToken;
        }),

        tap(accessToken => {

          this.tokenStore.setAccessToken(
            accessToken
          );
        }),

        /*
         * Refresh tamamlandığında yeni bir
         * refresh işlemine tekrar izin ver.
         */
        finalize(() => {
          this.refreshRequest$ = null;
        }),

        /*
         * Aynı anda gelen bütün 401 request'leri
         * aynı refresh sonucunu paylaşır.
         */
        shareReplay({
          bufferSize: 1,
          refCount: false
        })
      );

    return this.refreshRequest$;
  }
}