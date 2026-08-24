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
import { Cars } from './features/cars/car-list/cars';
import { CarDetail } from './features/cars/car-detail/car-detail';
import { CarEdit } from './features/cars/car-edit/car-edit';
import { Brands } from './features/brands/brand-list/brands';
import { BrandById } from './features/brands/brand-detail/brand-by-id';
import { CreateBrand } from './features/brands/brand-create/create-brand';
import { EditBrand } from './features/brands/brand-edit/edit-brand';
import { CategoryList } from './features/categories/category-list/category-list';
import { CreateCategory } from './features/categories/category-create/create-category';
import { CategoryDetail } from './features/categories/category-detail/category-detail';
import { EditCategory } from './features/categories/category-edit/edit-category';
import { LocationList } from './features/locations/location-list/location-list';
import { LocationDetail } from './features/locations/location-detail/location-detail';
import { LocationCreate } from './features/locations/location-create/location-create';
import { LocationEdit } from './features/locations/location-edit/location-edit';

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
        path: `${RoutePaths.Admin.Brands}/create`,
        component: CreateBrand
      },
      {
        path: `${RoutePaths.Admin.Brands}/:id`,
        component: BrandById
      },
      {
        path: `${RoutePaths.Admin.Brands}/:id/edit`,
        component: EditBrand
      },
      {
        path: `${RoutePaths.Admin.Categories}`,
        component: CategoryList
      },
      {
        path: `${RoutePaths.Admin.Categories}/create`,
        component: CreateCategory
      },
      {
        path: `${RoutePaths.Admin.Categories}/:id`,
        component: CategoryDetail
      },
      {
        path: `${RoutePaths.Admin.Categories}/:id/edit`,
        component: EditCategory
      },
      {
        path: `${RoutePaths.Admin.Locations}`,
        component: LocationList
      },
      {
        path: `${RoutePaths.Admin.Locations}/create`,
        component: LocationCreate
      },
      {
        path: `${RoutePaths.Admin.Locations}/:id`,
        component: LocationDetail
      },
      {
        path: `${RoutePaths.Admin.Locations}/:id/edit`,
        component: LocationEdit
      }
    ]
  }
];