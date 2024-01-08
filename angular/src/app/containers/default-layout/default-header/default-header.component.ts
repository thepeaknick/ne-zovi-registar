import { Component, Input } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { ClassToggleService, HeaderComponent } from '@coreui/angular';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';
import { RegUserService } from 'src/app/domain/services/reguser.service';
import { cilMenu } from '@coreui/icons';

import { TranslocoService } from '@ngneat/transloco';

@Component({
  selector: 'app-default-header',
  templateUrl: './default-header.component.html',
  styleUrls: ['./default-header.component.scss'],
})
export class DefaultHeaderComponent extends HeaderComponent {
  @Input() sidebarId: string = 'sidebar';
  icons = { cilMenu };
  isCyr: boolean = true;

  public newMessages = new Array(4);
  public newTasks = new Array(5);
  public newNotifications = new Array(5);

  @Input() currentUsername: string;

  constructor(
    public authenticationService: AuthenticationService,
    private translocoService: TranslocoService
  ) {
    super();
    this.currentUsername = AuthenticationService.CurrentUserName;
  }

  changeLanguage(lang: string) {
    this.translocoService.setActiveLang(lang);
    this.isCyr = lang == 'cir' ? true : false;
  }
}
