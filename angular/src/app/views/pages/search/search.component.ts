import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { OffcanvasComponent } from '@coreui/angular';
import { UserService } from 'src/app/domain/services/user.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent {
  @Input() token: string | undefined;
  @Input() showCaptchaMessage: boolean = false;
  phoneNumber: string;

  @Input() isFound: Boolean = false;
  @Input() isEntered: Boolean = true;
  @Input() showSearchMessage: Boolean = false;
  @Input() showErrorMessage: Boolean = false;
  @Input() apiErrorMessage: string = '';

  constructor(private userService: UserService) {
    this.token = undefined;
    this.phoneNumber = '';
  }

  vars = {
    '--cui-modal-width': '900px',
  };

  checkNumber() {
    this.isEntered = true;
    this.isFound = false;
    this.showSearchMessage = false;

    // console.log("phoneNumber: ", this.phoneNumber)
    if (this.phoneNumber == '') {
      this.isEntered = false;
      return;
    }
    if (this.token != undefined) {
      this.userService.getUser(this.phoneNumber).subscribe({
        next: () => {
          this.isFound = true;
          this.showSearchMessage = true;
          this.showErrorMessage = false;
        },
        error: (error) => {
          this.isFound = false;
          if (error.error != undefined) {
            this.showErrorMessage = true;
            this.showSearchMessage = false;
            this.apiErrorMessage = error.error;
          } else {
            this.showSearchMessage = true;
            this.showErrorMessage = false;
          }
          // console.log("error: ", error.error);
        },
      });
    } else {
      this.showCaptchaMessage = true;
    }
  }

  ngOnInit() {}
}
