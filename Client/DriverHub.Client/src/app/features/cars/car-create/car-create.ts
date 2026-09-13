import { Component, OnInit, signal } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { CarService } from '../../../core/services/car/car-service';
import { BrandService } from '../../../core/services/brand/brand-service';
import { CategoryService } from '../../../core/services/category/category-service';
import { LocationService } from '../../../core/services/location/location-service';
import { MediaService } from '../../../core/services/media/media-service';
import { CreateCarRequest } from '../../../core/services/car/models/create-car-request';
import { GetBrandsResponse } from '../../../core/services/brand/models/get-brands-response';
import { GetCategoriesResponse } from '../../../core/services/category/models/get-categories-response';
import { GetLocationsResponse } from '../../../core/services/location/models/get-locations-response';
import { ApiResponse } from '../../../core/models/api/api-response';
import { RouteLinks } from '../../../core/constants/route-paths';
import { ImageUrlHelper } from '../../../shared/helpers/image-url-helper';
import { ToastService } from '../../../shared/services/toast-service';

@Component({
  selector: 'app-car-create',
  imports: [FormsModule, RouterLink],
  templateUrl: './car-create.html',
  styleUrl: './car-create.scss',
})
export class CarCreate implements OnInit {
  readonly routeLinks = RouteLinks;
  readonly imageUrlHelper = ImageUrlHelper;

  brands = signal<GetBrandsResponse[]>([]);
  categories = signal<GetCategoriesResponse[]>([]);
  locations = signal<GetLocationsResponse[]>([]);
  isLoading = signal(true);
  isCreating = signal(false);
  isCoverImageUploading = signal(false);
  isBigImageUploading = signal(false);
  selectedCoverFileName = signal('');
  selectedBigFileName = signal('');
  errorMessage = signal('');

  brandId = '';
  categoryId = '';
  currentLocationId = '';
  model = '';
  modelYear = new Date().getFullYear();
  plate = '';
  vin = '';
  km = 0;
  transmission = '';
  seat = 5;
  luggage = 0;
  fuel = '';
  color = '';
  coverImageUrl = '';
  bigImageUrl = '';

  constructor(
    private readonly carService: CarService,
    private readonly brandService: BrandService,
    private readonly categoryService: CategoryService,
    private readonly locationService: LocationService,
    private readonly mediaService: MediaService,
    private readonly router: Router,
    private readonly toastService: ToastService
  ) {}

  ngOnInit(): void {
    let pending = 3;
    const done = () => { pending -= 1; if (pending === 0) this.isLoading.set(false); };
    this.brandService.getBrands().subscribe({ next: r => { if (r.isSuccess && r.data) this.brands.set(r.data); else this.errorMessage.set('Marka bilgisi alınamadı.'); done(); }, error: e => { this.setRequestError(e, 'Marka listesi alınırken bir hata oluştu.'); done(); } });
    this.categoryService.getCategories().subscribe({ next: r => { if (r.isSuccess && r.data) this.categories.set(r.data); else this.errorMessage.set('Kategori bilgisi alınamadı.'); done(); }, error: e => { this.setRequestError(e, 'Kategori listesi alınırken bir hata oluştu.'); done(); } });
    this.locationService.getLocations().subscribe({ next: r => { if (r.isSuccess && r.data) this.locations.set(r.data); else this.errorMessage.set('Lokasyon bilgisi alınamadı.'); done(); }, error: e => { this.setRequestError(e, 'Lokasyon listesi alınırken bir hata oluştu.'); done(); } });
  }

  createCar(): void {
    this.errorMessage.set('');
    const validation = this.validate();
    if (validation) { this.showError(validation); return; }
    this.isCreating.set(true);
    const request: CreateCarRequest = {
      brandId: this.brandId, categoryId: this.categoryId, currentLocationId: this.currentLocationId,
      model: this.model.trim(), modelYear: this.modelYear, plate: this.plate.trim(), vin: this.vin.trim().toUpperCase(),
      km: this.km, transmission: this.transmission, seat: this.seat, luggage: this.luggage,
      fuel: this.fuel, color: this.color.trim(), coverImageUrl: this.coverImageUrl, bigImageUrl: this.bigImageUrl
    };
    this.carService.createCar(request).subscribe({
      next: response => {
        this.isCreating.set(false);
        if (!response.isSuccess || !response.data) { this.showError('Araç eklenirken bir hata oluştu.'); return; }
        this.toastService.showSuccessMessage('Araç başarıyla eklendi.');
        this.router.navigateByUrl(this.routeLinks.Admin.CarDetail(response.data.id));
      },
      error: (error: HttpErrorResponse) => { this.isCreating.set(false); this.setRequestError(error, 'Araç eklenirken bir hata oluştu.'); }
    });
  }

  onCoverImageSelected(event: Event): void { this.uploadImage(event, true); }
  onBigImageSelected(event: Event): void { this.uploadImage(event, false); }

  private uploadImage(event: Event, isCover: boolean): void {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (!file) return;
    (isCover ? this.selectedCoverFileName : this.selectedBigFileName).set(file.name);
    (isCover ? this.isCoverImageUploading : this.isBigImageUploading).set(true);
    this.mediaService.upload(file).subscribe({
      next: response => {
        if (!response.isSuccess || !response.data) this.showError(isCover ? 'Kapak görseli yüklenemedi.' : 'Büyük görsel yüklenemedi.');
        else if (isCover) this.coverImageUrl = response.data.path;
        else this.bigImageUrl = response.data.path;
        (isCover ? this.isCoverImageUploading : this.isBigImageUploading).set(false);
      },
      error: (error: HttpErrorResponse) => { (isCover ? this.isCoverImageUploading : this.isBigImageUploading).set(false); this.setRequestError(error, isCover ? 'Kapak görseli yüklenirken bir hata oluştu.' : 'Büyük görsel yüklenirken bir hata oluştu.'); }
    });
  }

  private validate(): string {
    if (!this.brandId || !this.categoryId || !this.currentLocationId) return 'Marka, kategori ve lokasyon seçimi zorunludur.';
    if (!this.model.trim()) return 'Model alanı zorunludur.';
    if (this.modelYear < 1900 || this.modelYear > 2100) return 'Model yılı 1900 ile 2100 arasında olmalıdır.';
    if (!this.plate.trim()) return 'Plaka bilgisi zorunludur.';
    if (this.vin.trim().length !== 17) return 'VIN 17 karakter olmalıdır.';
    if (this.km < 0 || this.luggage < 0) return 'Kilometre ve bagaj kapasitesi 0 veya daha büyük olmalıdır.';
    if (!this.transmission || !this.fuel || !this.color.trim()) return 'Teknik alanların tamamı zorunludur.';
    if (this.seat < 1 || this.seat > 9) return 'Koltuk sayısı 1 ile 9 arasında olmalıdır.';
    if (!this.coverImageUrl || !this.bigImageUrl) return 'Kapak ve büyük görsel yüklenmelidir.';
    return '';
  }

  private setRequestError(error: HttpErrorResponse, fallback: string): void { this.showError((error.error as ApiResponse<unknown>)?.errors?.[0]?.message ?? fallback); }
  private showError(message: string): void { this.errorMessage.set(message); this.toastService.showErrorMessage(message); }
}
