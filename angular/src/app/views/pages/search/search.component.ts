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
        },
        error: (error) => {
          this.isFound = false;
          if (error.error != undefined) {
            this.showErrorMessage = true;
            this.apiErrorMessage = error.error;
          } else {
            this.showSearchMessage = true;
          }
          console.log("error: ", error.error);
        },
      });
    } else {
      this.showCaptchaMessage = true;
    }
  }

  ngOnInit() {
    // this.userService.getUser('0641234567').subscribe((value: any) => {
    // this.userService.getUser('0641323236').subscribe((value: any) => {
    //   this.phoneNumber = value;
    // });
    /*
    let after: Date = new Date();
    after.setMonth(3);
    
    this.userService
      .allUsers(after)
      .subscribe(users => this.users = users);
    */
    /*
    let response = this.userService
      .getUser('0652015766')
      .subscribe({
        next: pn => { this.phoneNumber = pn; },
        error: err => { this.phoneNumber = 'unknown'; }
      });
      */
  }
}
