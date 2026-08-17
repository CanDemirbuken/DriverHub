import { Component, OnInit, signal } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

import { CarService } from '../../core/services/car/car-service';
import { GetPagedCarRequest } from '../../core/services/car/models/get-paged-car-request';
import { GetPagedCarResponse } from '../../core/services/car/models/get-paged-car-response';

import { ApiResponse } from '../../core/models/api/api-response';
import { RouterLink } from '@angular/router';
import { RouteLinks } from '../../core/constants/route-paths';

@Component({
  selector: 'app-cars',
  imports: [RouterLink],
  templateUrl: './cars.html',
  styleUrl: './cars.scss',
})
export class Cars implements OnInit {
  readonly routeLinks = RouteLinks;
  
  pageNumber = signal(1);
  pageSize = signal(10);

  cars = signal<GetPagedCarResponse[]>([]);

  totalCount = signal(0);
  totalPages = signal(0);

  hasPreviousPage = signal(false);
  hasNextPage = signal(false);

  isLoading = signal(false);
  errorMessage = signal('');

  constructor(private readonly carService: CarService) {}

  ngOnInit(): void {
    this.get();
  }

  previousPage(): void {
    if (!this.hasPreviousPage()) {
      return;
    }

    this.pageNumber.update(page => page - 1);
    this.get();
  }

  nextPage(): void {
    if (!this.hasNextPage()) {
      return;
    }

    this.pageNumber.update(page => page + 1);
    this.get();
  }

  get(): void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    const request: GetPagedCarRequest = {
      pageNumber: this.pageNumber(),
      pageSize: this.pageSize()
    };

    this.carService
      .getCars(request)
      .subscribe({
        next: response => {
          if (
            !response.isSuccess ||
            !response.data
          ) {
            this.errorMessage.set(
              'Araçlar alınamadı.'
            );

            this.isLoading.set(false);

            return;
          }

          const data = response.data;

          this.cars.set(data.items);
          this.pageNumber.set(data.pageNumber);
          this.pageSize.set(data.pageSize);
          this.totalCount.set(data.totalCount);
          this.totalPages.set(data.totalPages);
          this.hasPreviousPage.set(data.hasPreviousPage);
          this.hasNextPage.set(data.hasNextPage);
          this.isLoading.set(false);
        },

        error: (
          error: HttpErrorResponse
        ) => {
          const apiResponse =
            error.error as ApiResponse<unknown>;

          this.errorMessage.set(
            apiResponse?.errors?.[0]?.message ??
              'Araçlar alınırken bir hata oluştu.'
          );

          this.isLoading.set(false);
        }
      });
  }
}