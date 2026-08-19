import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ToastService {

  message = signal('');
  type = signal<'success' | 'error'>('success');
  isOpenToast = signal(false);

  private closeTimer?: ReturnType<typeof setTimeout>;

  showSuccessMessage(message: string): void {
    this.openToast(message, 'success');
  }

  showErrorMessage(message: string): void {
    this.openToast(message, 'error');
  }

  private openToast(message: string, type: 'success' | 'error'): void {

    if (this.closeTimer) {
      clearTimeout(this.closeTimer);
    }

    this.message.set(message);
    this.type.set(type);
    this.isOpenToast.set(true);

    this.closeTimer = setTimeout(() => {
      this.isOpenToast.set(false);
      this.closeTimer = undefined;
    }, 3000);
  }
}