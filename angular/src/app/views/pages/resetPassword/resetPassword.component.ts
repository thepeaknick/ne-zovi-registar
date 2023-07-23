import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-search',
  templateUrl: './resetPassword.component.html',
  styleUrls: ['./resetPassword.component.scss'],
})
export class ResetPasswordComponent {

    public email: string | null | undefined;
    public token: string | null | undefined;

    resetPasswordForm!: FormGroup;

    public isSuccessfulyResetPasswordModalVisible = false
    public showResetPasswordErrorMessage = false
    public showResetPasswordRequestErrorMessage = false

    isValidated = false;
    requestError = '';

    constructor(
        private route: ActivatedRoute,
        private authenticationService: AuthenticationService,
        private formBuilder: FormBuilder
    ) {}

     // convenience getter for easy access to form fields
    get fields() {
        return this.resetPasswordForm.controls;
    }

    ngOnInit() {
        this.resetPasswordForm = this.formBuilder.group({
            newPassword1: ['', [Validators.required, Validators.minLength]],
            newPassword2: ['', [Validators.required, Validators.minLength]],
        });

        this.route.queryParams.subscribe((params) => {
            this.email = params['email'];
            this.token = params['token'];
        });

        console.log("t: ", this.token)
        console.log("e: ", this.email)
    }

    resetPassword(): void {
        this.isValidated = true;

        console.log("this.newPassword1 ", this.fields['newPassword1'].value, "; this.newPassword2: ",this.fields['newPassword2'].value)
        if ((this.fields['newPassword1'].value == this.fields['newPassword2'].value) && (this.fields['newPassword1'].value != '')) {
            this.showResetPasswordErrorMessage = false
            this.isSuccessfulyResetPasswordModalVisible = true

            if (this.email && this.token) {
                this.authenticationService
                .forgotPasswordResetPassword(this.email, this.token, this.fields['newPassword1'].value)
                .subscribe({
                    next: () => {
                        this.isSuccessfulyResetPasswordModalVisible = true
                    },
                    error: (error) => {
                        this.requestError = error.error.title;
                        console.log("error ", error)
                    },
                });
            }

        } else if ((this.fields['newPassword1'].value != '')) {
            this.showResetPasswordErrorMessage = true
        }


    }

    goToLogin(): void {
        this.isSuccessfulyResetPasswordModalVisible = false
    }
}
