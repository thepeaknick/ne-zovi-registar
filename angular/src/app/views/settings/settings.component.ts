import { Component, Input } from '@angular/core';
import {
  FormGroup,
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
})
export class SettingsComponent {
  @Input() currentUsername: string;
  @Input() userNameDisabled: boolean = true;
  @Input() showPasswordChangeErrorMessage: Boolean = false;

  public isConfirmationModalVisible = false;

  resetPasswordForm!: FormGroup;
  loading = false;
  error = '';

  password = '';
  newPassword = '';

  sideMenuForm = new UntypedFormGroup({
    radio1: new UntypedFormControl('sideMenuForm'),
  });

  constructor(
    private formBuilder: UntypedFormBuilder,
    public authenticationService: AuthenticationService,
    private router: Router
  ) {
    this.currentUsername = AuthenticationService.CurrentUserName;
  }

  ngOnInit() {
    this.resetPasswordForm = this.formBuilder.group({
      password: ['', Validators.required],
      newPassword: ['', Validators.required],
    });
  }

  toggleUserNameInput() {
    this.userNameDisabled = !this.userNameDisabled;
  }

  setRadioValue(value: string): void {
    this.sideMenuForm.setValue({ radio1: value });
  }

  get fields() {
    return this.resetPasswordForm.controls;
  }

  resetFields() {
    this.password = '';
    this.newPassword = '';
  }

  changePassword() {
    // stop here if form is invalid
    if (this.resetPasswordForm.invalid) {
      return;
    }

    // TODO: show spinner
    this.loading = true;

    this.authenticationService
      .resetPassword(
        this.fields['password'].value,
        this.fields['newPassword'].value
      )
      .subscribe({
        next: () => {
          // this.authenticationService.refreshToken();
          this.isConfirmationModalVisible = true;
          this.showPasswordChangeErrorMessage = false;
        },
        error: (error) => {
          this.error = error;
          this.loading = false;
          if (error.status == 400) {
            this.showPasswordChangeErrorMessage = true;
          }
        },
      });
  }
}
