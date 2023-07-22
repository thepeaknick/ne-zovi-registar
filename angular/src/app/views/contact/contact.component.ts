import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegUserService } from 'src/app/domain/services/reguser.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss'],
})
export class ContactComponent {
  formSubmitted: Boolean = false;
  messageForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private regUserService: RegUserService
  ) {}

  ngOnInit() {
    this.messageForm = this.formBuilder.group({
      inputName: ['', Validators.required],
      inputEmail: ['', Validators.email],
      inputCompanyName: ['', Validators.required],
      inputPhone: ['', Validators.required],
      inputMessage: ['', Validators.required],
    });
  }

  sendMessage() {
    // this.formSubmitted = true;

    let firstName: string = this.messageForm.controls['inputName'].value;
    let lastName: string = 'basta';
    let emailFrom: string = this.messageForm.controls['inputEmail'].value;
    let companyName: string =
      this.messageForm.controls['inputCompanyName'].value;
    let phoneNumber: string = this.messageForm.controls['inputPhone'].value;
    let content: string = this.messageForm.controls['inputMessage'].value;

    console.log(content);
    this.regUserService
      .sendEmail({
        firstName,
        lastName,
        companyName,
        emailFrom,
        phoneNumber,
        content,
      })
      .subscribe((answer: any) => {
        console.log(answer);
        this.formSubmitted = true;
      });
  }

  done(): void {
    this.formSubmitted = false;
  }
}
