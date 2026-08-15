import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { RouteLinks } from '../../core/constants/route-paths';

@Component({
  selector: 'app-forgot-password',
  imports: [FormsModule, RouterLink],
  templateUrl: './forgot-password.html',
  styleUrl: './forgot-password.scss',
})
export class ForgotPassword {
  email = '';
  isSent = false;

  readonly loginLink = RouteLinks.Admin.Login;

  sendResetLink(): void {
    // accountService.forgotPassword(this.email);
    console.log('Mail gönderildi.');

    this.isSent = true;
  }
}