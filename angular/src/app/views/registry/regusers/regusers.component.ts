import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { PaginationComponent } from '@coreui/angular';
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

  showInTableUsers: RegUserDto[] = [];

  itemsPerPage = 10;
  currentPage = 0;
  totalPagesNumber = 0;
  pages: number[] = [];

  ngOnInit(): void {
    this.regUserService.getRegUsers(RoleType.Obveznik).subscribe({
      next: (regUsers: RegUserDto[]) =>
        (this.regUsers = regUsers instanceof HttpErrorResponse ? [] : regUsers),
      complete: () => { 
          console.log(" this.regUsers.length ",  this.regUsers.length); 
          this.totalPagesNumber = Math.trunc(this.regUsers.length / this.itemsPerPage) + 1;
          this.setPage(1);
          // this.addRegUsers()
      },
    });
    this.currentPage = 1;
  }

  completeAllRegUsers() {
    if (this.regUsers.length === 0) {
      console.log('nema registrovanih obveznika');
    }
  }

  setPage(page: number) {
    this.currentPage = page;
    this.showInTableUsers = this.regUsers.slice( (page - 1) * this.itemsPerPage, page * this.itemsPerPage)
  }

  setItemPerPage(num: number) {
    this.itemsPerPage = num;
    this.setPage(this.currentPage)
    this.totalPagesNumber = Math.trunc(this.regUsers.length / this.itemsPerPage) + 1;
  }

  vars = {
    '--cui-pagination-active-bg': '#321fdb !important'
  };

}
