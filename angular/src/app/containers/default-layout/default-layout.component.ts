import { Component, Input } from '@angular/core';

import { navItems, ICustomNavData } from './_nav';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';
import {
  cilExitToApp,
  cilUser,
  cilSpreadsheet,
  cilGroup,
  cilBriefcase,
  cilCart,
  cilSettings,
  cilPuzzle,
  cilEnvelopeClosed,
} from '@coreui/icons';
import { RoleType } from 'src/app/domain/model/schemas';
import { IconSetService } from '@coreui/icons-angular';

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

  constructor(
    private route: ActivatedRoute,
    public iconSet: IconSetService,
    public authenticationService: AuthenticationService,
    private router: Router
  ) {
    this.id = 0;
    this.role = 0;

    // filter navitems by user role
    let currentUser = AuthenticationService.CurrentUser;
    let role = RoleType.Potrosac;
    if (currentUser) role = currentUser.role;

    this.navItems = this.filterNavItems(navItems, [RoleType[role]]) ?? [];

    iconSet.icons = {
      cilExitToApp,
      cilUser,
      cilSpreadsheet,
      cilGroup,
      cilBriefcase,
      cilCart,
      cilSettings,
      cilPuzzle,
      cilEnvelopeClosed,
    };
  }

  filterNavItems(
    navItems: ICustomNavData[] | undefined,
    roles: string[]
  ): ICustomNavData[] | undefined {
    let newNavItems: ICustomNavData[] | undefined = undefined;

    if (navItems) {
      newNavItems = [];
      navItems.forEach((navItem) => {
        if (navItem.roles === undefined && navItem.children === undefined)
          newNavItems?.push(navItem);
        else {
          let found = false;
          roles.forEach((r) => {
            found =
              found ||
              navItem.roles === undefined ||
              navItem.roles.indexOf(r) >= 0;
          });

          if (found) {
            let newNavItem: ICustomNavData = {
              ...navItem,
              children: this.filterNavItems(navItem.children, roles),
            };

            newNavItems?.push(newNavItem);
          }
        }
      });
    }

    return newNavItems;
  }

  logout() {
    this.authenticationService.logout().subscribe(() => {
      console.log('Logged out!');
      this.router.navigate(['/']);
    });
  }

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      console.log(params);

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
