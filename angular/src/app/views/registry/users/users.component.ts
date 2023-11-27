import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { UserService } from 'src/app/domain/services/user.service';
import {
  ModifyUserRequest,
  RoleType,
  UserDto,
} from '../../../domain/model/schemas';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegUserService } from 'src/app/domain/services/reguser.service';
import { RegUserDto, UserDtoPagedList } from '../../../domain/model/schemas';
import * as XLSX from 'xlsx';
import { BooleanInput } from '@angular/cdk/coercion';

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
    // this.canEditUsers = role == RoleType.Obveznik;
    this.canDeleteUsers = role == RoleType.Obveznik;
  }

  public isAddUserModalVisible: boolean = false;
  public isSuccessfulyAddedUserModalVisible: boolean = false;
  public modalText = '';

  @Input() selectedOperator!: RegUserDto;
  @Input() selectedOperatorId: number = 0;
  @Input() operators: RegUserDto[] = [];

  @Input() users!: UserDtoPagedList;
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

  fileName = 'KorisniciExcelSheet.xlsx';

  isValidated: BooleanInput = false;

  addPhoneNumberDiv() {
    this.divs.push(this.divs.length);
  }

  ngOnInit() {
    this.userForm = this.formBuilder.group({
      userPhoneNumber: [
        '',
        [Validators.required, Validators.minLength, Validators.pattern],
      ],
      userFirstName: ['', Validators.required],
      userLastName: ['', Validators.required],
      userJMBG: [
        '',
        [
          Validators.required,
          Validators.minLength,
          Validators.maxLength,
          Validators.pattern,
        ],
      ],

      userOperator: ['', [Validators.required, Validators.min(1)]],
    });

    this.reloadUsersAndGoToFirsPage();
  }

  exportExcel(): void {
    let data = this.users?.items.map((item) => {
      const formattedDate = new Date(item.registeredOn).toLocaleDateString();

      return {
        'Broj telefona': item.phoneNumber,
        'Datum upisa/ispisa': formattedDate,
      };
    });
    const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(data);

    // Set column widths
    const columnWidths = [
      { wch: 30 }, // Column A width
      { wch: 30 }, // Column B width
    ];

    // Update column widths in the worksheet
    columnWidths.forEach((width, colIndex) => {
      ws['!cols'] = ws['!cols'] || [];
      ws['!cols'][colIndex] = { wch: width.wch };
    });

    // Update first row height
    ws['!rows'] = ws['!rows'] || [];
    ws['!rows'][0] = { hpx: 30 };

    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

    /* save to file */
    XLSX.writeFile(wb, this.fileName);
  }

  reloadUsersAndGoToFirsPage() {
    let after: Date = new Date();
    after.setMonth(3);

    this.userService.allUsers(after).subscribe({
      next: (users: UserDtoPagedList) => {
        let tmpUsers = users;
        tmpUsers.items
          .slice()
          .reverse()
          .forEach((user, index, object) => {
            if (user.active == false) {
              console.log(user);
              console.log(index);
              users.items.splice(object.length - 1 - index, 1);
            } else {
              console.log(user);
            }
          });
        this.users =
          users instanceof HttpErrorResponse ? ({} as UserDtoPagedList) : users;
      },
      complete: () => {
        if (this.users.items) {
          this.totalPagesNumber =
            this.users.items.length % this.itemsPerPage === 0
              ? Math.trunc(this.users.items.length / this.itemsPerPage)
              : Math.trunc(this.users.items.length / this.itemsPerPage) + 1;
        }
        this.setPage(1);
      },
    });
  }

  // Pagination

  setPage(page: number) {
    if (this.users.items) {
      this.currentPage = page;
      this.showInTableUsers = this.users.items.slice(
        (page - 1) * this.itemsPerPage,
        page * this.itemsPerPage
      );
    }
  }

  setItemPerPage(num: number) {
    this.itemsPerPage = num;
    this.setPage(this.currentPage);
    this.totalPagesNumber =
      this.users.items.length % this.itemsPerPage === 0
        ? Math.trunc(this.users.items.length / this.itemsPerPage)
        : Math.trunc(this.users.items.length / this.itemsPerPage) + 1;
  }

  // Sort

  sortByCreatedModifiedOnASC() {
    var array = this.users.items;
    array.sort((a, b) => a.registeredOn.localeCompare(b.registeredOn));
    this.showInTableUsers = array.slice(
      (this.currentPage - 1) * this.itemsPerPage,
      this.currentPage * this.itemsPerPage
    );
  }

  sortByCreatedModifiedOnDESC() {
    var array = this.users.items;
    array.sort((a, b) => b.registeredOn.localeCompare(a.registeredOn));
    this.showInTableUsers = array.slice(
      (this.currentPage - 1) * this.itemsPerPage,
      this.currentPage * this.itemsPerPage
    );
  }

  // Handle modals
  toggleAddUserModal() {
    this.isValidated = false;
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
    this.operators = [];
    this.toggleConfirmationModal();
  }

  addUserWithNumbers() {
    this.isValidated = true;

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
          this.modalText = 'Uspešno dodat novi korisnik/broj';
          this.isAddUserModalVisible = false;
          this.toggleConfirmationModal();
          // console.log('Korisnik je uspešno dodat u registar');
        },
        error: (error) => {
          // console.log('Dodavanje korisnika u registar nije uspelo');
        },
      });
  }

  editNumber(number: string) {
    let modifiedUser: ModifyUserRequest = {
      firstName: '',
      lastName: '',
      jmbg: '',
      phoneNumber: '',
      operatorId: 0,
    };

    this.userService.modifyUser(number, modifiedUser).subscribe({
      next: () => {
        // console.log('Podaci o korisniku su promenjeni uspesno');
      },
      error: (error) => {
        // console.log('Neuspesno promenjeni podaci o korisniku');
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

        this.modalText = 'Uspešno obrisan korisnik';
        this.toggleConfirmationModal();
      },
      error: (error) => {
        // console.log('Neuspesno promenjeni podaci o obvezniku');
      },
    });
  }

  toggleConfirmationModal() {
    this.isSuccessfulyAddedUserModalVisible =
      !this.isSuccessfulyAddedUserModalVisible;
  }
}
