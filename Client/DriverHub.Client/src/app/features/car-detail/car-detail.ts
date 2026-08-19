import { Component, OnInit, signal } from '@angular/core';
import { CarService } from '../../core/services/car/car-service';
import { GetCarByIdResponse } from '../../core/services/car/models/get-car-by-id-response';
import { GetCarByIdRequest } from '../../core/services/car/models/get-car-by-id-request';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiResponse } from '../../core/models/api/api-response';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { RouteLinks } from '../../core/constants/route-paths';
import { ImageUrlHelper } from '../../shared/helpers/image-url-helper';
import { CarStatus, UpdateCarStatusRequest } from '../../core/services/car/models/update-car-status-request';
import { ToastService } from '../../shared/services/toast-service';
import { FormsModule } from '@angular/forms';
import { GetLocationsResponse } from '../../core/services/location/models/get-locations-response';
import { LocationService } from '../../core/services/location/location-service';
import { UpdateCarLocationRequest } from '../../core/services/car/models/update-car-location-request';
import { PricingType, UpdateCarPricingsRequest } from '../../core/services/car/models/update-car-pricings-request';
import { EditableCarPricing } from '../../core/services/car/models/editable-car-pricing';
import { EditableCarFeatures } from '../../core/services/car/models/editable-car-features';
import { FeatureService } from '../../core/services/feature/feature-service';
import { GetFeaturesResponse } from '../../core/services/feature/models/get-features-response';
import { UpdateCarFeaturesRequest } from '../../core/services/car/models/update-car-features-request';

@Component({
  selector: 'app-car-detail',
  imports: [RouterLink, FormsModule],
  templateUrl: './car-detail.html',
  styleUrl: './car-detail.scss',
})
export class CarDetail implements OnInit {
  carId = signal('');

  car = signal<GetCarByIdResponse | null>(null);

  isLoading = signal(false);
  errorMessage = signal('');

  imageUrlHelper = ImageUrlHelper;

  selectedStatus = signal<CarStatus>(CarStatus.Active);
  isStatusUpdating = signal(false);

  readonly routeLinks = RouteLinks;

  locations = signal<GetLocationsResponse[]>([]);
  selectedLocationId = signal('');
  isLocationUpdating = signal(false);

  editablePricings = signal<EditableCarPricing[]>([]);
  isPricingUpdating = signal(false);

  features = signal<GetFeaturesResponse[]>([]);
  editableFeatures = signal<EditableCarFeatures[]>([]);
  isFeatureUpdating = signal(false);
  isFeatureEditing = signal(false);

