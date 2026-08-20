import { Component, signal } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { BrandService } from '../../../core/services/brand/brand-service';
import { CreateBrandRequest } from '../../../core/services/brand/models/create-brand-request';
import { ApiResponse } from '../../../core/models/api/api-response';
import { RouteLinks } from '../../../core/constants/route-paths';

@Component({
  selector: 'app-create-brand',
  imports: [
    FormsModule,
    RouterLink
  ],
  templateUrl: './create-brand.html',
  styleUrl: './create-brand.scss',
})
export class CreateBrand {

  readonly routeLinks = RouteLinks;

  name = signal('');
  errorMessage = signal('');
  isCreating = signal(false);

  constructor(
    private readonly brandService: BrandService,
    private readonly router: Router
  ) {}

  createBrand(): void {
    const brandName = this.name().trim();

    if (!brandName) {
      this.errorMessage.set('Marka adı boş bırakılamaz.');
      return;
    }

    this.isCreating.set(true);
    this.errorMessage.set('');

    const request: CreateBrandRequest = {
      name: brandName
    };

    this.brandService
      .createBrand(request)
      .subscribe({
        next: response => {
          if (!response.isSuccess || !response.data) {
            this.errorMessage.set(
              'Marka eklenirken bir hata oluştu.'
            );

            this.isCreating.set(false);
            return;
          }

          this.isCreating.set(false);

          const url =
            this.routeLinks.Admin.BrandById(response.data.id);

          this.router.navigateByUrl(url);
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse =
            error.error as ApiResponse<unknown>;

          this.errorMessage.set(
            apiResponse?.errors?.[0]?.message ??
            'Marka bilgisi eklenirken bir hata oluştu.'
          );

          this.isCreating.set(false);
        }
      });
  }
}