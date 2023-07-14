import { Component, Input, OnInit } from '@angular/core';
import { RegUserService } from 'src/app/domain/services/reguser.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';
import { first } from 'rxjs';
import { RegUserDetailsDto, RoleType } from 'src/app/domain/model/schemas';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  @Input() showLoginErrorMessage: Boolean = false;

  loginForm!: FormGroup;
  forgotPasswordForm!: FormGroup;
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
    private formBuilder: FormBuilder
  ) {}

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });

    this.forgotPasswordForm = this.formBuilder.group({
      inputEmail: ['', Validators.email],
    });
  }

  // convenience getter for easy access to form fields
  get fields() {
    return this.loginForm.controls;
  }

  goSignUp(): void {
    this.router.navigate(['/signup']);
  }

  goDashboardHome(): void {
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this.authenticationService
      .login(this.fields['username'].value, this.fields['password'].value)
      .subscribe({
        next: () => {
          this.showLoginErrorMessage = false;
          // get return url from route parameters or default to '/'
          let returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

          if (returnUrl === '/') {
            let user: RegUserDetailsDto | null =
              AuthenticationService.CurrentUser;
            console.log(JSON.stringify(user));

            if (user) {
              switch (user.role) {
                case RoleType.Admin:
                  returnUrl = '/admin';
                  break;

                case RoleType.Trgovac:
                  returnUrl = '/registry/users';
                  break;

                case RoleType.Obveznik:
                  returnUrl = '/registry/users';
                  break;

                default:
                  this.authenticationService.logout();
              }
            }
          }

          // navigate
          this.router.navigate([returnUrl]);
        },
        error: (error) => {
          this.error = error;
          this.loading = false;
          if (error.status == 400) {
            this.showLoginErrorMessage = true;
          }
        },
      });
  }

  submitForgotPassword() {
    // stop here if form is invalid
    if (this.forgotPasswordForm.invalid) {
      return;
    }

    let email: string = this.forgotPasswordForm.controls['inputEmail'].value;

    this.authenticationService.forgotPasswordSendEMail(email);
  }

  
}
