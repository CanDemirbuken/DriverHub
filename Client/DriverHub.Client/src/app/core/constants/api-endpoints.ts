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
      `/api/cars/${id}`,

    UpdateStatus: (id: string) =>
      `/api/cars/${id}/status`,

    UpdateLocation: (id: string) => 
      `/api/cars/${id}/location`,

    UpdatePricings: (id: string) =>
      `/api/cars/${id}/pricings`
  },

  Brands: {
    GetBrands: `/api/brands`
  },

  Categories: {
    GetCategories: `/api/categories`
  },

  Locations: {
    GetLocations: `/api/locations`
  },

  Media: {
    Upload: `/api/media/upload`
  }
} as const;