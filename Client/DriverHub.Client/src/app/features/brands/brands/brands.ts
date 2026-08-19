import { Component, OnInit, signal } from '@angular/core';
import { BrandService } from '../../../core/services/brand/brand-service';
import { GetBrandsResponse } from '../../../core/services/brand/models/get-brands-response';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiResponse } from '../../../core/models/api/api-response';
import { RouterLink } from '@angular/router';
import { RouteLinks } from '../../../core/constants/route-paths';

@Component({
  selector: 'app-brands',
  imports: [RouterLink],
  templateUrl: './brands.html',
  styleUrl: './brands.scss',
})
export class Brands implements OnInit {
 
  routeLinks = RouteLinks

  constructor(private readonly brandService: BrandService){}
  brands = signal<GetBrandsResponse[]>([]);
  
  isLoading = signal(false);
  errorMessage = signal('');

  ngOnInit(): void {
    this.getBrands();  
  }

  getBrands(): void{
    this.isLoading.set(true);
    this.errorMessage.set('');
    
    this.brandService
      .getBrands()
      .subscribe({
        next: response => {
          if(!response.isSuccess || !response.data){
            this.errorMessage.set("Marka bilgisi alınamadı.");
            this.isLoading.set(false);
            return;
          }

          this.brands.set(response.data);
          this.isLoading.set(false);
        },

        error: (error: HttpErrorResponse ) => {
          const apiResponse = error.error as ApiResponse<unknown>;
          this.errorMessage.set(
            apiResponse?.errors?.[0]?.message ??
              'Markalar alınırken bir hata oluştu.'
          );

          this.isLoading.set(false);
        }
      })
  }
}
