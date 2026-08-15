import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthState } from '../../../../core/auth/auth-state';
import { RouteLinks } from '../../../../core/constants/route-paths';

@Component({
  selector: 'app-topbar',
  imports: [],
  templateUrl: './topbar.html',
  styleUrl: './topbar.scss',
})
export class Topbar {
  isUserMenuOpen = false;

  user = {
    firstName: 'Yaşarcan',
    lastName: 'Demirbüken',
    role: 'Admin'
  };

  constructor(
    private readonly authState: AuthState,
    private readonly router: Router
  ) {}

  toggleUserMenu(): void {
    this.isUserMenuOpen = !this.isUserMenuOpen;
  }

  logout(): void {
    this.authState.logout();
    this.router.navigateByUrl(RouteLinks.Admin.Login);
  }
}