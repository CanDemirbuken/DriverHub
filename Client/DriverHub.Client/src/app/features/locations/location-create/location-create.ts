import { Component, signal } from '@angular/core';
import { LocationService } from '../../../core/services/location/location-service';
import { Router, RouterLink } from '@angular/router';
import { CreateLocationRequest } from '../../../core/services/location/models/create-location-request';
import { ToastService } from '../../../shared/services/toast-service';
import { RouteLinks } from '../../../core/constants/route-paths';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiResponse } from '../../../core/models/api/api-response';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-location-create',
  imports: [FormsModule, RouterLink],
  templateUrl: './location-create.html',
  styleUrl: './location-create.scss',
})
export class LocationCreate {
  constructor(
    private readonly locationService: LocationService,
    private readonly router: Router,
    private readonly toastService: ToastService
  ){}

  name = signal('');

  errorMessage = signal('');
  isAdding = signal(false);

  routeLinks = RouteLinks;

  createLocation(): void{
    const locationName = this.name().trim();
    if(!locationName){
      this.errorMessage.set('Şube adı boş bırakılamaz.');
      this.toastService.showErrorMessage(this.errorMessage());
      return;
    }

    this.isAdding.set(true);
    this.errorMessage.set('');

    const request: CreateLocationRequest = {
      name: locationName
    }

    this.locationService
      .createLocation(request)
      .subscribe({
        next: response => {
          if(!response.data || !response.isSuccess){
            this.errorMessage.set('Şube eklenirken bir hata oluştu.');
            this.toastService.showErrorMessage(this.errorMessage());
            this.isAdding.set(false);
            return;
          }

          this.isAdding.set(false);
          this.toastService.showSuccessMessage('Şube başarıyla eklendi.');

          const url = this.routeLinks.Admin.LocationDetail(response.data.id);
          this.router.navigateByUrl(url);
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse = error.error as ApiResponse<unknown>;
          this.errorMessage.set(apiResponse?.errors[0]?.message ?? 'Şube eklenirken bir hata oluştu.');
          this.toastService.showErrorMessage(this.errorMessage());
          this.isAdding.set(false);
        }
      });
  }
}
