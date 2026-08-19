import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { CarService } from '../../core/services/car/car-service';
import { BrandService } from '../../core/services/brand/brand-service';
import { CategoryService } from '../../core/services/category/category-service';
import { LocationService } from '../../core/services/location/location-service';
import { MediaService } from '../../core/services/media/media-service';

import { GetCarByIdRequest } from '../../core/services/car/models/get-car-by-id-request';
import { GetCarByIdResponse } from '../../core/services/car/models/get-car-by-id-response';
import { UpdateCarRequest } from '../../core/services/car/models/update-car-request';

import { GetBrandsResponse } from '../../core/services/brand/models/get-brands-response';
import { GetCategoriesResponse } from '../../core/services/category/models/get-categories-response';
import { GetLocationsResponse } from '../../core/services/location/models/get-locations-response';

import { ApiResponse } from '../../core/models/api/api-response';
import { RouteLinks } from '../../core/constants/route-paths';

import { ImageUrlHelper } from '../../shared/helpers/image-url-helper';
import { ToastService } from '../../shared/services/toast-service';

@Component({
  selector: 'app-car-edit',
  imports: [FormsModule, RouterLink],
  templateUrl: './car-edit.html',
  styleUrl: './car-edit.scss',
})
export class CarEdit implements OnInit {

  carId = signal('');
  car = signal<GetCarByIdResponse | null>(null);

  brands = signal<GetBrandsResponse[]>([]);
  categories = signal<GetCategoriesResponse[]>([]);
  locations = signal<GetLocationsResponse[]>([]);

  isLoading = signal(false);
  isUpdating = signal(false);

  isCoverImageUploading = signal(false);
  isBigImageUploading = signal(false);

  selectedCoverFileName = signal('');
  selectedBigFileName = signal('');

  errorMessage = signal('');

  readonly routeLinks = RouteLinks;

  brandId = '';
  categoryId = '';
  currentLocationId = '';

  model = '';
  modelYear = 0;

  plate = '';
  vin = '';

  coverImageUrl = '';
  bigImageUrl = '';

  km = 0;

  transmission = '';
  fuel = '';
  color = '';

  seat = 0;
  luggage = 0;

  imageUrlHelper = ImageUrlHelper;

  constructor(
    private readonly carService: CarService,
    private readonly brandService: BrandService,
    private readonly categoryService: CategoryService,
    private readonly locationService: LocationService,
    private readonly mediaService: MediaService,
    private readonly activatedRoute: ActivatedRoute,
    private readonly toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(params => {
      const id = params.get('id') ?? '';

      if (!id) {
        this.errorMessage.set('Araç Id bilgisi alınamadı.');
        return;
      }

      this.carId.set(id);

      this.getById();
      this.getBrands();
      this.getCategories();
      this.getLocations();
    });
  }

  getById(): void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    const request: GetCarByIdRequest = { carId: this.carId() };

