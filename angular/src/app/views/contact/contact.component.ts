import { Component } from '@angular/core';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent {

  formSubmitted: Boolean = false;

  submit(): void {
    this.formSubmitted = true
  }

  done(): void {
    this.formSubmitted = false
  }

}
