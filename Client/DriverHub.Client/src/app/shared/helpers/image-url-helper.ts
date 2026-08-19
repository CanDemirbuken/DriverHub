import { environment } from "../../../environments/environment";

export class ImageUrlHelper{
    static getImageUrl(path: string | null | undefined): string {
      if (!path) {
        return '';
      }

      if (
        path.startsWith('http://') ||
        path.startsWith('https://')
      ) {
        return path;
      }

      return `${environment.apiUrl}${path}`;
    }
}