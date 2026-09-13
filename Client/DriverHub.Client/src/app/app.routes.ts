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
        loadComponent: () => import('./features/cars/car-list/cars').then(m => m.Cars)
      },
      {
        path: `${RoutePaths.Admin.Cars}/create`,
        loadComponent: () => import('./features/cars/car-create/car-create').then(m => m.CarCreate)
      },
      {
        path: `${RoutePaths.Admin.Cars}/:id`,
        loadComponent: () => import('./features/cars/car-detail/car-detail').then(m => m.CarDetail)
      },
      {
        path: `${RoutePaths.Admin.Cars}/:id/edit`,
        loadComponent: () => import('./features/cars/car-edit/car-edit').then(m => m.CarEdit)
      },
      {
        path: `${RoutePaths.Admin.Reservations}/create`,
        loadComponent: () => import('./features/reservations/reservation-create/reservation-create').then(m => m.ReservationCreate)
      },
      {
        path: RoutePaths.Admin.Brands,
        loadComponent: () => import('./features/brands/brand-list/brands').then(m => m.Brands)
      },
      {
        path: `${RoutePaths.Admin.Brands}/create`,
        loadComponent: () => import('./features/brands/brand-create/create-brand').then(m => m.CreateBrand)
      },
      {
        path: `${RoutePaths.Admin.Brands}/:id`,
        loadComponent: () => import('./features/brands/brand-detail/brand-by-id').then(m => m.BrandById)
      },
      {
        path: `${RoutePaths.Admin.Brands}/:id/edit`,
        loadComponent: () => import('./features/brands/brand-edit/edit-brand').then(m => m.EditBrand)
      },
      {
        path: `${RoutePaths.Admin.Categories}`,
        loadComponent: () => import('./features/categories/category-list/category-list').then(m => m.CategoryList)
      },
      {
        path: `${RoutePaths.Admin.Categories}/create`,
        loadComponent: () => import('./features/categories/category-create/create-category').then(m => m.CreateCategory)
      },
      {
        path: `${RoutePaths.Admin.Categories}/:id`,
        loadComponent: () => import('./features/categories/category-detail/category-detail').then(m => m.CategoryDetail)
      },
      {
        path: `${RoutePaths.Admin.Categories}/:id/edit`,
        loadComponent: () => import('./features/categories/category-edit/edit-category').then(m => m.EditCategory)
      },
      {
        path: `${RoutePaths.Admin.Locations}`,
        loadComponent: () => import('./features/locations/location-list/location-list').then(m => m.LocationList)
      },
      {
        path: `${RoutePaths.Admin.Locations}/create`,
        loadComponent: () => import('./features/locations/location-create/location-create').then(m => m.LocationCreate)
      },
      {
        path: `${RoutePaths.Admin.Locations}/:id`,
        loadComponent: () => import('./features/locations/location-detail/location-detail').then(m => m.LocationDetail)
      },
      {
        path: `${RoutePaths.Admin.Locations}/:id/edit`,
        loadComponent: () => import('./features/locations/location-edit/location-edit').then(m => m.LocationEdit)
      },
      {
        path: `${RoutePaths.Admin.Features}`,
        loadComponent: () => import('./features/car-features/feature-list/feature-list').then(m => m.FeatureList)
      },
      {
        path: `${RoutePaths.Admin.Features}/create`,
        loadComponent: () => import('./features/car-features/feature-create/feature-create').then(m => m.FeatureCreate)
      },
      {
        path: `${RoutePaths.Admin.Features}/:id`,
        loadComponent: () => import('./features/car-features/feature-detail/feature-detail').then(m => m.FeatureDetail)
      },
      {
        path: `${RoutePaths.Admin.Features}/:id/edit`,
        loadComponent: () => import('./features/car-features/feature-edit/feature-edit').then(m => m.FeatureEdit)
      },
    ]
  }
];
