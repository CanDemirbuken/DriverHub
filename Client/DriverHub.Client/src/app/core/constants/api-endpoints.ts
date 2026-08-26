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
      `/api/cars/${id}/pricings`,

    UpdateFeatures: (id: string) =>
      `/api/cars/${id}/features`
  },

  Brands: {
    GetBrands: `/api/brands`,
    
    CreateBrand: `/api/brands`,

    GetById: (id: string) =>
      `/api/brands/${id}`,

    RemoveBrand: (id: string) =>
      `/api/brands/${id}`,

    EditBrand: (id: string) =>
      `/api/brands/${id}`
  },

  Categories: {
    GetCategories: `/api/categories`,

    RemoveCategory: (id: string) =>
      `/api/categories/${id}`,

    CreateCategory:  `/api/categories`,

    GetCategoryById: (id: string) =>
      `/api/categories/${id}`,

    EditCategory: (id: string) =>
      `/api/categories/${id}`
  },

  Locations: {
    GetLocations: `/api/locations`,

    GetLocationById: (id: string) => 
      `/api/locations/${id}`,

    CreateLocation: `/api/locations`,

    RemoveLocation: (id: string) =>
      `/api/locations/${id}`,

    EditLocation: (id: string) => 
      `/api/locations/${id}`
  },

  Features: {
    GetFeatures: `/api/features`,

    GetFeatureById: (id: string) =>
      `/api/features/${id}`,

    CreateFeature:  `/api/features`,

    RemoveFeature: (id: string) =>
      `/api/features/${id}`,

    EditFeature: (id: string) =>
      `/api/features/${id}`
  },

  Media: {
    Upload: `/api/media/upload`
  }
} as const;