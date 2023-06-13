import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { RegUserDto, RoleType, UserDto } from 'src/app/domain/model/schemas';
import { RegUserService } from 'src/app/domain/services/reguser.service';

@Component({
  selector: 'app-merchants',
  templateUrl: './merchants.component.html',
  styleUrls: ['./merchants.component.scss'],
})
export class MerchantsComponent {
  constructor(private regUserService: RegUserService) {}

  @Input() public regUsers: RegUserDto[] = [];

  ngOnInit(): void {
    this.regUserService.getRegUsers(RoleType.Trgovac).subscribe({
      next: (regUsers: RegUserDto[]) =>
        (this.regUsers = regUsers instanceof HttpErrorResponse ? [] : regUsers),
      complete: () => this.addRegUsers(),
    });
    // 2023-06-11T12:58:03.3910839
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
