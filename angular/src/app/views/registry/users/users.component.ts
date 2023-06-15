import { Component, Input, OnInit } from '@angular/core';
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

  @Input() users: UserDto[] = [];
  phoneNumber: string | null = null;
  divs: number[] = [1];

  showInTableUsers: UserDto[] = [];

  itemsPerPage = 10;
  currentPage = 0;
  totalPagesNumber = 0;

  createDiv() {
    this.divs.push(this.divs.length);
  }

  ngOnInit() {
    let after: Date = new Date();
    after.setMonth(3);

    this.userService.allUsers(after).subscribe((users) => {
      console.log('SAD');
      console.log(users);
      this.users = users instanceof HttpErrorResponse ? [] : users;
    });
    this.userService.allUsers(after).subscribe({
      next: (users: UserDto[]) =>
        (this.users = users instanceof HttpErrorResponse ? [] : users),
      complete: () => this.addUsers(),
    });
  }

  addUsers() {
    if (this.users.length === 0) {
      console.log('nema korisnika');
    }
  }

  setPage(page: number) {
    this.currentPage = page;
    this.showInTableUsers = this.users.slice( (page - 1) * this.itemsPerPage, page * this.itemsPerPage)
  }

  setItemPerPage(num: number) {
    this.itemsPerPage = num;
    this.setPage(this.currentPage)
    this.totalPagesNumber = Math.trunc(this.users.length / this.itemsPerPage) + 1;
  }
}
