import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PaginationComponent } from '@coreui/angular';
import { RegUserDto, RoleType } from 'src/app/domain/model/schemas';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';
import { RegUserService } from 'src/app/domain/services/reguser.service';

@Component({
  selector: 'app-regusers',
  templateUrl: './regusers.component.html',
  styleUrls: ['./regusers.component.scss'],
})
export class RegUsersComponent implements OnInit {
  constructor(
    private regUserService: RegUserService,
    private formBuilder: FormBuilder,
    private authenticationService: AuthenticationService
  ) {}

  @Input() public regUsers: RegUserDto[] = [];

  public isAddReguserModalVisible = false;
  public isSuccessfulyRegisteredUserModalVisible = false;

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
          this.totalPagesNumber = (this.regUsers.length % this.itemsPerPage === 0) ? Math.trunc(this.regUsers.length / this.itemsPerPage) : Math.trunc(this.regUsers.length / this.itemsPerPage) + 1;
          this.setPage(1);
          // this.addRegUsers()
      },
    });
    this.currentPage = 1;
  }

  addRegTestUser() {
    this.regUserService
      .registerRegUser({
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
      })
      .subscribe({
        next: () => {
          console.log('XBV');
        },
        complete: () => {
          console.log('lkj');
        },
      });
  }

  get fields() {
    return this.regUserForm.controls;
  }

  resetFields() {
    this.regUserForm.reset();
    // this.newPassword = '';
    this.toggleConfirmationModal();
  }

  toggleConfirmationModal() {
    this.isSuccessfulyRegisteredUserModalVisible =
      !this.isSuccessfulyRegisteredUserModalVisible;
  }

  toggleAddRegUsernModal() {
    this.isAddReguserModalVisible = !this.isAddReguserModalVisible;
  }

  addRegUsers() {
    this.regUserService
      .registerRegUser({
        name: this.fields['regUserName'].value,
        address: this.fields['regUserAddress'].value,
        regNumber: this.fields['regUserMB'].value,
        taxNumber: this.fields['regUserPIB'].value,
        firstName: this.fields['regUserFirstName'].value,
        lastName: this.fields['regUserLastName'].value,
        userName: this.fields['regUserUsername'].value,
        password: this.fields['regUserPassword'].value,
        email: this.fields['regUserEmail'].value,
        roles: [RoleType.Obveznik],
      })
      .subscribe({
        next: () => {
          console.log('Successfuly registered user');
          this.toggleAddRegUsernModal();
          this.toggleConfirmationModal();
        },
        error: (error) => {
          console.log('Unsuccessfuly registered user complete callback', error);
        },
        complete: () => {
          console.log('Successfuly registered user complete callback');
        },
      });
  }


  setPage(page: number) {
    this.currentPage = page;
    this.showInTableUsers = this.regUsers.slice( (page - 1) * this.itemsPerPage, page * this.itemsPerPage)
  }

  setItemPerPage(num: number) {
    this.itemsPerPage = num;
    this.setPage(this.currentPage)
    this.totalPagesNumber = (this.regUsers.length % this.itemsPerPage === 0) ? Math.trunc(this.regUsers.length / this.itemsPerPage) : Math.trunc(this.regUsers.length / this.itemsPerPage) + 1;
  }

  sortByName() {
    var array = this.regUsers;
    array.sort((a,b) => a.name.localeCompare(b.name));
    this.showInTableUsers = array.slice( (this.currentPage - 1) * this.itemsPerPage, this.currentPage * this.itemsPerPage)
  }

  sortByCreatedOnASC() {
    var array = this.regUsers;
    array.sort((a,b) => a.createdOn.localeCompare(b.createdOn));
    this.showInTableUsers = array.slice( (this.currentPage - 1) * this.itemsPerPage, this.currentPage * this.itemsPerPage)
  }

  sortByCreatedOnDESC() {
    var array = this.regUsers;
    array.sort((a,b) => b.createdOn.localeCompare(a.createdOn));
    this.showInTableUsers = array.slice( (this.currentPage - 1) * this.itemsPerPage, this.currentPage * this.itemsPerPage)
  }

}
