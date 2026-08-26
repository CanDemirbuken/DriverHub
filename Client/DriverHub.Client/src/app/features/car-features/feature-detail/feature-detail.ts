import { Component, OnInit, signal } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute, RouterLink } from '@angular/router';

import { FeatureService } from '../../../core/services/feature/feature-service';
import { GetFeatureByIdResponse } from '../../../core/services/feature/models/get-feature-by-id-response';
import { GetFeatureByIdRequest } from '../../../core/services/feature/models/get-feature-by-id-request';
import { ToastService } from '../../../shared/services/toast-service';
import { ApiResponse } from '../../../core/models/api/api-response';
import { RouteLinks } from '../../../core/constants/route-paths';

@Component({
  selector: 'app-feature-detail',
  imports: [RouterLink],
  templateUrl: './feature-detail.html',
  styleUrl: './feature-detail.scss',
})
export class FeatureDetail implements OnInit {

  constructor(
    private readonly featureService: FeatureService,
    private readonly toastService: ToastService,
    private readonly activatedRoute: ActivatedRoute
  ){}

  routeLinks = RouteLinks;

  featureId = signal('');
  feature = signal<GetFeatureByIdResponse | null>(null);

  isLoading = signal(false);

  errorMessage = signal('');

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(params => {
      const id = params.get('id');

      if(!id){
        this.errorMessage.set('Id bilgisi alınamadı.');
        this.toastService.showErrorMessage(this.errorMessage());
        return;
      }

      this.featureId.set(id);

      this.getFeatureById(this.featureId());
    });
  }

  getFeatureById(id: string): void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    const request: GetFeatureByIdRequest = {
      id: id
    };

    this.featureService
      .getFeatureById(request)
      .subscribe({
        next: response => {
          if(!response.data || !response.isSuccess){
            this.errorMessage.set(
              'Araç özelliği bilgisi alınırken bir hata oluştu.'
            );

            this.toastService.showErrorMessage(
              this.errorMessage()
            );

            this.isLoading.set(false);
            return;
          }

          this.feature.set(response.data);

          this.isLoading.set(false);
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse = error.error as ApiResponse<unknown>;

          this.errorMessage.set(
            apiResponse?.errors?.[0]?.message ??
            'Araç özelliği bilgisi alınırken bir hata oluştu.'
          );

          this.toastService.showErrorMessage(
            this.errorMessage()
          );

          this.isLoading.set(false);
        }
      });
  }
}