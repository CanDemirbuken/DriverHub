import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Sidebar } from "./components/sidebar/sidebar";
import { Topbar } from "./components/topbar/topbar";
import { Footer } from "./components/footer/footer";

@Component({
  selector: 'app-admin-layout',
  imports: [RouterOutlet, Sidebar, Topbar, Footer],
  templateUrl: './admin-layout.html',
  styleUrl: './admin-layout.scss',
})
export class AdminLayout {}
