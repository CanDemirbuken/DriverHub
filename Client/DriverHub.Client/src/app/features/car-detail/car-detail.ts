import { Component, OnInit, signal } from '@angular/core';
import { CarService } from '../../core/services/car/car-service';
import { GetCarByIdResponse } from '../../core/services/car/models/get-car-by-id-response';
import { GetCarByIdRequest } from '../../core/services/car/models/get-car-by-id-request';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiResponse } from '../../core/models/api/api-response';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { RouteLinks } from '../../core/constants/route-paths';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-car-detail',
  imports: [RouterLink],
  templateUrl: './car-detail.html',
  styleUrl: './car-detail.scss',
})
export class CarDetail implements OnInit {
  carId = signal('');

  car = signal<GetCarByIdResponse | null>(null);

  isLoading = signal(false);
  errorMessage = signal('');

readonly routeLinks = RouteLinks;

  constructor(
    private readonly carService: CarService,
    private readonly route: ActivatedRoute
  ){}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id') ?? '';0

      if(!id){
        this.errorMessage.set('Id bilgisi alınamadı.');
        return;
      }

      this.carId.set(id);
      this.getById();
    })
  }

  getById(): void{
    this.isLoading.set(true);
    this.errorMessage.set('');

    const request: GetCarByIdRequest = {
      carId: this.carId()
    }

    this.carService
      .getCarById(request)
      .subscribe({
        next: response => {
          if(!response.data || !response.isSuccess){
            this.errorMessage.set('Araç bilgisi alınamadı.');
            this.isLoading.set(false);

            return;
          }

          const data = response.data;

          this.car.set(data);
          this.isLoading.set(false);
        },
        error: (
          error: HttpErrorResponse
        ) => {
          const apiResponse =
            error.error as ApiResponse<unknown>;

          this.errorMessage.set(
            apiResponse?.errors?.[0]?.message ??
              'Araç alınırken bir hata oluştu.'
          );

          this.isLoading.set(false);
        }
      })
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

  getImageUrl(path: string): string {
    if (!path) {
      return '';
    }
  
    if (
      path.startsWith('http://') ||
      path.startsWith('https://')
    ) {
      return path;
    }
  
    return `${environment.apiUrl}${path}`;
  }
}