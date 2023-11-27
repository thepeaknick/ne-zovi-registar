import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl, AbstractControl } from '@angular/forms';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';
import { validateFieldsEquality } from './fieldsEqualityValidator';
import { FieldsEqualityValidatorDirective } from './fieldsEqualityValidator.directive';

@Component({
  selector: 'app-search',
  templateUrl: './resetPassword.component.html',
  styleUrls: ['./resetPassword.component.scss'],
})
export class ResetPasswordComponent {

    // public email: string | null | undefined;
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
        private formBuilder: FormBuilder,
        private router: Router
    ) {}

     // convenience getter for easy access to form fields
    get fields() {
        return this.resetPasswordForm.controls;
    }

    get email() { 
        console.log("email ", this.resetPasswordForm.get('email')?.errors)
        return this.resetPasswordForm.get('email'); 
    }

    get newPassword1() { 
        console.log("newP ", this.resetPasswordForm.get('newPassword1')?.errors)
        return this.resetPasswordForm.get('newPassword1'); 
    }

    get newPassword2() { 
        console.log("newP2 ", this.resetPasswordForm.get('newPassword2')?.errors)
        return this.resetPasswordForm.get('newPassword2'); 
    }

    ngOnInit() {
        this.resetPasswordForm = this.formBuilder.group({
            email: ['', [Validators.required, Validators.email]],
            newPassword1: ['', [Validators.required, Validators.minLength]],
            newPassword2: ['', [Validators.required, Validators.minLength]],
        });

        this.route.queryParams.subscribe((params) => {
            // this.email = params['email'];
            this.token = params['token'];
        });

        console.log("t: ", this.token)
        // console.log("e: ", this.email)
    }

    resetPassword(): void {
        this.isValidated = true;

        console.log("this.newPassword1 ", this.fields['newPassword1'].value, "; this.newPassword2: ",this.fields['newPassword2'].value)
        if ((this.fields['newPassword1'].value == this.fields['newPassword2'].value) && (this.fields['newPassword1'].value != '')) {
            this.showResetPasswordErrorMessage = false
            this.isValidated = false;

            if (this.token) {
                this.authenticationService
                .forgotPasswordResetPassword(this.fields['email'].value, this.token, this.fields['newPassword1'].value)
                .subscribe({
                    complete: () => {
                        console.log("complete")
                        this.isSuccessfulyResetPasswordModalVisible = true
                    },
                    error: (error) => {
                        console.log("error")
                        this.requestError = error.error.title;
                        console.log("error ", error)
                    },
                });
            }

        } else if ((this.fields['newPassword1'].value != '')) {
            this.showResetPasswordErrorMessage = true
            this.isValidated = false;
        }


    }

    goToLogin(): void {
        this.isSuccessfulyResetPasswordModalVisible = false
        this.router.navigate(['/']);
    }
}

