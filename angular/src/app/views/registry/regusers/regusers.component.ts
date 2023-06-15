import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PaginationComponent } from '@coreui/angular';
import { RegUserDto, RoleType } from 'src/app/domain/model/schemas';
import { RegUserService } from 'src/app/domain/services/reguser.service';

@Component({
  selector: 'app-regusers',
  templateUrl: './regusers.component.html',
  styleUrls: ['./regusers.component.scss'],
})
export class RegUsersComponent implements OnInit {
  constructor(
    private regUserService: RegUserService,
    private formBuilder: FormBuilder
  ) {}

  @Input() public regUsers: RegUserDto[] = [];

  showInTableUsers: RegUserDto[] = [];

  itemsPerPage = 10;
  currentPage = 0;
  totalPagesNumber = 0;

  regUserForm!: FormGroup;

  ngOnInit(): void {
    this.regUserForm = this.formBuilder.group({
      regUserName: ['', Validators.required],
      regUserAddress: ['', Validators.required],
      regUserMB: ['', Validators.required],
      regUserPIB: ['', Validators.required],
      regUserUsername: ['', Validators.required],
      regUserPassword: ['', Validators.required],
      regUserEmail: ['', Validators.required],
      regUserFirstName: ['', Validators.required],
      regUserLastName: ['', Validators.required],
    });

    this.regUserService.getRegUsers(RoleType.Obveznik).subscribe({
      next: (regUsers: RegUserDto[]) =>
        (this.regUsers = regUsers instanceof HttpErrorResponse ? [] : regUsers),
      complete: () => {
        console.log(' this.regUsers.length ', this.regUsers.length);
        this.totalPagesNumber =
          Math.trunc(this.regUsers.length / this.itemsPerPage) + 1;
        this.setPage(1);
        // {}
      },
    });
    this.currentPage = 1;

    // 2023-06-11T12:58:03.3910839
  }

  addRegTestUser() {
    this.regUserService.registerRegUser({
      name: 'jetel',
      address: 'mala4',
      regNumber: '50505050',
      taxNumber: '505050505',
      firstName: 'milenko',
      lastName: 'milenkovic',
      userName: 'mmmilenkovic',
      password: 'test123',
      email: 'jetel@jetel.com',
      roles: [RoleType.Obveznik],
    });

    // this.regUserService.registerRegUser({
    //   name: this.fields['regUserName'].value,
    //   address: this.fields['regUserAddress'].value,
    //   regNumber: this.fields['regUserMB'].value,
    //   taxNumber: this.fields['regUserPIB'].value,
    //   firstName: this.fields['regUserFirstName'].value,
    //   lastName: this.fields['regUserLastName'].value,
    //   userName: this.fields['regUserUsername'].value,
    //   password: this.fields['regUserPassword'].value,
    //   email: this.fields['regUserEmail'].value,
    //   roles: [RoleType.Obveznik],
    // });
  }

  get fields() {
    return this.regUserForm.controls;
  }

  addRegUsers() {
    // if (this.regUsers.length === 0) {
    //   this.regUserService.registerRegUser({
    //     name: 'Yettel',
    //     address: 'Yettel Srbija',
    //     firstName: 'YUserName',
    //     lastName: 'YUserLastname',
    //     password: 'test123',
    //     regNumber: '123456',
    //     roles: [RoleType.Obveznik],
    //     taxNumber: '123456789',
    //     userName: 'yettel',
    //   });
    // }
  }

  setPage(page: number) {
    this.currentPage = page;
    this.showInTableUsers = this.regUsers.slice(
      (page - 1) * this.itemsPerPage,
      page * this.itemsPerPage
    );
  }

  setItemPerPage(num: number) {
    this.itemsPerPage = num;
    this.setPage(this.currentPage);
    this.totalPagesNumber =
      Math.trunc(this.regUsers.length / this.itemsPerPage) + 1;
  }
}
