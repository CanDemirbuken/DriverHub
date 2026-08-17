import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { CarService } from '../../core/services/car/car-service';

import { GetCarByIdRequest } from '../../core/services/car/models/get-car-by-id-request';
import { GetCarByIdResponse } from '../../core/services/car/models/get-car-by-id-response';
import { UpdateCarRequest } from '../../core/services/car/models/update-car-request';

import { ApiResponse } from '../../core/models/api/api-response';
import { RouteLinks } from '../../core/constants/route-paths';

@Component({
  selector: 'app-car-edit',
  imports: [FormsModule, RouterLink],
  templateUrl: './car-edit.html',
  styleUrl: './car-edit.scss',
})
export class CarEdit implements OnInit {
  carId = signal('');

  car = signal<GetCarByIdResponse | null>(null);

  isLoading = signal(false);
  isUpdating = signal(false);

  errorMessage = signal('');
  successMessage = signal('');

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

  constructor(
    private readonly carService: CarService,
    private readonly activatedRoute: ActivatedRoute
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
    });
  }

  getById(): void {
    this.isLoading.set(true);
    this.errorMessage.set('');
    this.successMessage.set('');

    const request: GetCarByIdRequest = {
      carId: this.carId()
    };

    this.carService
      .getCarById(request)
      .subscribe({
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

        error: (
          error: HttpErrorResponse
        ) => {
          const apiResponse =
            error.error as ApiResponse<unknown>;

          this.errorMessage.set(
            apiResponse?.errors?.[0]?.message ??
              'Araç bilgisi alınırken bir hata oluştu.'
          );

          this.isLoading.set(false);
        }
      });
  }

  updateCar(): void {
    this.errorMessage.set('');
    this.successMessage.set('');

    const id = this.carId();

    if (!id) {
      this.errorMessage.set('Araç Id bilgisi bulunamadı.');
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

    this.carService
      .updateCar(id, request)
      .subscribe({
        next: () => {
          this.successMessage.set('Araç başarıyla güncellendi.');
          this.isUpdating.set(false);
        },

        error: (
          error: HttpErrorResponse
        ) => {
          const apiResponse =
            error.error as ApiResponse<unknown>;

          this.errorMessage.set(
            apiResponse?.errors?.[0]?.message ??
              'Araç güncellenirken bir hata oluştu.'
          );

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
}