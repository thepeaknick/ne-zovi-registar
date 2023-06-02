import { Component } from '@angular/core';

@Component({
  selector: 'app-page',
  templateUrl: './page-layout.component.html',
  styleUrls: ['./page-layout.component.scss'],
})
export class PageLayoutComponent {
  public perfectScrollbarConfig = {
    suppressScrollX: true,
  };

  constructor() {}
}
