import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { RouteLinks } from '../../core/constants/route-paths';

@Component({
  selector: 'app-reset-password',
  imports: [FormsModule, RouterLink],
  templateUrl: './reset-password.html',
  styleUrl: './reset-password.scss',
})
export class ResetPassword implements OnInit {
  email = '';
  resetToken = '';
  password = '';
  passwordConfirm = '';

  readonly loginLink = RouteLinks.Admin.Login;

  constructor(
    private readonly router: Router,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.email = this.route.snapshot.queryParamMap.get('email') ?? '';
    this.resetToken = this.route.snapshot.queryParamMap.get('token') ?? '';
  }

  resetPassword(): void {
    if (this.password === this.passwordConfirm) {
      // accountService.resetPassword(this.email, this.resetToken, this.password);
      this.router.navigateByUrl(this.loginLink);
    }
  }
}