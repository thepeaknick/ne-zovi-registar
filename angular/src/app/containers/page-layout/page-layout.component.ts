import { Component } from '@angular/core';

@Component({
  selector: 'app-page',
  templateUrl: './page-layout.component.html',
})
export class PageLayoutComponent {

  public perfectScrollbarConfig = {
    suppressScrollX: true,
  };

  constructor() {}
}
