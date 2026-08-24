import { Component, OnInit, signal } from '@angular/core';
import { BrandService } from '../../../core/services/brand/brand-service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { GetBrandByIdResponse } from '../../../core/services/brand/models/get-brand-by-id-response';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiResponse } from '../../../core/models/api/api-response';
import { ToastService } from '../../../shared/services/toast-service';
import { UpdateBrandRequest } from '../../../core/services/brand/models/update-brand-request';
import { FormsModule } from '@angular/forms';
import { RouteLinks } from '../../../core/constants/route-paths';

@Component({
  selector: 'app-edit-brand',
  imports: [
    FormsModule,
    RouterLink
  ],
  templateUrl: './edit-brand.html',
  styleUrl: './edit-brand.scss',
})
export class EditBrand implements OnInit {

  routeLinks = RouteLinks;

  constructor(
    private readonly brandService: BrandService,
    private readonly activatedRoute: ActivatedRoute,
    private readonly toastService: ToastService
  ){}

  brandId = signal('');
  brand = signal<GetBrandByIdResponse | null>(null);

  name = signal('');

  isLoading = signal(false);
  isEditing = signal(false);

  errorMessage = signal('');

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(params => {
      const id = params.get('id') ?? '';

      if(!id){
        this.errorMessage.set('Id bilgisi alınamadı.');
        return;
      }

      this.brandId.set(id);
      this.getById(id);
    });
  }

  getById(id: string): void{
    this.errorMessage.set('');
    this.isLoading.set(true);

    this.brandService
      .getBrandById(id)
      .subscribe({
        next: response => {
          if(!response.data || !response.isSuccess){
            this.errorMessage.set('Marka bilgisi alınamadı.');
            this.isLoading.set(false);
            return;
          }

          this.brand.set(response.data);
          this.name.set(response.data.name);

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

  editBrand(): void{
    const brand = this.brand();

    if(!brand){
      return;
    }

    const name = this.name().trim();

    if(!name){
      this.toastService.showErrorMessage(
        'Marka adı boş bırakılamaz.'
      );
      return;
    }

    if(name === brand.name){
      this.toastService.showErrorMessage(
        'Marka bilgisinde herhangi bir değişiklik yapılmadı.'
      );
      return;
    }

    this.errorMessage.set('');
    this.isEditing.set(true);

    const request: UpdateBrandRequest = {
      name: name
    };

    this.brandService
      .updateBrand(this.brandId(), request)
      .subscribe({
        next: () => {
          this.brand.update(currentBrand => {
            if(!currentBrand){
              return currentBrand;
            }

            return {
              ...currentBrand,
              name: name
            };
          });

          this.name.set(name);

          this.toastService.showSuccessMessage(
            'Marka bilgisi başarıyla güncellendi.'
          );

          this.isEditing.set(false);
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse = error.error as ApiResponse<unknown>;

          const message =
            apiResponse?.errors?.[0]?.message ??
            'Marka bilgisi güncellenirken bir hata oluştu.';

          this.toastService.showErrorMessage(message);

          this.isEditing.set(false);
        }
      });
  }
}