import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  RegUserDetailsDto,
  RegUserDto,
  RoleType,
} from 'src/app/domain/model/schemas';
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
  @Input() public modalAddEditUserTitle: string = 'Dodaj novog obveznika';
  @Input() public modalAddEditUserConfirmButton: string = 'Dodaj obveznika';
  @Input() public isEditing: boolean = true;

  public isAddReguserModalVisible = false;
  public isSuccessfulyRegisteredUserModalVisible = false;

  showInTableUsers: RegUserDto[] = [];

  itemsPerPage = 10;
  currentPage = 0;
  totalPagesNumber = 0;

  private editingUserGuidId = '';
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
        this.totalPagesNumber =
          this.regUsers.length % this.itemsPerPage === 0
            ? Math.trunc(this.regUsers.length / this.itemsPerPage)
            : Math.trunc(this.regUsers.length / this.itemsPerPage) + 1;
        this.setPage(1);
        // this.addRegUsers()
      },
    });
    this.currentPage = 1;

    // 2023-06-11T12:58:03.3910839
  }

  // Data handling

  get fields() {
    return this.regUserForm.controls;
  }

  resetFields() {
    this.regUserForm.reset();
    this.toggleConfirmationModal();
  }

  modifyRegUser() {
    this.regUserService
      .modifyRegUserByGuid(this.editingUserGuidId, {
        name: this.fields['regUserName'].value,
        address: this.fields['regUserAddress'].value,
        email: this.fields['regUserEmail'].value,
        regNumber: this.fields['regUserMB'].value,
        taxNumber: this.fields['regUserPIB'].value,
        firstName: this.fields['regUserFirstName'].value,
        lastName: this.fields['regUserLastName'].value,
        userName: this.fields['regUserUsername'].value,
        role: RoleType.Obveznik,
      })
      .subscribe({
        next: () => {},
        error: (error) => {
          console.log('Neuspesno promenjeni podaci o obvezniku');
        },
      });
  }

  addRegUser() {
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
        role: RoleType.Obveznik,
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

  // Modal handling

  toggleConfirmationModal() {
    this.isSuccessfulyRegisteredUserModalVisible =
      !this.isSuccessfulyRegisteredUserModalVisible;
  }

  toggleAddRegUsernModal() {
    this.isAddReguserModalVisible = !this.isAddReguserModalVisible;
  }

  showAddUserModal() {
    this.modalAddEditUserTitle = 'Dodaj novog obveznika';
    this.modalAddEditUserConfirmButton = 'Dodaj obveznika';
    this.isEditing = false;
    this.regUserForm.reset();
    this.toggleAddRegUsernModal();
  }

  showEditUserDataModal(guidId: string) {
    this.isEditing = true;
    this.modalAddEditUserTitle = 'Izmeni podatke o obvezniku';
    this.modalAddEditUserConfirmButton = 'Sačuvaj izmene';
    this.editingUserGuidId = guidId;

    this.regUserService.getRegUserData(guidId).subscribe({
      next: (regUser: RegUserDetailsDto) => {
        // TODO: Proveri zasto ne radi API
        this.regUserForm.patchValue({
          regUserName: regUser.companyName,
          regUserAddress: regUser.address,
          regUserMB: regUser.regNumber,
          regUserPIB: regUser.taxNumber,
          regUserFirstName: regUser.firstName,
          regUserLastName: regUser.lastName,
          regUserEmail: regUser.email,
          regUserUsername: regUser.userName,
        });
      },
      error: (error) => {
        console.log('Neuspesno dohvaceni podaci o obvezniku');
      },
    });
    this.toggleAddRegUsernModal();
  }

  // Pagination

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
      this.regUsers.length % this.itemsPerPage === 0
        ? Math.trunc(this.regUsers.length / this.itemsPerPage)
        : Math.trunc(this.regUsers.length / this.itemsPerPage) + 1;
  }

  // Sort

  sortByName() {
    var array = this.regUsers;
    array.sort((a, b) => a.name.localeCompare(b.name));
    this.showInTableUsers = array.slice(
      (this.currentPage - 1) * this.itemsPerPage,
      this.currentPage * this.itemsPerPage
    );
  }

  sortByCreatedOnASC() {
    var array = this.regUsers;
    array.sort((a, b) => a.createdOn.localeCompare(b.createdOn));
    this.showInTableUsers = array.slice(
      (this.currentPage - 1) * this.itemsPerPage,
      this.currentPage * this.itemsPerPage
    );
  }

  sortByCreatedOnDESC() {
    var array = this.regUsers;
    array.sort((a, b) => b.createdOn.localeCompare(a.createdOn));
    this.showInTableUsers = array.slice(
      (this.currentPage - 1) * this.itemsPerPage,
      this.currentPage * this.itemsPerPage
    );
  }
}
