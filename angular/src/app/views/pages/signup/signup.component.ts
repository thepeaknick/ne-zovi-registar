import { BooleanInput } from '@angular/cdk/coercion';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegUserDto, RoleType } from 'src/app/domain/model/schemas';
import { RegUserService } from 'src/app/domain/services/reguser.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignUpComponent implements OnInit {

  @Input() token: string | undefined;
  @Input() showCaptchaMessage: boolean = false;

  constructor(
    private router: Router,
    private regUserService: RegUserService,
    private formBuilder: FormBuilder
  ) {
    this.token = undefined;
  }

  regUserForm!: FormGroup;
  isValidated: BooleanInput = false;
  public modalText = '';

  public isSuccessfulyRegisteredUserModalVisible = false;

  ngOnInit(): void {
    this.regUserForm = this.formBuilder.group({
      regUserName: ['', Validators.required],
      regUserAddress: ['', Validators.required],
      regUserMB: [
        '',
        [
          Validators.required,
          Validators.minLength,
          Validators.maxLength,
          Validators.pattern,
        ],
      ],
      regUserPIB: [
        '',
        [
          Validators.required,
          Validators.minLength,
          Validators.maxLength,
          Validators.pattern,
        ],
      ],
      regUserUsername: ['', [Validators.required, Validators.minLength]],
      regUserPassword: ['', [Validators.required, Validators.minLength]],
      regUserEmail: ['', [Validators.required, Validators.email]],
      regUserFirstName: ['', Validators.required],
      regUserLastName: ['', Validators.required],
    });
  }

  // Data handling
  get fields() {
    return this.regUserForm.controls;
  }

  addRegUser() {
    this.isValidated = true;

    if (!this.allFieldsValidated()) {
      return;
    }

    if (this.token == undefined) {
      this.showCaptchaMessage = true;
      return
    }

    this.regUserService
      .registerRegUserWithoutAuth({
        name: this.fields['regUserName'].value,
        address: this.fields['regUserAddress'].value,
        regNumber: this.fields['regUserMB'].value,
        taxNumber: this.fields['regUserPIB'].value,
        firstName: this.fields['regUserFirstName'].value,
        lastName: this.fields['regUserLastName'].value,
        userName: this.fields['regUserUsername'].value,
        password: this.fields['regUserPassword'].value,
        email: this.fields['regUserEmail'].value,
        role: RoleType.Trgovac,
      })
      .subscribe({
        next: () => {
          console.debug('Successfuly registered user');

          this.modalText = 'Uspešno ste registrovali novog trgovca';
          this.toggleConfirmationModal();
        },
        error: (error) => {
          console.debug(
            'Unsuccessfuly registered user complete callback',
            error
          );
        },
        complete: () => {
          console.log('Successfuly registered user complete callback');
        },
      });
  }

  allFieldsValidated(): Boolean {
    if (
      this.fields['regUserName'].valid &&
      this.fields['regUserAddress'].valid &&
      this.fields['regUserMB'].valid &&
      this.fields['regUserPIB'].valid &&
      this.fields['regUserFirstName'].valid &&
      this.fields['regUserLastName'].valid &&
      this.fields['regUserUsername'].valid &&
      this.fields['regUserPassword'].valid &&
      this.fields['regUserEmail'].valid
    ) {
      return true;
    } else {
      return false;
    }
  }

  toggleConfirmationModal() {
    this.isSuccessfulyRegisteredUserModalVisible =
      !this.isSuccessfulyRegisteredUserModalVisible;
  }

  resetFields() {
    this.isValidated = false;
    this.regUserForm.reset();
    this.toggleConfirmationModal();
    this.router.navigate(['/login']);
  }

  goBack() {
    this.router.navigate(['/login']);
  }
}