    this.carService.getCarById(request).subscribe({
      next: response => {
        if (!response.isSuccess || !response.data) {
          this.errorMessage.set('Araç bilgisi alınamadı.');
          this.isLoading.set(false);
          return;
        }

        const data = response.data;

        this.car.set(data);
        this.fillEditState(data);
        this.isLoading.set(false);
      },

      error: (error: HttpErrorResponse) => {
        const apiResponse = error.error as ApiResponse<unknown>;

        this.errorMessage.set(apiResponse?.errors?.[0]?.message ?? 'Araç bilgisi alınırken bir hata oluştu.');
        this.isLoading.set(false);
      }
    });
  }

  getBrands(): void {
    this.brandService.getBrands().subscribe({
      next: response => {
        if (!response.isSuccess || !response.data) {
          this.errorMessage.set('Marka bilgisi alınamadı.');
          return;
        }

        this.brands.set(response.data);
      },

      error: (error: HttpErrorResponse) => {
        const apiResponse = error.error as ApiResponse<unknown>;

        this.errorMessage.set(apiResponse?.errors?.[0]?.message ?? 'Marka listesi alınırken bir hata oluştu.');
      }
    });
  }

  getCategories(): void {
    this.categoryService.getCategories().subscribe({
      next: response => {
        if (!response.isSuccess || !response.data) {
          this.errorMessage.set('Kategori bilgisi alınamadı.');
          return;
        }

        this.categories.set(response.data);
      },

      error: (error: HttpErrorResponse) => {
        const apiResponse = error.error as ApiResponse<unknown>;

        this.errorMessage.set(apiResponse?.errors?.[0]?.message ?? 'Kategori listesi alınırken bir hata oluştu.');
      }
    });
  }

  getLocations(): void {
    this.locationService.getLocations().subscribe({
      next: response => {
        if (!response.isSuccess || !response.data) {
          this.errorMessage.set('Lokasyon bilgisi alınamadı.');
          return;
        }

        this.locations.set(response.data);
      },

      error: (error: HttpErrorResponse) => {
        const apiResponse = error.error as ApiResponse<unknown>;

        this.errorMessage.set(apiResponse?.errors?.[0]?.message ?? 'Lokasyon listesi alınırken bir hata oluştu.');
      }
    });
  }

  onCoverImageSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0];

    if (!file) {
      return;
    }

    this.selectedCoverFileName.set(file.name);
    this.uploadCoverImage(file);
  }

  onBigImageSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0];

    if (!file) {
      return;
    }

    this.selectedBigFileName.set(file.name);
    this.uploadBigImage(file);
  }

  updateCar(): void {
    this.errorMessage.set('');

    const id = this.carId();

    if (!id) {
      this.toastService.showErrorMessage('Araç Id bilgisi bulunamadı.');
      return;
    }

    const request: UpdateCarRequest = {
      brandId: this.brandId,
      categoryId: this.categoryId,
      currentLocationId: this.currentLocationId,
      model: this.model,
      modelYear: this.modelYear,
      plate: this.plate,
      vin: this.vin,
      coverImageUrl: this.coverImageUrl,
      km: this.km,
      transmission: this.transmission,
      seat: this.seat,
      luggage: this.luggage,
      fuel: this.fuel,
      color: this.color,
      bigImageUrl: this.bigImageUrl
    };

    this.isUpdating.set(true);

    this.carService.updateCar(id, request).subscribe({
      next: () => {
        this.toastService.showSuccessMessage('Araç başarıyla güncellendi.');
        this.isUpdating.set(false);
      },

      error: (error: HttpErrorResponse) => {
        const apiResponse = error.error as ApiResponse<unknown>;
        const message = apiResponse?.errors?.[0]?.message ?? 'Araç güncellenirken bir hata oluştu.';

        this.toastService.showErrorMessage(message);
        this.isUpdating.set(false);
      }
    });
  }

  private fillEditState(data: GetCarByIdResponse): void {
    this.brandId = data.brandId;
    this.categoryId = data.categoryId;
    this.currentLocationId = data.currentLocationId;
    this.model = data.model;
    this.modelYear = data.modelYear;
    this.plate = data.plate;
    this.vin = data.vin;
    this.coverImageUrl = data.coverImageUrl;
    this.bigImageUrl = data.bigImageUrl;
    this.km = data.km;
    this.transmission = data.transmission;
    this.fuel = data.fuel;
    this.color = data.color;
    this.seat = data.seat;
    this.luggage = data.luggage;
  }

  private uploadCoverImage(file: File): void {
    this.errorMessage.set('');
    this.isCoverImageUploading.set(true);

    this.mediaService.upload(file).subscribe({
      next: response => {
        if (!response.isSuccess || !response.data) {
          this.toastService.showErrorMessage('Kapak görseli yüklenemedi.');
          this.isCoverImageUploading.set(false);
          return;
        }

        this.coverImageUrl = response.data.path;
        this.isCoverImageUploading.set(false);
      },

      error: (error: HttpErrorResponse) => {
        const apiResponse = error.error as ApiResponse<unknown>;
        const message = apiResponse?.errors?.[0]?.message ?? 'Kapak görseli yüklenirken bir hata oluştu.';

        this.toastService.showErrorMessage(message);
        this.isCoverImageUploading.set(false);
      }
    });
  }

  private uploadBigImage(file: File): void {
    this.errorMessage.set('');
    this.isBigImageUploading.set(true);

    this.mediaService.upload(file).subscribe({
      next: response => {
        if (!response.isSuccess || !response.data) {
          this.toastService.showErrorMessage('Büyük görsel yüklenemedi.');
          this.isBigImageUploading.set(false);
          return;
        }

        this.bigImageUrl = response.data.path;
        this.isBigImageUploading.set(false);
      },

      error: (error: HttpErrorResponse) => {
        const apiResponse = error.error as ApiResponse<unknown>;
        const message = apiResponse?.errors?.[0]?.message ?? 'Büyük görsel yüklenirken bir hata oluştu.';

        this.toastService.showErrorMessage(message);
        this.isBigImageUploading.set(false);
      }
    });
  }
}