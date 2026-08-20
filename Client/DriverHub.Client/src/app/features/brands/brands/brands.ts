import { Component, OnInit, signal } from '@angular/core';
import { BrandService } from '../../../core/services/brand/brand-service';
import { GetBrandsResponse } from '../../../core/services/brand/models/get-brands-response';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiResponse } from '../../../core/models/api/api-response';
import { RouterLink } from '@angular/router';
import { RouteLinks } from '../../../core/constants/route-paths';
import { ToastService } from '../../../shared/services/toast-service';

@Component({
  selector: 'app-brands',
  imports: [RouterLink],
  templateUrl: './brands.html',
  styleUrl: './brands.scss',
})
export class Brands implements OnInit {

  routeLinks = RouteLinks;

  constructor(
    private readonly brandService: BrandService,
    private readonly toastService: ToastService
  ){}

  brands = signal<GetBrandsResponse[]>([]);

  isLoading = signal(false);
  errorMessage = signal('');

  isRemoving = signal(false);
  selectedBrandForRemove = signal<GetBrandsResponse | null>(null);

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
            this.errorMessage.set('Marka bilgisi alınamadı.');
            this.isLoading.set(false);
            return;
          }

          this.brands.set(response.data);
          this.isLoading.set(false);
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse = error.error as ApiResponse<unknown>;

          this.errorMessage.set(
            apiResponse?.errors?.[0]?.message ??
            'Markalar alınırken bir hata oluştu.'
          );

          this.isLoading.set(false);
        }
      });
  }

  removeBrand(id: string): void{
    this.isRemoving.set(true);

    this.brandService
      .removeBrand(id)
      .subscribe({
        next: () => {
          this.toastService.showSuccessMessage(
            'Marka bilgisi başarıyla silindi.'
          );

          this.isRemoving.set(false);
          this.selectedBrandForRemove.set(null);

          this.getBrands();
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse = error.error as ApiResponse<unknown>;

          const message =
            apiResponse?.errors?.[0]?.message ??
            'Marka silinirken bir hata oluştu.';

          this.toastService.showErrorMessage(message);

          this.isRemoving.set(false);
        }
      });
  }

  openRemoveConfirmation(brand: GetBrandsResponse): void{
    this.selectedBrandForRemove.set(brand);
  }

  closeRemoveConfirmation(): void{
    if(this.isRemoving()){
      return;
    }

    this.selectedBrandForRemove.set(null);
  }

  confirmRemoveBrand(): void{
    const brand = this.selectedBrandForRemove();

    if(!brand){
      return;
    }

    this.removeBrand(brand.id);
  }
}