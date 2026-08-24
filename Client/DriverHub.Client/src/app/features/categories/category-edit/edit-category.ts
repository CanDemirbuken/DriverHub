import { Component, OnInit, signal } from '@angular/core';
import { CategoryService } from '../../../core/services/category/category-service';
import { ToastService } from '../../../shared/services/toast-service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { GetCategoryByIdRequest } from '../../../core/services/category/models/get-category-by-id-request';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiResponse } from '../../../core/models/api/api-response';
import { GetCategoryByIdResponse } from '../../../core/services/category/models/get-category-by-id-response';
import { UpdateCategoryRequest } from '../../../core/services/category/models/update-category-request';
import { FormsModule } from '@angular/forms';
import { RouteLinks } from '../../../core/constants/route-paths';

@Component({
  selector: 'app-edit-category',
  imports: [FormsModule, RouterLink],
  templateUrl: './edit-category.html',
  styleUrl: './edit-category.scss',
})
export class EditCategory implements OnInit {

  constructor(
    private readonly categoryService: CategoryService,
    private readonly toastService: ToastService,
    private readonly activatedRoute: ActivatedRoute
  ){}

  routeLinks = RouteLinks;

  categoryId = signal('');
  category = signal<GetCategoryByIdResponse | null>(null);

  isLoading = signal(false);
  errorMessage = signal('');

  name = signal('');
  isUpdating = signal(false);

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(p => {
      const id = p.get('id');
      
      if(!id){
        this.errorMessage.set('Id bilgisi alınamadı.');
        this.toastService.showErrorMessage(this.errorMessage());
        return;
      }

      this.categoryId.set(id);
      this.getCategoryById(this.categoryId());
    });
  }

  getCategoryById(id: string): void{
    this.isLoading.set(true);
    this.errorMessage.set('');

    const request: GetCategoryByIdRequest = {
      id: id
    }

    this.categoryService
      .getCategoryById(request)
      .subscribe({
        next: response => {
          if(!response.data || !response.isSuccess){
            this.errorMessage.set('Kategori bilgisi alınırken bir hata oluştu.');
            this.isLoading.set(false);
            return;
          }

          this.category.set(response.data);
          this.name.set(response.data.name);
          this.isLoading.set(false);          
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse = error.error as ApiResponse<unknown>;
          this.errorMessage.set(apiResponse.errors[0]?.message ?? 'Kategori bilgisi alınırken bir hata oluştu.');

          this.toastService.showErrorMessage(this.errorMessage());
          this.isLoading.set(false);
        }
      })
  }

  updateCategory(): void{
    const category = this.category();

    if(!category){
      return;
    }

    const name = this.name().trim();

    if(!name){
      this.toastService.showErrorMessage('Kategori adı boş bırakılamaz.');
      return;
    }

    if(name === category.name){
      this.toastService.showErrorMessage('Kategori bilgisinde herhangi bir değişiklik yapılmadı.');
      return;
    }

    this.errorMessage.set('');
    this.isUpdating.set(true);

    const request: UpdateCategoryRequest = {
      name: name
    }

    this.categoryService
      .updateCategory(this.categoryId(), request)
      .subscribe({
        next: () => {
          this.category.update(currentCategory => {
            if(!currentCategory){
              return currentCategory;
            }

            return {
              ...currentCategory,
              name: name
            };
          });

          this.name.set(name);

          this.toastService.showSuccessMessage('Kategori bilgisi başarıyla güncellendi.');
          this.isUpdating.set(false);
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse = error.error as ApiResponse<unknown>;
          const message = apiResponse?.errors?.[0]?.message ?? 'Kategori bilgisi güncellenirken bir hata oluştu.';

          this.toastService.showErrorMessage(message);
          this.isUpdating.set(false);
        }
      })
  }
}
