import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RegUserDto, RoleType } from 'src/app/domain/model/schemas';
import { RegUserService } from 'src/app/domain/services/reguser.service';

@Component({
  selector: 'app-regusers',
  templateUrl: './regusers.component.html',
  styleUrls: ['./regusers.component.scss'],
})
export class RegUsersComponent implements OnInit {
  constructor(private regUserService: RegUserService) {}

  private regUsers: RegUserDto[] = [];

  ngOnInit(): void {
    this.regUserService.getRegUsers(RoleType.Obveznik).subscribe({
      next: (regUsers: RegUserDto[]) =>
        (this.regUsers = regUsers instanceof HttpErrorResponse ? [] : regUsers),
      complete: () => this.addRegUsers(),
    });
  }

  addRegUsers() {
    if (this.regUsers.length === 0) {
      this.regUserService.registerRegUser({
        name: 'Yettel',
        address: 'Yettel Srbija',
        firstName: 'YUserName',
        lastName: 'YUserLastname',
        password: 'test123',
        regNumber: '123456',
        roles: [RoleType.Obveznik],
        taxNumber: '123456789',
        userName: 'yettel',
      });
    }
  }
}
