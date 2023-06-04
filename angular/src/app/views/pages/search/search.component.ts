import { Component } from '@angular/core';
import { OffcanvasComponent } from '@coreui/angular';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent {
  token: string | undefined;

  constructor() {
    this.token = undefined;
  }
}
