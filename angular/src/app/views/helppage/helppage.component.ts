import { Component } from '@angular/core';

@Component({
  selector: 'app-helppage',
  templateUrl: './helppage.component.html',
  styleUrls: ['./helppage.component.scss']
})
export class HelppageComponent {

    items = [1, 2, 3, 4, 5, 6];

    constructor() { }


    vars = {
      '--cui-accordion-active-bg': '#133F85',
      '--cui-accordion-active-color': '#ffffff',
      '--cui-accordion-btn-focus-box-shadow': 'none',
    };
}
