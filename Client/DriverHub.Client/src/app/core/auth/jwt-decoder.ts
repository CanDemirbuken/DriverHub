import { Injectable } from '@angular/core';

import { JwtPayload } from './models/jwt-payload';

@Injectable({
  providedIn: 'root'
})
export class JwtDecoder {

  decode(token: string): JwtPayload | null {
    try {
      const parts = token.split('.');

      if (parts.length !== 3) {
        return null;
      }

      const payload = parts[1]
        .replace(/-/g, '+')
        .replace(/_/g, '/');

      const normalizedPayload =
        payload.padEnd(
          payload.length + (4 - payload.length % 4) % 4,
          '='
        );

      const decodedPayload =
        decodeURIComponent(
          atob(normalizedPayload)
            .split('')
            .map(character =>
              '%' +
              character
                .charCodeAt(0)
                .toString(16)
                .padStart(2, '0')
            )
            .join('')
        );

      return JSON.parse(decodedPayload) as JwtPayload;
    }
    catch {
      return null;
    }
  }
}