import { Component, Input } from '@angular/core';

import { navItems } from './_nav';
import { ActivatedRoute, Router } from '@angular/router';
import { cilExitToApp } from '@coreui/icons';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.scss'],
})
export class DefaultLayoutComponent {

  icons = { cilExitToApp };

  public navItems = navItems;

  public perfectScrollbarConfig = {
    suppressScrollX: true,
  };

  private id: Number;
  @Input() role: Number;

  constructor(private route: ActivatedRoute) {
    this.id = 0;
    this.role = 0;
    // console.log(this.router.getCurrentNavigation().extras.queryParams); // should log out 'bar'
  }

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      console.log(params); // { order: "popular" }

      this.id = params['id'];
      this.role = params['userRole'];
    });
  }

  vars = {
    '--cui-sidebar-bg': '#133F85',
    '--cui-sidebar-toggler-bg': 'rgba(0, 0, 21, 0.0)',
    '--cui-sidebar-brand-bg': 'rgba(0, 0, 21, 0.0)',
    '--cui-sidebar-header-bg': 'rgba(0, 0, 21, 0.0)',
    '--cui-sidebar-nav-group-bg': 'rgba(0, 0, 21, 0.0)',
    '--cui-sidebar-nav-link-active-bg': '#3F6AA3',
    // '--my-another-css-var': 'red',
  };
}
