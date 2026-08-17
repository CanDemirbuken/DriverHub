import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthState } from '../../../../core/auth/auth-state';
import { RouteLinks } from '../../../../core/constants/route-paths';
import { SessionService } from '../../../../core/services/session/session-service';
import { AuthUser } from '../../../../core/auth/models/auth-user';

@Component({
  selector: 'app-topbar',
  imports: [],
  templateUrl: './topbar.html',
  styleUrl: './topbar.scss',
})
export class Topbar {
  isUserMenuOpen = false;

  constructor(
    private readonly authState: AuthState,
    private readonly sessionService: SessionService,
    private readonly router: Router
  ) {}

  get user(): AuthUser | null{
    return this.authState.currentUser;
  }

  get primaryRole(): string{
    return this.user?.roles[0] ?? '';
  }

  toggleUserMenu(): void {
    this.isUserMenuOpen = !this.isUserMenuOpen;
  }

  logout(): void {
    this.sessionService.logout().subscribe({
      next: () => {
        this.completeLogout();
      },

      error: () => {
        this.completeLogout();
      }
    });
  }

  private completeLogout(): void{
    this.authState.logout();
    void this.router.navigateByUrl(RouteLinks.Admin.Login);
  }
}