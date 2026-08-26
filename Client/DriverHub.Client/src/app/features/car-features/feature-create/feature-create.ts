import { Component, signal } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { FeatureService } from '../../../core/services/feature/feature-service';
import { CreateFeatureRequest } from '../../../core/services/feature/models/create-feature-request';
import { ToastService } from '../../../shared/services/toast-service';
import { RouteLinks } from '../../../core/constants/route-paths';
import { ApiResponse } from '../../../core/models/api/api-response';

@Component({
  selector: 'app-feature-create',
  imports: [FormsModule, RouterLink],
  templateUrl: './feature-create.html',
  styleUrl: './feature-create.scss',
})
export class FeatureCreate {

  constructor(
    private readonly featureService: FeatureService,
    private readonly router: Router,
    private readonly toastService: ToastService
  ){}

  name = signal('');

  errorMessage = signal('');
  isAdding = signal(false);

  routeLinks = RouteLinks;

  createFeature(): void {
    const featureName = this.name().trim();

    if(!featureName){
      this.errorMessage.set('Araç özelliği adı boş bırakılamaz.');
      this.toastService.showErrorMessage(this.errorMessage());
      return;
    }

    this.isAdding.set(true);
    this.errorMessage.set('');

    const request: CreateFeatureRequest = {
      name: featureName
    };

    this.featureService
      .createFeature(request)
      .subscribe({
        next: response => {
          if(!response.data || !response.isSuccess){
            this.errorMessage.set(
              'Araç özelliği eklenirken bir hata oluştu.'
            );

            this.toastService.showErrorMessage(
              this.errorMessage()
            );

            this.isAdding.set(false);
            return;
          }

          this.isAdding.set(false);

          this.toastService.showSuccessMessage(
            'Araç özelliği başarıyla eklendi.'
          );

          const url = this.routeLinks.Admin.FeatureDetail(
            response.data.id
          );

          this.router.navigateByUrl(url);
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse = error.error as ApiResponse<unknown>;

          this.errorMessage.set(
            apiResponse?.errors?.[0]?.message ??
            'Araç özelliği eklenirken bir hata oluştu.'
          );

          this.toastService.showErrorMessage(
            this.errorMessage()
          );

          this.isAdding.set(false);
        }
      });
  }
}