import { Component, OnInit, signal } from '@angular/core';
import { LocationService } from '../../../core/services/location/location-service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ToastService } from '../../../shared/services/toast-service';
import { GetLocationByIdResponse } from '../../../core/services/location/models/get-location-by-id-response';
import { GetLocationByIdRequest } from '../../../core/services/location/models/get-location-by-id-request';
import { UpdateLocationRequest } from '../../../core/services/location/models/update-location-request';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiResponse } from '../../../core/models/api/api-response';
import { FormsModule } from '@angular/forms';
import { RouteLinks } from '../../../core/constants/route-paths';

@Component({
  selector: 'app-location-edit',
  imports: [FormsModule, RouterLink],
  templateUrl: './location-edit.html',
  styleUrl: './location-edit.scss',
})
export class LocationEdit implements OnInit {

  constructor(
    private readonly locationService: LocationService,
    private readonly activatedRoute: ActivatedRoute,
    private readonly toastService: ToastService
  ){}

  locationId = signal('');

  location = signal<GetLocationByIdResponse | null>(null);

  name = signal('');

  isLoading = signal(false);

  isUpdating = signal(false);

  errorMessage = signal('');

  routeLinks = RouteLinks;

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(p => {
      const id = p.get('id');

      if(!id){
        this.errorMessage.set('Id bilgisi alınırken bir hata oluştu.');
        this.toastService.showErrorMessage(this.errorMessage());
        return;
      }

      this.locationId.set(id);
      this.getLocationById(this.locationId());
    });
  }

  getLocationById(id: string): void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    const request: GetLocationByIdRequest = {
      id: id
    }

    this.locationService
      .getLocationById(request)
      .subscribe({
        next: response => {
          if(!response.data || !response.isSuccess){
            this.errorMessage.set('Şube bilgisi alınırken bir hata oluştu.');
            this.toastService.showErrorMessage(this.errorMessage());
            this.isLoading.set(false);
            return;
          }

          this.location.set(response.data);
          this.name.set(response.data.name);

          this.isLoading.set(false);
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse = error.error as ApiResponse<unknown>;
          this.errorMessage.set(apiResponse?.errors?.[0]?.message ?? 'Şube bilgisi alınırken bir hata oluştu.');
          this.toastService.showErrorMessage(this.errorMessage());

          this.isLoading.set(false);
        }
      });
  }

  updateLocation(): void {
    const location = this.location();

    if(!location){
      return;
    }

    const locationName = this.name().trim();

    if(!locationName){
      this.toastService.showErrorMessage('Şube adı boş bırakılamaz.');
      return;
    }

    if(locationName === location.name){
      this.toastService.showErrorMessage('Şube bilgisinde herhangi bir değişiklik yapılmadı.');
      return;
    }

    this.isUpdating.set(true);

    const request: UpdateLocationRequest = {
      name: locationName
    }

    this.locationService
      .updateLocation(this.locationId(), request)
      .subscribe({
        next: () => {
          this.location.update(currentLocation => {
            if(!currentLocation){
              return currentLocation;
            }

            return {
              ...currentLocation,
              name: locationName
            };
          });

          this.name.set(locationName);
          this.toastService.showSuccessMessage('Şube bilgisi başarıyla güncellendi.');

          this.isUpdating.set(false);
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse = error.error as ApiResponse<unknown>;
          const message = apiResponse?.errors?.[0]?.message ?? 'Şube bilgisi güncellenirken bir hata oluştu.';

          this.toastService.showErrorMessage(message);

          this.isUpdating.set(false);
        }
      });
  }
}