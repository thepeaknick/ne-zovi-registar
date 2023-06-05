import { Component, OnInit } from '@angular/core';
import { RegUserService } from 'src/app/domain/services/reguser.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';
import { first } from 'rxjs';
import { CurrentUser } from 'src/app/domain/model/current-user';
import { RoleType } from 'src/app/domain/model/schemas';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})

export class LoginComponent implements OnInit {

  loginForm!: FormGroup;
  loading = false;
  submitted = false;
  error = '';

  username = '';
  password = '';

  constructor(
    private regUserService: RegUserService, 
    private router: Router,
    private route: ActivatedRoute,
    private authenticationService: AuthenticationService,
    private formBuilder: FormBuilder) {

      // if user is already logged in navigate to home page
      //if(AuthenticationService.Token !== null)
      //  this.router.navigate(['/']);

  }

  ngOnInit() {

    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  // convenience getter for easy access to form fields
  get fields() { return this.loginForm.controls; }

  goDashboardHome(): void {

    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
        return;
    }

    this.loading = true;
    this.regUserService
      .loginRegUser({
          username: this.fields["username"].value, 
          password: this.fields["password"].value 
      })
      .pipe(first())
      .subscribe({
          next: () => {
            // get return url from route parameters or default to '/'
            let returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

            if(returnUrl === '/') {

              // TODO: get current user from logged in data (extend token)
              let user: CurrentUser = { roles: [ RoleType.Admin ] };

              if(user.roles?.includes(RoleType.Admin)) {
                returnUrl = '/admin';
              } else if(user.roles?.includes(RoleType.Trgovac)) {
                returnUrl = '/registry/merchants';
              } if(user.roles?.includes(RoleType.Obveznik)) {
                returnUrl = '/registry/regusers';
              }
            }

            // navigate
            this.router.navigate([returnUrl]);
          },
          error: error => {
              this.error = error;
              this.loading = false;
          }
      });
  }
}
