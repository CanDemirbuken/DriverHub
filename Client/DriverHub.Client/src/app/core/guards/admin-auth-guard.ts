import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthState } from '../auth/auth-state';
import { RouteLinks } from '../constants/route-paths';

export const adminAuthGuard: CanActivateFn = () => {
  const authState = inject(AuthState);
  const router = inject(Router);

  if (authState.isAuthenticated) {
    return true;
  }

  return router.parseUrl(RouteLinks.Admin.Login);
};