import { Component, OnInit, signal } from '@angular/core';
import { CategoryService } from '../../../core/services/category/category-service';
import { GetCategoriesResponse } from '../../../core/services/category/models/get-categories-response';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiResponse } from '../../../core/models/api/api-response';
import { ToastService } from '../../../shared/services/toast-service';
import { RouterLink } from "@angular/router";
import { RouteLinks } from '../../../core/constants/route-paths';

@Component({
  selector: 'app-category-list',
  imports: [RouterLink],
  templateUrl: './category-list.html',
  styleUrl: './category-list.scss',
})
export class CategoryList implements OnInit {

  constructor(
    private readonly categoryService: CategoryService,
    private readonly toastService: ToastService
  ){}

  categories = signal<GetCategoriesResponse[]>([]);

  errorMessage = signal('');

  isLoading = signal(false);
  isRemoving = signal(false);

  selectedCategoryForRemove = signal<GetCategoriesResponse | null>(null);

  routeLinks = RouteLinks
  
  ngOnInit(): void {
    this.getCategories();
  }

  getCategories(): void{
    this.errorMessage.set('');
    this.isLoading.set(true);

    this.categoryService
      .getCategories()
      .subscribe({
        next: response => {
          if(!response.data || !response.isSuccess){
            this.errorMessage.set(
              'Kategoriler alınırken bir hata oluştu.'
            );

            this.isLoading.set(false);
            return;
          }

          this.categories.set(response.data);

          this.isLoading.set(false);
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse =
            error.error as ApiResponse<unknown>;

          this.errorMessage.set(
            apiResponse?.errors?.[0]?.message ??
            'Kategoriler alınırken bir hata oluştu.'
          );

          this.isLoading.set(false);
        }
      });
  }

  removeCategory(id: string): void{
    this.isRemoving.set(true);

    this.categoryService
      .removeCategory(id)
      .subscribe({
        next: () => {
          this.toastService.showSuccessMessage(
            'Kategori başarıyla silindi.'
          );

          this.isRemoving.set(false);
          this.selectedCategoryForRemove.set(null);

          this.getCategories();
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse =
            error.error as ApiResponse<unknown>;

          const message =
            apiResponse?.errors?.[0]?.message ??
            'Kategori silinirken bir hata oluştu.';

          this.toastService.showErrorMessage(message);

          this.isRemoving.set(false);
        }
      });
  }

  openRemoveConfirmation(
    category: GetCategoriesResponse
  ): void{
    this.selectedCategoryForRemove.set(category);
  }

  closeRemoveConfirmation(): void{
    if(this.isRemoving()){
      return;
    }

    this.selectedCategoryForRemove.set(null);
  }

  confirmRemoveCategory(): void{
    const category =
      this.selectedCategoryForRemove();

    if(!category){
      return;
    }

    this.removeCategory(category.id);
  }
}