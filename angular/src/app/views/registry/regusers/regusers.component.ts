import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
      complete: () => {},
    });

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
  }

  get fields() {
    return this.regUserForm.controls;
  }

  addRegUsers() {
    this.regUserService.registerRegUser({
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
    });
  }
}
