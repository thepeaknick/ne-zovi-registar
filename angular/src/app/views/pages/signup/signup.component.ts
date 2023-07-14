import { BooleanInput } from '@angular/cdk/coercion';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignUpComponent implements OnInit {

  constructor(
    private router: Router,
    private formBuilder: FormBuilder
  ) {}
  
  regUserForm!: FormGroup;
  isValidated: BooleanInput = false;

  public isSuccessfulyRegisteredUserModalVisible = false;

  ngOnInit(): void {
    this.regUserForm = this.formBuilder.group({
      regUserName: ['', Validators.required],
      regUserAddress: ['', Validators.required],
      regUserMB: ['', [Validators.required, Validators.minLength, Validators.maxLength, Validators.pattern]],
      regUserPIB: ['', [Validators.required, Validators.minLength, Validators.maxLength, Validators.pattern,]],
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
  }

  allFieldsValidated(): Boolean {
    if  (
          this.fields['regUserName'].valid &&
          this.fields['regUserAddress'].valid &&
          this.fields['regUserMB'].valid &&
          this.fields['regUserPIB'].valid &&
          this.fields['regUserFirstName'].valid &&
          this.fields['regUserLastName'].valid &&
          this.fields['regUserUsername'].valid &&
          this.fields['regUserPassword'].valid &&
          this.fields['regUserEmail'].valid
        )
    {
      return true;
    } else {
      return false;
    }
  }

  toggleConfirmationModal() {
    this.isSuccessfulyRegisteredUserModalVisible = !this.isSuccessfulyRegisteredUserModalVisible;
  }

  resetFields() {
    this.isValidated = false;
    this.regUserForm.reset();
    this.toggleConfirmationModal();    
  }

  goBack() {
    this.router.navigate(['/login']);
  }
  
}
