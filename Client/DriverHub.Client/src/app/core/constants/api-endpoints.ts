export const ApiEndpoints = {
  Authentication: {
    Login: '/api/authentication/login'
  },

  Sessions: {
    RefreshToken: '/api/sessions/refresh-token',
    Logout: '/api/sessions/logout',
    LogoutAll: '/api/sessions/logout-all'
  },
  
  Cars: {
    GetPaged: (pageNumber: number, pageSize: number) =>
      `/api/cars?PageNumber=${pageNumber}&PageSize=${pageSize}`,

    GetById: (id: string) =>
      `/api/cars/${id}`,

    Update: (id: string) =>
      `/api/cars/${id}`
  }
} as const;