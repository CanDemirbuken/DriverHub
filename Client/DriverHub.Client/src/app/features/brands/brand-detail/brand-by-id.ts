import { Component, OnInit, signal } from '@angular/core';
import { BrandService } from '../../../core/services/brand/brand-service';
import { GetBrandByIdResponse } from '../../../core/services/brand/models/get-brand-by-id-response';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { GetBrandByIdRequest } from '../../../core/services/brand/models/get-brand-by-id-request';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiResponse } from '../../../core/models/api/api-response';
import { RouteLinks } from '../../../core/constants/route-paths';

@Component({
  selector: 'app-brand-by-id',
  imports: [RouterLink],
  templateUrl: './brand-by-id.html',
  styleUrl: './brand-by-id.scss',
})
export class BrandById implements OnInit {
  constructor(
    private readonly brandService: BrandService,
    private readonly route: ActivatedRoute
  ){}

  routeLinks = RouteLinks

  brandId = signal('');
  brand = signal<GetBrandByIdResponse | null>(null);
  
  errorMessage = signal('');
  isLoading = signal(false);

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');

      if (!id) {
        this.errorMessage.set('Id bilgisi alınamadı.');
        return;
      }

      this.brandId.set(id);
      this.getById();
    });
  }

  getById(): void{
    this.isLoading.set(true);
    this.errorMessage.set('');

    const request: GetBrandByIdRequest = {
      brandId: this.brandId()
    }

    this.brandService
      .getBrandById(request.brandId)
      .subscribe({
        next: response => {
          if(!response.isSuccess || !response.data){
            this.errorMessage.set('Marka bilgisi alınamadı.');
            this.isLoading.set(false);
            return;
          }

          this.brand.set(response.data);
          this.isLoading.set(false);
        },

      error: (error: HttpErrorResponse) => {
        const apiResponse = error.error as ApiResponse<unknown>;

        this.errorMessage.set(
          apiResponse?.errors?.[0]?.message ??
          'Marka bilgisi alınırken bir hata oluştu.'
        );

        this.isLoading.set(false);
      }
      });
  }
}
