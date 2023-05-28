import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { UserService } from 'src/app/domain/services/user.service';
import { UserDto } from '../../../domain/model/schemas';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  constructor(private userService: UserService) {

  }

  users: UserDto[] = [];
  phoneNumber: string | null = null;

  ngOnInit() {
    let after: Date = new Date();
    after.setMonth(3);
    
    this.userService
      .all(after)
      .subscribe(users => this.users = users);

    let response = this.userService
      .getUser('0652015766')
      .subscribe({
        next: pn => { this.phoneNumber = pn; },
        error: err => { this.phoneNumber = 'unknown'; }
      });
  }

}
