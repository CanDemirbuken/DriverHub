import { Component, OnInit, signal } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { RouterLink } from '@angular/router';

import { FeatureService } from '../../../core/services/feature/feature-service';
import { GetFeaturesResponse } from '../../../core/services/feature/models/get-features-response';
import { ToastService } from '../../../shared/services/toast-service';
import { ApiResponse } from '../../../core/models/api/api-response';
import { RouteLinks } from '../../../core/constants/route-paths';

@Component({
  selector: 'app-feature-list',
  imports: [RouterLink],
  templateUrl: './feature-list.html',
  styleUrl: './feature-list.scss',
})
export class FeatureList implements OnInit {

  constructor(
    private readonly featureService: FeatureService,
    private readonly toastService: ToastService
  ){}

  routeLinks = RouteLinks;

  features = signal<GetFeaturesResponse[]>([]);

  isLoading = signal(false);
  errorMessage = signal('');

  isRemoving = signal(false);
  selectedFeatureForRemove = signal<GetFeaturesResponse | null>(null);

  ngOnInit(): void {
    this.getFeatures();
  }

  getFeatures(): void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    this.featureService
      .getFeatures()
      .subscribe({
        next: response => {
          if(!response.data || !response.isSuccess){
            this.errorMessage.set('Araç özellikleri getirilirken bir hata oluştu.');
            this.toastService.showErrorMessage(this.errorMessage());
            this.isLoading.set(false);
            return;
          }

          this.features.set(response.data);
          this.isLoading.set(false);
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse = error.error as ApiResponse<unknown>;

          this.errorMessage.set(
            apiResponse?.errors?.[0]?.message ??
            'Araç özellikleri getirilirken bir hata oluştu.'
          );

          this.toastService.showErrorMessage(this.errorMessage());

          this.isLoading.set(false);
        }
      });
  }

  removeFeature(id: string): void {
    this.isRemoving.set(true);

    this.featureService
      .removeFeature(id)
      .subscribe({
        next: () => {
          this.features.update(current =>
            current.filter(feature => feature.id !== id)
          );

          this.toastService.showSuccessMessage(
            'Araç özelliği başarıyla silindi.'
          );

          this.isRemoving.set(false);
          this.selectedFeatureForRemove.set(null);
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse = error.error as ApiResponse<unknown>;

          const message =
            apiResponse?.errors?.[0]?.message ??
            'Araç özelliği silinirken bir hata oluştu.';

          this.toastService.showErrorMessage(message);

          this.isRemoving.set(false);
          this.selectedFeatureForRemove.set(null);
        }
      });
  }

  openRemoveConfirmation(feature: GetFeaturesResponse): void {
    this.selectedFeatureForRemove.set(feature);
  }

  closeRemoveConfirmation(): void {
    if(this.isRemoving()){
      return;
    }

    this.selectedFeatureForRemove.set(null);
  }

  confirmRemoveFeature(): void {
    const feature = this.selectedFeatureForRemove();

    if(!feature){
      return;
    }

    this.removeFeature(feature.id);
  }
}