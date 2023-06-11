import { Component, Input } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { ClassToggleService, HeaderComponent } from '@coreui/angular';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';
import { RegUserService } from 'src/app/domain/services/reguser.service';

@Component({
  selector: 'app-default-header',
  templateUrl: './default-header.component.html',
  styleUrls: ['./default-header.component.scss']
})
export class DefaultHeaderComponent extends HeaderComponent {

  @Input() sidebarId: string = "sidebar";

  public newMessages = new Array(4)
  public newTasks = new Array(5)
  public newNotifications = new Array(5)

  constructor(
    private classToggler: ClassToggleService,
    private router: Router,
    private route: ActivatedRoute,
    public authenticationService: AuthenticationService) {
    super();
  }

  logout() {
    this.authenticationService
      .logout()
      .subscribe(() => {
        console.log('Logged out!');
        this.router.navigate(['/']);
    });
  }
}
