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

  ratelMapLink =
    'https://www.google.com/maps/place/%D0%A0%D0%90%D0%A2%D0%95%D0%9B/@44.8123526,20.4672521,18.65z/data=!4m6!3m5!1s0x475a7ab4d3e7c459:0xa0bbee3519b4b839!8m2!3d44.8125548!4d20.4678093!16s%2Fg%2F11cn3v72vd?authuser=0&entry=ttu';

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
        this.formSubmitted = true;
        this.messageForm.reset();
      });
  }

  done(): void {
    this.formSubmitted = false;
  }
}
