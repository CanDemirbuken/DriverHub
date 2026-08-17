import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { RouteLinks } from '../../core/constants/route-paths';
import { AuthService } from '../../core/services/auth/auth-service';
import { LoginRequest } from '../../core/services/auth/models/login-request';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiResponse } from '../../core/models/api/api-response';
import { TokenStore } from '../../core/auth/token-store';

@Component({
  selector: 'app-admin-login',
  imports: [FormsModule, RouterLink],
  templateUrl: './admin-login.html',
  styleUrl: './admin-login.scss'
})
export class AdminLogin {
  email = '';
  password = '';
  errorMessage = '';

  readonly forgotPasswordLink = RouteLinks.Admin.ForgotPassword;

  constructor(
    private readonly authService: AuthService,
    private readonly tokenStore: TokenStore,
    private readonly router: Router
  ) {}

  login(): void {
    this.errorMessage = '';

    const request: LoginRequest = {
      email: this.email,
      password: this.password
    };

    this.authService.login(request).subscribe({
      next: response => {
        if (!response.data) {
          this.errorMessage = 'Giriş bilgileri alınamadı.';
          return;
        }
      
        this.tokenStore.setAccessToken(response.data.accessToken);
        this.router.navigateByUrl(RouteLinks.Admin.Dashboard);
      },
      error: (error: HttpErrorResponse) => {
        const apiResponse = error.error as ApiResponse<unknown>;

        this.errorMessage =
          apiResponse.errors[0]?.message ??
          'Giriş işlemi sırasında bir hata oluştu.';
      }
    });
  }
}