import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})

export class AuthState{
    isAuthenticated = false;

    login(): void{
        this.isAuthenticated = true;
    }

    logout(): void{
        this.isAuthenticated = false;
    }
}
