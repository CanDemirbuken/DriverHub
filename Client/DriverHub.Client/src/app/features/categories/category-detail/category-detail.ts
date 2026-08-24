import { Component, OnInit, signal } from '@angular/core';
import { CategoryService } from '../../../core/services/category/category-service';
import { GetCategoryByIdRequest } from '../../../core/services/category/models/get-category-by-id-request';
import { GetCategoryByIdResponse } from '../../../core/services/category/models/get-category-by-id-response';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiResponse } from '../../../core/models/api/api-response';
import { ToastService } from '../../../shared/services/toast-service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { RouteLinks } from '../../../core/constants/route-paths';

@Component({
  selector: 'app-category-detail',
  imports: [RouterLink],
  templateUrl: './category-detail.html',
  styleUrl: './category-detail.scss',
})
export class CategoryDetail implements OnInit {
  constructor(
    private readonly categoryService: CategoryService,
    private readonly toastService: ToastService,
    private readonly activatedRoute: ActivatedRoute
  ){}

  categoryId = signal('');
  category = signal<GetCategoryByIdResponse | null>(null);

  errorMessage = signal('');
  isLoading = signal(false);

  routeLinks = RouteLinks;

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(p => {
      const id = p.get('id');

      if(!id){
        this.errorMessage.set('Id bilgisi alınamadı');
        this.toastService.showErrorMessage(this.errorMessage());
        return;
      }

      this.categoryId.set(id);
      this.getCategoryById(this.categoryId());
    });
  }

  getCategoryById(id: string){
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
}
