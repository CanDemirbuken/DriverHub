import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, switchMap, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { TokenStore } from '../auth/token-store';
import { RefreshCoordinator } from '../auth/refresh-coordinator';
import { RouteLinks } from '../constants/route-paths';
import { SKIP_AUTH } from '../auth/auth-context';

export const authInterceptor:
  HttpInterceptorFn = (request, next) => {
    const tokenStore = inject(TokenStore);
    const refreshCoordinator = inject(RefreshCoordinator);
    const router = inject(Router);

    /*
     * Login / refresh gibi auth pipeline'ından
     * geçmesini istemediğimiz request'ler.
     */
    if (request.context.get(SKIP_AUTH)) {
      return next(request);
    }

    const accessToken = tokenStore.getAccessToken();

    const authenticatedRequest =
      accessToken
        ? request.clone({
            setHeaders: {
              Authorization:
                `Bearer ${accessToken}`
            }
          })
        : request;

    return next(authenticatedRequest).pipe(

      catchError(
        (error: HttpErrorResponse) => {

          /*
           * 401 dışındaki hatalara dokunmuyoruz.
           *
           * 400, 403, 404, 409, 500...
           * olduğu gibi devam eder.
           */
          if (error.status !== 401) {
            return throwError(
              () => error
            );
          }

          /*
           * Access token geçersiz / expire.
           *
           * Refresh token ile yeni access token
           * almaya çalış.
           */
          return refreshCoordinator
            .refreshAccessToken()
            .pipe(

              switchMap(
                newAccessToken => {

                  /*
                   * Başarısız olan orijinal
                   * request'i YENİ token ile
                   * tekrar oluşturuyoruz.
                   */
                  const retryRequest =
                    request.clone({
                      setHeaders: {
                        Authorization:
                          `Bearer ${newAccessToken}`
                      }
                    });

                  return next(
                    retryRequest
                  );
                }
              ),

              catchError(
                refreshError => {

                  /*
                   * Refresh token da artık
                   * kullanılamıyorsa session bitti.
                   */
                  tokenStore.clear();

                  void router.navigateByUrl(
                    RouteLinks.Admin.Login
                  );

                  return throwError(
                    () => refreshError
                  );
                }
              )
            );
        }
      )
    );
  };