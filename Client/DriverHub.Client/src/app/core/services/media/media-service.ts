import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';
import { ApiEndpoints } from '../../constants/api-endpoints';
import { ApiResponse } from '../../models/api/api-response';
import { UploadMediaResponse } from './models/upload-media-response';

@Injectable({
  providedIn: 'root',
})
export class MediaService {
  constructor(private readonly http: HttpClient) {}

  upload(file: File): Observable<ApiResponse<UploadMediaResponse>> {
    const url = `${environment.apiUrl}${ApiEndpoints.Media.Upload}`;

    const formData = new FormData();
    formData.append(
      'File',
      file,
      file.name
    );

    return this.http.post<ApiResponse<UploadMediaResponse>>(url, formData);
  }
}