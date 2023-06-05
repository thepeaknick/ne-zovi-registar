import { Component } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent {

  sideMenuForm = new UntypedFormGroup({
    radio1: new UntypedFormControl('sideMenuForm')
  });

  constructor(
    private formBuilder: UntypedFormBuilder
  ) { }

  setRadioValue(value: string): void {
    this.sideMenuForm.setValue({ radio1: value });
  }

}
