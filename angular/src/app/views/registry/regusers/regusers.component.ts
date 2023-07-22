import { BooleanInput } from '@angular/cdk/coercion';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  RegUserDetailsDto,
  RegUserDto,
  RoleType,
} from 'src/app/domain/model/schemas';
import { RegUserService } from 'src/app/domain/services/reguser.service';
import * as XLSX from 'xlsx';

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

  public modalText = '';
  public isAddReguserModalVisible = false;
  public isSuccessfulyRegisteredUserModalVisible = false;
  public isSuccessfulyDeleted = false;

  showInTableUsers: RegUserDto[] = [];

  itemsPerPage = 10;
  currentPage = 0;
  totalPagesNumber = 0;

  private editingUserGuidId = '';
  regUserForm!: FormGroup;

  isValidated: BooleanInput = false;

  fileName = 'ObvezniciExcelSheet.xlsx';

  ngOnInit(): void {
    this.regUserForm = this.formBuilder.group({
      regUserName: ['', Validators.required],
      regUserAddress: ['', Validators.required],
      regUserMB: [
        '',
        [
          Validators.required,
          Validators.minLength,
          Validators.maxLength,
          Validators.pattern,
        ],
      ],
      regUserPIB: [
        '',
        [
          Validators.required,
          Validators.minLength,
          Validators.maxLength,
          Validators.pattern,
        ],
      ],
      regUserUsername: ['', [Validators.required, Validators.minLength]],
      regUserPassword: ['', [Validators.required, Validators.minLength]],
      regUserEmail: ['', [Validators.required, Validators.email]],
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

  exportExcel(): void {
    let data = this.regUsers.map(({ guidId, id, createdOn, ...item }) => {
      const formattedDate = new Date(createdOn).toLocaleDateString();

      return {
        'Ime firme': item.name,
        PIB: item.taxNumber,
        MB: item.regNumber,
        'Datum upisa': formattedDate,
      };
    });
    const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(data);

    // Set column widths
    const columnWidths = [
      { wch: 30 }, // Column A width
      { wch: 20 }, // Column B width
      { wch: 20 }, // Column C width
      { wch: 20 }, // Column D width
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

  // Data handling

  get fields() {
    return this.regUserForm.controls;
  }

  resetFields() {
    this.regUserForm.reset();
    this.isSuccessfulyDeleted = false;
    this.toggleConfirmationModal();
  }

  modifyRegUser() {
    this.isValidated = true;

    if (!this.allFieldsValidated()) {
      return;
    }

    this.modalText = 'Uspešno ste izmenili podatke o obvezniku';
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
        next: () => {
          console.debug('Uspešno promenjeni podaci o obvezniku');
          var currentPage = this.currentPage;
          this.regUserService.getRegUsers(RoleType.Obveznik).subscribe({
            next: (regUsers: RegUserDto[]) =>
              (this.regUsers =
                regUsers instanceof HttpErrorResponse ? [] : regUsers),
            complete: () => {
              this.totalPagesNumber =
                this.regUsers.length % this.itemsPerPage === 0
                  ? Math.trunc(this.regUsers.length / this.itemsPerPage)
                  : Math.trunc(this.regUsers.length / this.itemsPerPage) + 1;
              this.setPage(currentPage);
              this.currentPage = currentPage;
              // this.addRegUsers()
            },
          });

          this.toggleAddRegUsernModal();
          this.toggleConfirmationModal();
        },
        error: (error) => {
          console.debug('Neuspešno promenjeni podaci o obvezniku');
        },
      });
  }

  addRegUser() {
    this.isValidated = true;

    if (!this.allFieldsValidated()) {
      return;
    }

    this.modalText = 'Uspešno ste registrovali novog obveznika';
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
          console.debug('Uspešno kreiran obveznik');
          var currentPage = this.currentPage;
          this.regUserService.getRegUsers(RoleType.Obveznik).subscribe({
            next: (regUsers: RegUserDto[]) =>
              (this.regUsers =
                regUsers instanceof HttpErrorResponse ? [] : regUsers),
            complete: () => {
              this.totalPagesNumber =
                this.regUsers.length % this.itemsPerPage === 0
                  ? Math.trunc(this.regUsers.length / this.itemsPerPage)
                  : Math.trunc(this.regUsers.length / this.itemsPerPage) + 1;
              this.setPage(currentPage);
              this.currentPage = currentPage;
            },
          });

          this.toggleAddRegUsernModal();
          this.toggleConfirmationModal();
        },
        error: (error) => {
          console.debug('Neuspeašno kreiran obveznik', error);
        },
        complete: () => {
          // console.debug('Successfuly registered user complete callback');
        },
      });
  }

  // Modal handling

  toggleConfirmationModal() {
    this.isSuccessfulyRegisteredUserModalVisible =
      !this.isSuccessfulyRegisteredUserModalVisible;
  }

  toggleAddRegUsernModal() {
    this.isValidated = false;
    this.isAddReguserModalVisible = !this.isAddReguserModalVisible;
  }

  showAddUserModal() {
    this.modalAddEditUserTitle = 'Dodaj novog obveznika';
    this.modalAddEditUserConfirmButton = 'Dodaj obveznika';
    this.isEditing = false;
    this.regUserForm.reset();
    this.toggleAddRegUsernModal();
  }

  deleteUser(guidId: string) {
    this.modalText = 'Uspešno ste obrisali obveznika';
    this.regUserService.removeRegUser(guidId).subscribe({
      next: (regUser: RegUserDto) => {
        console.log('Uspešno obrisan obveznik');
        var currentPage = this.currentPage;
        this.regUserService.getRegUsers(RoleType.Obveznik).subscribe({
          next: (regUsers: RegUserDto[]) =>
            (this.regUsers =
              regUsers instanceof HttpErrorResponse ? [] : regUsers),
          complete: () => {
            this.totalPagesNumber =
              this.regUsers.length % this.itemsPerPage === 0
                ? Math.trunc(this.regUsers.length / this.itemsPerPage)
                : Math.trunc(this.regUsers.length / this.itemsPerPage) + 1;
            this.setPage(currentPage);
            this.currentPage = currentPage;
          },
        });

        this.isSuccessfulyDeleted = true;
        this.toggleConfirmationModal();
      },
      error: (error) => {
        console.log('Neuspešno obrisan obveznik');
      },
    });
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

  allFieldsValidated(): Boolean {
    if (
      this.fields['regUserName'].valid &&
      this.fields['regUserAddress'].valid &&
      this.fields['regUserMB'].valid &&
      this.fields['regUserPIB'].valid &&
      this.fields['regUserFirstName'].valid &&
      this.fields['regUserLastName'].valid &&
      this.fields['regUserUsername'].valid &&
      // this.fields['regUserPassword'].valid &&
      this.fields['regUserEmail'].valid
    ) {
      return true;
    } else {
      return false;
    }
  }
}
