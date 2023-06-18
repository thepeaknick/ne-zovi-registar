import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { UserService } from 'src/app/domain/services/user.service';
import { ModifyUserRequest, RoleType, UserDto } from '../../../domain/model/schemas';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegUserService } from 'src/app/domain/services/reguser.service';
import { RegUserDto } from '../../../domain/model/schemas';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
})
export class UsersComponent implements OnInit {
  constructor(
    private userService: UserService,
    private regUserService: RegUserService,
    private formBuilder: FormBuilder
  ) {
    let currentUser = AuthenticationService.CurrentUser;
    let role = RoleType.Potrosac;
    if (currentUser) role = currentUser.role;
    this.canAddUsers = role == RoleType.Obveznik;
    this.canEditUsers = role == RoleType.Obveznik;
    this.canDeleteUsers = role == RoleType.Obveznik;
  }

  public isAddUserModalVisible: boolean = false;

  @Input() selectedOperator!: RegUserDto;
  @Input() selectedOperatorId: number = 0;
  @Input() operators: RegUserDto[] = [];

  @Input() users: UserDto[] = [];
  @Input() canAddUsers: boolean = false;
  @Input() canEditUsers: boolean = false;
  @Input() canDeleteUsers: boolean = false;
  phoneNumber: string | null = null;
  divs: number[] = [1];

  showInTableUsers: UserDto[] = [];

  itemsPerPage = 10;
  currentPage = 0;
  totalPagesNumber = 0;

  userForm!: FormGroup;

  addPhoneNumberDiv() {
    this.divs.push(this.divs.length);
  }

  ngOnInit() {
    this.userForm = this.formBuilder.group({
      userPhoneNumber: ['', Validators.required],
      userFirstName: ['', Validators.required],
      userLastName: ['', Validators.required],
      userJMBG: ['', Validators.required],
      userOperator: ['', Validators.required],
    });

    this.reloadUsersAndGoToFirsPage();
  }

  reloadUsersAndGoToFirsPage() {
    let after: Date = new Date();
    after.setMonth(3);

    this.userService.allUsers(after).subscribe({
      next: (users: UserDto[]) =>
        (this.users = users instanceof HttpErrorResponse ? [] : users),
      complete: () => {
        this.totalPagesNumber =
          this.users.length % this.itemsPerPage === 0
            ? Math.trunc(this.users.length / this.itemsPerPage)
            : Math.trunc(this.users.length / this.itemsPerPage) + 1;
        this.setPage(1);
      },
    });
  }

  // Pagination

  setPage(page: number) {
    this.currentPage = page;
    this.showInTableUsers = this.users.slice(
      (page - 1) * this.itemsPerPage,
      page * this.itemsPerPage
    );
  }

  setItemPerPage(num: number) {
    this.itemsPerPage = num;
    this.setPage(this.currentPage);
    this.totalPagesNumber =
      this.users.length % this.itemsPerPage === 0
        ? Math.trunc(this.users.length / this.itemsPerPage)
        : Math.trunc(this.users.length / this.itemsPerPage) + 1;
  }

  // Sort

  sortByCreatedModifiedOnASC() {
    var array = this.users;
    array.sort((a, b) =>
      a.createdModifiedOn.localeCompare(b.createdModifiedOn)
    );
    this.showInTableUsers = array.slice(
      (this.currentPage - 1) * this.itemsPerPage,
      this.currentPage * this.itemsPerPage
    );
  }

  sortByCreatedModifiedOnDESC() {
    var array = this.users;
    array.sort((a, b) =>
      b.createdModifiedOn.localeCompare(a.createdModifiedOn)
    );
    this.showInTableUsers = array.slice(
      (this.currentPage - 1) * this.itemsPerPage,
      this.currentPage * this.itemsPerPage
    );
  }

  // Handle modals
  toggleAddUserModal() {
    this.isAddUserModalVisible = !this.isAddUserModalVisible;
  }

  showAddUserModal() {
    // Dohvati sve operatere
    this.regUserService.getRegUsers(RoleType.Obveznik).subscribe({
      next: (operators: RegUserDto[]) =>
        (this.operators =
          operators instanceof HttpErrorResponse ? [] : operators),
      complete: () => {},
    });
    this.toggleAddUserModal();
  }

  // Data handling

  get fields() {
    return this.userForm.controls;
  }

  resetFields() {
    this.userForm.reset();
  }

  addUserWithNumbers() {
    console.log(this.fields['userFirstName'].value);
    console.log(this.fields['userLastName'].value);
    console.log(this.fields['userJMBG'].value);
    console.log(this.fields['userPhoneNumber'].value);
    console.log(this.selectedOperator.id);

    this.userService
      .addUser({
        firstName: this.fields['userFirstName'].value,
        lastName: this.fields['userLastName'].value,
        jmbg: this.fields['userJMBG'].value,
        phoneNumbers: [this.fields['userPhoneNumber'].value],
        operatorId: this.selectedOperator.id,
      })
      .subscribe({
        next: () => {
          this.isAddUserModalVisible = false;
          console.log('Korisnik je uspeno dodat u registar');
        },
        error: (error) => {
          console.log('Dodavanje korisnika u registar nije uspelo');
        },
      });
  }

  editNumber(number: string) {
    let modifiedUser: ModifyUserRequest = {
      firstName: '',
      lastName: '',
      jmbg: '',
      phoneNumber: '',
      operatorId: 0
    };

    this.userService
    .modifyUser(number, modifiedUser)
    .subscribe({
      next: () => {
        console.log('Podaci o korisniku su promenjeni uspesno');
      },
      error: (error) => {
        console.log('Neuspesno promenjeni podaci o korisniku');
      },
    });
  }

  deleteNumber(number: string) {
    this.userService.removeUser(number).subscribe({
      next: () => {
        this.showInTableUsers.forEach((element, index) => {
          if (element.phoneNumber == number) {
            this.showInTableUsers.splice(index, 1);
          }
        });
        // TODO: Show success modal
        console.log('DELETED: ' + number);
      },
      error: (error) => {
        console.log('Neuspesno promenjeni podaci o obvezniku');
      },
    });
  }
}
