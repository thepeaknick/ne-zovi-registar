import { Component, OnInit } from '@angular/core';
import { RegUserService } from 'src/app/domain/services/reguser.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private regUserService: RegUserService) {

  }


  ngOnInit() {
    /*
    let after: Date = new Date();
    after.setMonth(3);
    
    this.userService
      .allUsers(after)
      .subscribe(users => this.users = users);
    */

    this.regUserService
      .loginRegUser({
        username: "ratel",
        password: "test123"
      });

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
