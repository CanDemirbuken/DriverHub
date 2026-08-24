import { Component, OnInit, signal } from '@angular/core';
import { LocationService } from '../../../core/services/location/location-service';
import { GetLocationsResponse } from '../../../core/services/location/models/get-locations-response';
import { ToastService } from '../../../shared/services/toast-service';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiResponse } from '../../../core/models/api/api-response';
import { RouterLink } from '@angular/router';
import { RouteLinks } from '../../../core/constants/route-paths';

@Component({
  selector: 'app-location-list',
  imports: [RouterLink],
  templateUrl: './location-list.html',
  styleUrl: './location-list.scss',
})
export class LocationList implements OnInit {

  constructor(
    private readonly locationService: LocationService,
    private readonly toastService: ToastService
  ){}

  routeLinks = RouteLinks;

  locations = signal<GetLocationsResponse[]>([]);

  isLoading = signal(false);
  errorMessage = signal('');

  isRemoving = signal(false);
  selectedLocationForRemove = signal<GetLocationsResponse | null>(null);

  ngOnInit(): void {
    this.getLocations();
  }

  getLocations(): void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    this.locationService
      .getLocations()
      .subscribe({
        next: response => {
          if(!response.data || !response.isSuccess){
            this.errorMessage.set('Şubeler getirilirken bir hata oluştu.');
            this.toastService.showErrorMessage(this.errorMessage());
            this.isLoading.set(false);
            return;
          }

          this.locations.set(response.data);
          this.isLoading.set(false);
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse = error.error as ApiResponse<unknown>;

          this.errorMessage.set(
            apiResponse?.errors?.[0]?.message ??
            'Şubeler getirilirken bir hata oluştu.'
          );

          this.toastService.showErrorMessage(this.errorMessage());

          this.isLoading.set(false);
        }
      });
  }

  removeLocation(id: string): void {
    this.isRemoving.set(true);

    this.locationService
      .removeLocation(id)
      .subscribe({
        next: () => {
          this.locations.update(current =>
            current.filter(location => location.id !== id)
          );

          this.toastService.showSuccessMessage(
            'Şube başarıyla silindi.'
          );

          this.isRemoving.set(false);
          this.selectedLocationForRemove.set(null);
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse = error.error as ApiResponse<unknown>;

          const message =
            apiResponse?.errors?.[0]?.message ??
            'Şube silinirken bir hata oluştu.';

          this.toastService.showErrorMessage(message);

          this.isRemoving.set(false);
          this.selectedLocationForRemove.set(null);
        }
      });
  }

  openRemoveConfirmation(location: GetLocationsResponse): void {
    this.selectedLocationForRemove.set(location);
  }

  closeRemoveConfirmation(): void {
    if(this.isRemoving()){
      return;
    }

    this.selectedLocationForRemove.set(null);
  }

  confirmRemoveLocation(): void {
    const location = this.selectedLocationForRemove();

    if(!location){
      return;
    }

    this.removeLocation(location.id);
  }
}