  constructor(
    private readonly carService: CarService,
    private readonly route: ActivatedRoute,
    private readonly toastService: ToastService,
    private readonly locationService: LocationService,
    private readonly featureService: FeatureService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id') ?? '';

      if (!id) {
        this.errorMessage.set('Id bilgisi alınamadı.');
        return;
      }

      this.carId.set(id);
      this.getById();
      this.getLocations();
      this.getFeatures();
    });
  }

  updateCarFeatures(): void {
    const currentCar = this.car();

    if (!currentCar) {
      return;
    }

    if (!this.hasFeatureChanges()) {
      return;
    }

    const featureIds = this.editableFeatures()
      .filter(feature => feature.isSelected)
      .map(feature => feature.featureId);

    const request: UpdateCarFeaturesRequest = {
      featureIds: featureIds
    };

    this.isFeatureUpdating.set(true);

    this.carService.updateCarFeatures(this.carId(), request).subscribe({
      next: () => {
        const updatedFeatures = this.editableFeatures()
          .filter(feature => feature.isSelected)
          .map(feature => ({
            featureId: feature.featureId,
            featureName: feature.name
          }));

        this.car.update(car => car ? {
          ...car,
          features: updatedFeatures
        } : car);

        this.toastService.showSuccessMessage('Araç özellikleri başarıyla güncellendi.');

        this.isFeatureUpdating.set(false);
      },

      error: (error: HttpErrorResponse) => {
        const apiResponse = error.error as ApiResponse<unknown>;
        const message = apiResponse?.errors?.[0]?.message ?? 'Araç özellikleri güncellenirken bir hata oluştu.';

        this.toastService.showErrorMessage(message);

        this.isFeatureUpdating.set(false);
      }
    });
  }

  startFeatureEditing(): void {
    this.initializeEditableFeatures();
    this.isFeatureEditing.set(true);
  }
  
  cancelFeatureEditing(): void {
    this.initializeEditableFeatures();
    this.isFeatureEditing.set(false);
  }

  getFeatures(): void {
    this.featureService
      .getFeatures()
      .subscribe({
        next: response => {
          if (!response.isSuccess || !response.data) {
            this.errorMessage.set('Özellik bilgisi alınamadı.');
            return;
          }

          this.features.set(response.data);

          this.initializeEditableFeatures();
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse = error.error as ApiResponse<unknown>;
          const message = apiResponse?.errors?.[0]?.message ?? 'Özellik bilgisi alınırken bir hata oluştu.';

          this.toastService.showErrorMessage(message);
        }
      });
  }

  private initializeEditableFeatures(): void {
    const currentCar = this.car();

    if (!currentCar) {
      return;
    }

    const editableFeatures: EditableCarFeatures[] = this.features()
      .map(feature => ({
        featureId: feature.id,
        name: feature.name,
        isSelected: currentCar.features.some(
          carFeature => carFeature.featureId === feature.id
        )
      }));

    this.editableFeatures.set(editableFeatures);
  }

  toggleFeature(featureId: string): void {
    this.editableFeatures.update(features =>
      features.map(feature =>
        feature.featureId === featureId
          ? {
              ...feature,
              isSelected: !feature.isSelected
            }
          : feature
      )
    );
  }

  hasFeatureChanges(): boolean {
    const currentCar = this.car();

    if (!currentCar) {
      return false;
    }

    const currentFeatureIds = currentCar.features
      .map(feature => feature.featureId)
      .sort();

    const editableFeatureIds = this.editableFeatures()
      .filter(feature => feature.isSelected)
      .map(feature => feature.featureId)
      .sort();

    if (currentFeatureIds.length !== editableFeatureIds.length) {
      return true;
    }

    return currentFeatureIds.some(
      (id, index) => id !== editableFeatureIds[index]
    );
  }

  cancelFeatureChanges(): void {
    this.initializeEditableFeatures();
  }

  updateCarPricings(): void {
    const currentCar = this.car();
    
    if (!currentCar) {
      return;
    }
  
    const pricings = this.editablePricings()
      .filter(pricing => pricing.amount !== null && pricing.amount > 0)
      .map(pricing => ({
        type: pricing.type,
        amount: pricing.amount!
      }));
    
    if (pricings.length === 0) {
      this.toastService.showErrorMessage('En az bir geçerli fiyat girilmelidir.');
      return;
    }
  
    const request: UpdateCarPricingsRequest = {
      pricings: pricings
    };
  
    this.isPricingUpdating.set(true);
  
    this.carService.updateCarPricings(this.carId(), request).subscribe({
      next: () => {
        this.car.update(car => car ? {
          ...car,
          pricings: pricings
        } : car);
      
        this.toastService.showSuccessMessage('Araç fiyatlandırmaları başarıyla güncellendi.');
        this.isPricingUpdating.set(false);
      },
    
      error: (error: HttpErrorResponse) => {
        const apiResponse = error.error as ApiResponse<unknown>;
        const message = apiResponse?.errors?.[0]?.message ?? 'Araç fiyatlandırmaları güncellenirken bir hata oluştu.';
      
        this.toastService.showErrorMessage(message);
        this.isPricingUpdating.set(false);
      }
    });
  }

  getLocations(): void {
    this.locationService
      .getLocations()
      .subscribe({
        next: response => {
          if (!response.isSuccess || !response.data) {
            this.errorMessage.set('Lokasyon bilgisi alınamadı.');
            return;
          }

          this.locations.set(response.data);
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse = error.error as ApiResponse<unknown>;
          const message = apiResponse?.errors?.[0]?.message ?? 'Lokasyon bilgisi alınırken bir hata oluştu.';

          this.toastService.showErrorMessage(message);
        }
      });
  }

  updateCarLocation(): void {
    const currentCar = this.car();

    if (!currentCar) {
      return;
    }

    if (this.selectedLocationId() === currentCar.currentLocationId) {
      return;
    }

    this.isLocationUpdating.set(true);

    const request: UpdateCarLocationRequest = {
      currentLocationId: this.selectedLocationId()
    };

    this.carService.updateCarLocation(this.carId(), request).subscribe({
      next: () => {
        const selectedLocation = this.locations().find(location => location.id === this.selectedLocationId());

        this.car.update(car => car && selectedLocation ? {
          ...car,
          currentLocationId: selectedLocation.id,
          currentLocationName: selectedLocation.name
        } : car);

        this.toastService.showSuccessMessage('Araç lokasyon bilgisi başarıyla güncellendi.');
        this.isLocationUpdating.set(false);
      },

      error: (error: HttpErrorResponse) => {
        const apiResponse = error.error as ApiResponse<unknown>;
        const message = apiResponse?.errors?.[0]?.message ?? 'Araç lokasyonu güncellenirken bir hata oluştu.';

        this.toastService.showErrorMessage(message);

        this.isLocationUpdating.set(false);
      }
    });
  }

  updateCarStatus(): void {
    const currentCar = this.car();

    if (!currentCar) {
      return;
    }

    if (this.selectedStatus() === currentCar.status) {
      return;
    }

    this.isStatusUpdating.set(true);

    const request: UpdateCarStatusRequest = {
      status: this.selectedStatus()
    };

    this.carService.updateCarStatus(this.carId(), request).subscribe({
      next: () => {
        this.car.update(car => car ? { ...car, status: this.selectedStatus() } : car);

        this.toastService.showSuccessMessage('Araç statüsü başarıyla güncellendi.');

        this.isStatusUpdating.set(false);
      },

      error: (error: HttpErrorResponse) => {
        const apiResponse = error.error as ApiResponse<unknown>;
        const message = apiResponse?.errors?.[0]?.message ?? 'Araç statüsü güncellenirken bir hata oluştu.';

        this.toastService.showErrorMessage(message);

        this.isStatusUpdating.set(false);
      }
    });
  }

  getById(): void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    const request: GetCarByIdRequest = {
      carId: this.carId()
    };

    this.carService.getCarById(request).subscribe({
      next: response => {
        if (!response.data || !response.isSuccess) {
          this.errorMessage.set('Araç bilgisi alınamadı.');
          this.isLoading.set(false);

          return;
        }

        const data = response.data;

        this.car.set(data);
        this.selectedStatus.set(data.status as CarStatus);
        this.selectedLocationId.set(data.currentLocationId);

        this.initializeEditableFeatures();
        
        const pricingTypes = [
          PricingType.Daily,
          PricingType.Weekly,
          PricingType.Monthly
        ];

        const editablePricings = pricingTypes.map(type => {
          const existingPricing = data.pricings.find(pricing => pricing.type === type);
        
          return {
            type: type,
            amount: existingPricing?.amount ?? null
          };
        });

        this.editablePricings.set(editablePricings);

        this.isLoading.set(false);
      },

      error: (error: HttpErrorResponse) => {
        const apiResponse = error.error as ApiResponse<unknown>;

        this.errorMessage.set(
          apiResponse?.errors?.[0]?.message ??
          'Araç alınırken bir hata oluştu.'
        );

        this.isLoading.set(false);
      }
    });
  }

  getPricingTypeLabel(type: number): string {
    switch (type) {
      case 1:
        return 'Günlük';

      case 2:
        return 'Haftalık';

      case 3:
        return 'Aylık';

      default:
        return 'Fiyat';
    }
  }

  getCarStatusLabel(type: number): string {
    switch (type) {
      case 1:
        return 'Aktif';

      case 2:
        return 'Bakımda';

      case 3:
        return 'Servis Dışı';

      case 4:
        return 'Hasarlı';

      case 5:
        return 'Kullanım Dışı';

      default:
        return 'Bilinmiyor';
    }
  }
}