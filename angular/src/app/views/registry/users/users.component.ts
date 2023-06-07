import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { UserService } from 'src/app/domain/services/user.service';
import { UserDto } from '../../../domain/model/schemas';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
})
export class UsersComponent implements OnInit {
  constructor(private userService: UserService) {}

  users: UserDto[] = [];
  phoneNumber: string | null = null;
  divs: number[] = [1];

  createDiv() {
    console.log('qwe');
    this.divs.push(this.divs.length);
  }

  ngOnInit() {
    let after: Date = new Date();
    after.setMonth(3);

    this.userService
      .allUsers(after)
      .subscribe(
        (users) =>
          (this.users = users instanceof HttpErrorResponse ? [] : users)
      );

    /*
    this.userService
      .addUser({ 
        firstName: "FirstName",
        lastName: "LAst name",
        jmbg: "0110969710420",
        operatorId: 2,
        phoneNumber: "0641946800"
      });
*/

    // this.userService
    // .modifyUser("0641946800", {
    //   firstName: "Goran",
    //   lastName: "Zafirovic",
    //   jmbg: "0110969710420",
    //   operatorId: 2,
    //   phoneNumber: "0652015766"
    // });

    //this.userService.removeUser("0652015766");

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
