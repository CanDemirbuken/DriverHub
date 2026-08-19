import { Routes } from '@angular/router';
import { RoutePaths } from './core/constants/route-paths';
import { Dashboard } from './features/dashboard/dashboard';
import { AdminLayout } from './layouts/admin-layout/admin-layout';
import { PublicLayout } from './layouts/public-layout/public-layout';
import { adminAuthGuard } from './core/guards/admin-auth-guard';
import { AdminLogin } from './features/admin-login/admin-login';
import { ForgotPassword } from './features/forgot-password/forgot-password';
import { ResetPassword } from './features/reset-password/reset-password';
import { adminGuestGuard } from './core/guards/admin-guest-guard';
import { Cars } from './features/cars/cars';
import { CarDetail } from './features/car-detail/car-detail';
import { CarEdit } from './features/car-edit/car-edit';
import { Brands } from './features/brands/brands/brands';
import { BrandById } from './features/brands/brand-by-id/brand-by-id';

export const routes: Routes = [
  {
    path: RoutePaths.Public.Root,
    component: PublicLayout
  },
  {
    path: `${RoutePaths.Admin.Root}/${RoutePaths.Admin.Login}`,
    component: AdminLogin,
    canActivate: [adminGuestGuard]
  },
  {
    path: `${RoutePaths.Admin.Root}/${RoutePaths.Admin.ForgotPassword}`,
    component: ForgotPassword
  },
  {
    path: `${RoutePaths.Admin.Root}/${RoutePaths.Admin.ResetPassword}`,
    component: ResetPassword
  },
  {
    path: RoutePaths.Admin.Root,
    component: AdminLayout,
    canActivate: [adminAuthGuard],
    children: [
      {
        path: '',
        redirectTo: RoutePaths.Admin.Dashboard,
        pathMatch: 'full'
      },
      {
        path: RoutePaths.Admin.Dashboard,
        component: Dashboard
      },
      {
        path: RoutePaths.Admin.Cars,
        component: Cars
      },
      {
        path: `${RoutePaths.Admin.Cars}/:id`,
        component: CarDetail
      },
      {
        path: `${RoutePaths.Admin.Cars}/:id/edit`,
        component: CarEdit
      },
      {
        path: RoutePaths.Admin.Brands,
        component: Brands
      },
      {
        path: `${RoutePaths.Admin.Brands}/:id`,
        component: BrandById
      }
    ]
  }
];