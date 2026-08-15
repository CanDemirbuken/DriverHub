import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { RouteLinks } from '../../../../core/constants/route-paths';

@Component({
  selector: 'app-sidebar',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.scss',
})
export class Sidebar {
  protected readonly routeLinks = RouteLinks;
}
