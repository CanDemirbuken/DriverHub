import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { CategoryService } from '../../../core/services/category/category-service';
import { CreateCategoryRequest } from '../../../core/services/category/models/create-category-request';
import { ToastService } from '../../../shared/services/toast-service';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiResponse } from '../../../core/models/api/api-response';
import { RouteLinks } from '../../../core/constants/route-paths';

@Component({
  selector: 'app-create-category',
  imports: [
    FormsModule,
    RouterLink
  ],
  templateUrl: './create-category.html',
  styleUrl: './create-category.scss',
})
export class CreateCategory {

  routeLinks = RouteLinks;

  constructor(
    private readonly categoryService: CategoryService,
    private readonly toastService: ToastService
  ){}

  name = signal('');

  errorMessage = signal('');
  isAdding = signal(false);

  createCategory(): void{
    const name = this.name().trim();

    if(!name){
      this.toastService.showErrorMessage('Kategori adı boş bırakılamaz.');
      return;
    }

    this.isAdding.set(true);
    this.errorMessage.set('');

    const request: CreateCategoryRequest = {
      name: name
    };

    this.categoryService
      .createCategory(request)
      .subscribe({
        next: response => {
          if(!response.data || !response.isSuccess){
            this.errorMessage.set('Kategori eklenirken bir hata oluştu.');
            this.isAdding.set(false);
            return;
          }

          this.toastService.showSuccessMessage('Kategori başarıyla eklendi.');

          this.name.set('');
          this.isAdding.set(false);
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse = error.error as ApiResponse<unknown>;
          const message = apiResponse?.errors?.[0]?.message ?? 'Kategori eklenirken bir hata oluştu.';

          this.toastService.showErrorMessage(message);
          this.isAdding.set(false);
        }
      });
  }
}