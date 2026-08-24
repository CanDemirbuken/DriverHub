import { Component, OnInit, signal } from '@angular/core';
import { LocationService } from '../../../core/services/location/location-service';
import { ToastService } from '../../../shared/services/toast-service';
import { GetLocationByIdResponse } from '../../../core/services/location/models/get-location-by-id-response';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { GetLocationByIdRequest } from '../../../core/services/location/models/get-location-by-id-request';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiResponse } from '../../../core/models/api/api-response';
import { RouteLinks } from '../../../core/constants/route-paths';

@Component({
  selector: 'app-location-detail',
  imports: [RouterLink],
  templateUrl: './location-detail.html',
  styleUrl: './location-detail.scss',
})
export class LocationDetail implements OnInit {
  constructor(
    private readonly locationService: LocationService,
    private readonly toastService: ToastService,
    private readonly activatedRoute: ActivatedRoute
  ){}

  routeLinks = RouteLinks;

  locationId = signal('');
  location = signal<GetLocationByIdResponse | null>(null);

  isLoading = signal(false);
  
  errorMessage = signal('');

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(p => {
      const id = p.get('id');
      
      if(!id){
        this.errorMessage.set('Id bilgisi alınamadı.');
        this.toastService.showErrorMessage(this.errorMessage());
        return;
      }

      this.locationId.set(id);
      this.getLocationById(this.locationId());
    })
  }

  getLocationById(id: string): void{
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
          this.isLoading.set(false);
        },

        error: (error: HttpErrorResponse) => {
          const apiResponse = error.error as ApiResponse<unknown>;
          this.errorMessage.set(apiResponse?.errors[0]?.message ?? 'Şube bilgisi alınırken bir hata oluştu.');
          this.toastService.showErrorMessage(this.errorMessage());
          this.isLoading.set(false);
        }
      });
  }
}
