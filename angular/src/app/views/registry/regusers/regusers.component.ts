import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { RegUserDto, RoleType } from 'src/app/domain/model/schemas';
import { RegUserService } from 'src/app/domain/services/reguser.service';

@Component({
  selector: 'app-regusers',
  templateUrl: './regusers.component.html',
  styleUrls: ['./regusers.component.scss'],
})
export class RegUsersComponent implements OnInit {
  constructor(private regUserService: RegUserService) {}

  @Input() public regUsers: RegUserDto[] = [];

  ngOnInit(): void {
    this.regUserService.getRegUsers(RoleType.Obveznik).subscribe({
      next: (regUsers: RegUserDto[]) =>
        (this.regUsers = regUsers instanceof HttpErrorResponse ? [] : regUsers),
      complete: () => this.completeAllRegUsers(),
    });
    // 2023-06-11T12:58:03.3910839
  }

  completeAllRegUsers() {
    if (this.regUsers.length === 0) {
      console.log('nema registrovanih obveznika');
    }
  }
}
