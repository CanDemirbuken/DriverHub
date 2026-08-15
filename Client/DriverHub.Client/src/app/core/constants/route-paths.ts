export const RoutePaths = {
  Public: {
    Root: ''
  },
  Admin: {
    Root: 'admin',
    Login: 'login',
    ForgotPassword: 'forgot-password',
    ResetPassword: 'reset-password',
    Dashboard: 'dashboard'
  }
} as const;

export const RouteLinks = {
  Admin: {
    Root: `/${RoutePaths.Admin.Root}`,
    Login: `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Login}`,
    ForgotPassword: `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.ForgotPassword}`,
    ResetPassword: `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.ResetPassword}`,
    Dashboard: `/${RoutePaths.Admin.Root}/${RoutePaths.Admin.Dashboard}`
  }
} as const;