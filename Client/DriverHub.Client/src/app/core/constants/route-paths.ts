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
    Cars: 'cars'
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
    CarDetail: (id: string) => `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Cars}/${id}`,
    CarEdit: (id: string) => `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Cars}/${id}/edit`
  }
} as const;