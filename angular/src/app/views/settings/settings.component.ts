import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormArray,
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { cilTrash } from '@coreui/icons';
import { Router } from '@angular/router';
import { RegUserAccountArrayDto } from 'src/app/domain/model/schemas';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';
import { RegUserService } from 'src/app/domain/services/reguser.service';
import { IconSetService } from '@coreui/icons-angular';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
})
export class SettingsComponent {
  @Input() currentUsername: string;

  @Input() userNameDisabled: boolean = true;
  @Input() showPasswordChangeErrorMessage: Boolean = false;
  @Input() menuItemSelected: string = 'settingsMyProfileForm';

  regUserAccounts: RegUserAccountArrayDto = { accounts: [] };
  currentUserRegUserID: string;

  public isConfirmationModalVisible = false;
  public isSuccessfulyAccountUpdatedModalVisible = false;

  userAccountsForm!: FormGroup;

  resetPasswordForm!: FormGroup;
  loading = false;
  error = '';

  password = '';
  newPassword = '';

  sideMenuForm = new UntypedFormGroup({
    radio1: new UntypedFormControl('sideMenuForm'),
  });

  icons = { cilTrash };

  constructor(
    public iconSet: IconSetService,
    private router: Router,
    private regUserService: RegUserService,
    private formBuilder: UntypedFormBuilder,
    private userAccountsFormBuilder: FormBuilder,
    public authenticationService: AuthenticationService
  ) {
    this.currentUsername = AuthenticationService.CurrentUserName;
    this.currentUserRegUserID = AuthenticationService.CurrentUser!.guidId;
    iconSet.icons = {
      cilTrash,
    };
  }

  ngOnInit() {
    this.resetPasswordForm = this.formBuilder.group({
      password: ['', Validators.required],
      newPassword: ['', Validators.required],
    });

    this.regUserService
      .getRegUserAccounts(this.currentUserRegUserID)
      .subscribe({
        next: (regUserAccounts: RegUserAccountArrayDto) =>
          (this.regUserAccounts =
            regUserAccounts instanceof HttpErrorResponse
              ? { accounts: [] }
              : regUserAccounts),
        complete: () => {
          if (this.regUserAccounts.accounts.length > 0) {
            this.userAccountsForm = this.userAccountsFormBuilder.group({
              accounts: this.formBuilder.array(this.initItems()),
            });
          }
        },
      });
  }

  customTrackBy(index: number, obj: any): any {
    return index;
  }

  initItems(): FormGroup[] {
    var formArray: FormGroup[] = [];
    this.regUserAccounts.accounts.forEach((key: any, val: any) => {
      console.log(key);
      formArray.push(
        this.formBuilder.group({
          firstName: key.firstName,
          lastName: key.lastName,
          username: key.username,
          password: key.password,
        })
      );
    });
    return formArray;
  }

  createEmptyItem(): FormGroup {
    return this.formBuilder.group({
      firstName: '',
      lastName: '',
      username: '',
      password: '',
    });
  }

  addUserAccount() {
    const accounts = this.userAccountsForm.get('accounts') as FormArray;
    accounts.push(this.createEmptyItem());
  }

  addUserAccountFormControl(fieldName: string, validators: any[] = []) {
    this.userAccountsForm.addControl(
      fieldName,
      this.formBuilder.control('', validators)
    );
  }

  deleteUserAccount(id: number) {
    const accounts = this.userAccountsForm.get('accounts') as FormArray;
    accounts.removeAt(id);
  }

  saveRegUserAccounts() {
    this.regUserService
      .saveRegUserAccounts(
        this.currentUserRegUserID,
        this.userAccountsForm.value
      )
      .subscribe({
        next: (answer: string) => {
          if (JSON.parse(answer) == true) {
            this.toggleModal();
          }
        },
        complete: () => {},
      });
  }

  toggleUserNameInput() {
    this.userNameDisabled = !this.userNameDisabled;
  }

  setRadioValue(value: string): void {
    this.sideMenuForm.setValue({ radio1: value });
    this.menuItemSelected = value;
  }

  get fields() {
    return this.resetPasswordForm.controls;
  }

  toggleModal() {
    this.isSuccessfulyAccountUpdatedModalVisible =
      !this.isSuccessfulyAccountUpdatedModalVisible;
  }
  hideModalAndRedirect() {
    this.toggleModal();
    this.router.navigate(['/admin']);
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
