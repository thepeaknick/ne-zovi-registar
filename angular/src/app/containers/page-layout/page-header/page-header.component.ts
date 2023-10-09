import { Component, Input } from '@angular/core';

import { ClassToggleService, HeaderComponent } from '@coreui/angular';
import { NavigationEnd, Router } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';

@Component({
  selector: 'app-page-header',
  templateUrl: './page-header.component.html',
  styleUrls: ['./page-header.component.scss'],
})
export class PageHeaderComponent extends HeaderComponent {
  public newMessages = new Array(4);
  public newTasks = new Array(5);
  public newNotifications = new Array(5);

  isLogin: boolean = false;
  isSearch: boolean = false;

  constructor(
    private classToggler: ClassToggleService,
    private router: Router,
    private translocoService: TranslocoService
  ) {
    super();
  }

  changeLanguage(lang: string) {
    this.translocoService.setActiveLang(lang);
  }

  ngOnInit() {
    this.router.events.subscribe((evt) => {
      if (!(evt instanceof NavigationEnd)) {
        if (this.router.url === '/login') {
          this.isLogin = true;
          this.isSearch = false;
        }
        if (this.router.url === '/search') {
          this.isSearch = true;
          this.isLogin = false;
        }
        return;
      }
    });
  }

  goToSignIn($myParam: string = ''): void {
    const navigationDetails: string[] = ['/login'];
    if ($myParam.length) {
      navigationDetails.push($myParam);
    }
    this.router.navigate(navigationDetails);
  }

  goBack(): void {
    const navigationDetails: string[] = ['/'];
    this.router.navigate(navigationDetails);
  }
}
