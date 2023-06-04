import { Component } from '@angular/core';

import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  token: string | undefined;

  constructor(private router: Router) {
    this.token = undefined;
  }

  goDashboardHome(): void {
    const navigationDetails: string[] = ['/admin'];
    this.router.navigate(navigationDetails);

    //TODO: Swiftch case ADMIN, MERCHANT, OPERATOR
  }
}
