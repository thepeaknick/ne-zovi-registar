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

  ngOnInit() {
    let after: Date = new Date();
    after.setMonth(3);
    
    this.userService
      .all(after)
      .forEach(u => console.log(u));

    let phoneNumber: string | null = this.userService.getUser('0652015766');
    console.log(`phone number = ${phoneNumber}`);
  }

}
