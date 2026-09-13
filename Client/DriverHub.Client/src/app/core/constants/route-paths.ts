export const RoutePaths = {
  Public: {
    Root: ''
  },
  Admin: {
    Root: 'admin',
    Login: 'login',
    ForgotPassword: 'forgot-password',
    ResetPassword: 'reset-password',
    Dashboard: 'dashboard',
    Cars: 'cars',
    Reservations: 'reservations',
    Brands: 'brands',
    Categories: 'categories',
    Locations: 'locations',
    Features: 'features'
  }
} as const;

export const RouteLinks = {
  Admin: {
    Root: `/${RoutePaths.Admin.Root}`,
    
    Login: `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Login}`,
    ForgotPassword: `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.ForgotPassword}`,
    ResetPassword: `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.ResetPassword}`,
    
    Dashboard: `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Dashboard}`,
    
    Cars: `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Cars}`,
    CreateCar: `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Cars}/create`,
    CreateReservation: `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Reservations}/create`,
    CarDetail: (id: string) => `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Cars}/${id}`,
    CarEdit: (id: string) => `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Cars}/${id}/edit`,

    Brands: `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Brands}`,
    BrandById: (id: string) => `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Brands}/${id}`,
    CreateBrand: `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Brands}/create`,
    EditBrand: (id: string) => `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Brands}/${id}/edit`,

    Categories: `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Categories}`,
    CreateCategory: `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Categories}/create`,
    CategoryDetail: (id: string) => `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Categories}/${id}`,
    EditCategory: (id: string) => `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Categories}/${id}/edit`,

    Locations: `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Locations}`,
    CreateLocation: `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Locations}/create`,
    LocationDetail: (id: string) => `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Locations}/${id}`,
    EditLocation: (id: string) => `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Locations}/${id}/edit`,

    Features: `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Features}`,
    CreateFeature: `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Features}/create`,
    FeatureDetail: (id: string) => `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Features}/${id}`,
    EditFeature: (id: string) => `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Features}/${id}/edit`
  }
} as const;
