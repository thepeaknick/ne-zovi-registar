import { Component } from '@angular/core';

import { navItems } from './_nav';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.scss'],
})
export class DefaultLayoutComponent {
  public navItems = navItems;

  public perfectScrollbarConfig = {
    suppressScrollX: true,
  };

  constructor() {}

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
