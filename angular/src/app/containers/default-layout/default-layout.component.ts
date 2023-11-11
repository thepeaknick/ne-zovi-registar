import { Component, Input, OnInit } from '@angular/core';

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
  cilChevronRight,
  cilChevronBottom
} from '@coreui/icons';
import { RoleType } from 'src/app/domain/model/schemas';
import { IconSetService } from '@coreui/icons-angular';

import { TranslocoService } from '@ngneat/transloco';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.scss'],
})
export class DefaultLayoutComponent implements OnInit{

  showSidebarChildren = false;

  handleClick() {
    this.showSidebarChildren = !this.showSidebarChildren
    // Add your click logic here, e.g., navigate to a URL
    console.log('Clicked on');
  }

  icons = { cilExitToApp, cilChevronRight, cilChevronBottom };

  iconMapping: { [key: string]: any } = {
    'pocetna': cilUser,
    'registar': cilSpreadsheet,
    'korisnici': cilGroup,
    'obveznici': cilBriefcase,
    'trgovci': cilCart,
    'podesavanja': cilSettings,
    'pomoc': cilPuzzle,
    'kontakt': cilEnvelopeClosed,
  };

  public navItems = navItems;

  public perfectScrollbarConfig = {
    suppressScrollX: true,
  };

  private id: Number;
  @Input() role: Number;
  @Input() currentUsername: string;

  constructor(
    private route: ActivatedRoute,
    public iconSet: IconSetService,
    public authenticationService: AuthenticationService,
    private router: Router,
    private transloco: TranslocoService
  ) {
    this.id = 0;
    this.role = 0;

    // filter navitems by user role
    let currentUser = AuthenticationService.CurrentUser;
    this.currentUsername = AuthenticationService.CurrentUserName;
    let role = RoleType.Potrosac;
    if (currentUser) role = currentUser.role;

    this.navItems = this.filterNavItems(navItems, [RoleType[role]]) ?? [];

    // this.transloco.langChanges$.subscribe((newLang) => {
    //   this.updateNavItemsWithTranslations(newLang);
    // });

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
      cilChevronRight,
      cilChevronBottom
    };
  }

  

  private updateNavItemsWithTranslations(language: string): void {
    console.log("Lang ", this.transloco.getActiveLang())
    // navItem.name = this.transloco.translate(String(navItem.name))
    // Iterate through the navItems and update the name property with translations
    this.navItems = this.navItems.map((navItem) => {
      console.log("navItem ", navItem)
      navItem.name = this.transloco.translate(navItem.name ?? "", undefined, language);
      console.log("navItem1 ",this.transloco.translate(navItem.name ?? ""))
      return navItem;
    });
    
    // Trigger Transloco's change detection to ensure translations are updated
    // this.transloco.markDirty('your-translation-scope'); // Replace with your actual translation scope
  }

  filterNavItems(
    navItems: ICustomNavData[] | undefined,
    roles: string[]
  ): ICustomNavData[] | undefined {
    let newNavItems: ICustomNavData[] | undefined = undefined;

    if (navItems) {
      newNavItems = [];
      navItems.forEach((navItem) => {
        if (navItem.roles === undefined && navItem.children === undefined) {
          newNavItems?.push(navItem);
        }
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
    this.authenticationService.logout().subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: (error) => {
        console.error(error);
        this.router.navigate(['/']);
      },
    });
  }

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      this.id = params['id'];
      this.role = params['userRole'];
    });

    // this.transloco.events$.subscribe((lang) =>  {
    //   console.log(lang, "LANG")
    //   this.updateNavItemsWithTranslations(lang.payload.langName);
    // });

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
