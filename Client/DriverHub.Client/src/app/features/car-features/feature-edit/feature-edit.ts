import { Component, OnInit, signal } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';

import { FeatureService } from '../../../core/services/feature/feature-service';
import { ToastService } from '../../../shared/services/toast-service';

import { GetFeatureByIdResponse } from '../../../core/services/feature/models/get-feature-by-id-response';
import { GetFeatureByIdRequest } from '../../../core/services/feature/models/get-feature-by-id-request';
import { UpdateFeatureRequest } from '../../../core/services/feature/models/update-feature-request';

import { ApiResponse } from '../../../core/models/api/api-response';
import { RouteLinks } from '../../../core/constants/route-paths';

@Component({
  selector: 'app-feature-edit',
  imports: [FormsModule, RouterLink],
  templateUrl: './feature-edit.html',
  styleUrl: './feature-edit.scss',
})
export class FeatureEdit implements OnInit {

  constructor(
    private readonly featureService: FeatureService,
    private readonly activatedRoute: ActivatedRoute,
    private readonly toastService: ToastService
  ){}

  featureId = signal('');

  feature = signal<GetFeatureByIdResponse | null>(null);

  name = signal('');

  isLoading = signal(false);

  isUpdating = signal(false);

  errorMessage = signal('');

  routeLinks = RouteLinks;

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(params => {
      const id = params.get('id');

      if(!id){
        this.errorMessage.set(
          'Id bilgisi alınırken bir hata oluştu.'
        );

        this.toastService.showErrorMessage(
          this.errorMessage()
        );

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

          this.name.set(
            response.data.name
          );

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

  updateFeature(): void {
    const feature = this.feature();

    if(!feature){
      return;
    }

    const featureName = this.name().trim();

    if(!featureName){
      this.toastService.showErrorMessage(
        'Araç özelliği adı boş bırakılamaz.'
      );

      return;
    }

    if(featureName === feature.name){
      this.toastService.showErrorMessage(
        'Araç özelliği bilgisinde herhangi bir değişiklik yapılmadı.'
      );

      return;
    }

    this.isUpdating.set(true);

    const request: UpdateFeatureRequest = {
      name: featureName
    };

    this.featureService
      .updateFeature(
        this.featureId(),
        request
      )
      .subscribe({
        next: () => {
          this.feature.update(currentFeature => {
            if(!currentFeature){
              return currentFeature;
            }

            return {
              ...currentFeature,
              name: featureName
            };
          });

          this.name.set(featureName);

          this.toastService.showSuccessMessage(
            'Araç özelliği başarıyla güncellendi.'
          );

          this.isUpdating.set(false);
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse = error.error as ApiResponse<unknown>;

          const message =
            apiResponse?.errors?.[0]?.message ??
            'Araç özelliği güncellenirken bir hata oluştu.';

          this.toastService.showErrorMessage(
            message
          );

          this.isUpdating.set(false);
        }
      });
  }
}