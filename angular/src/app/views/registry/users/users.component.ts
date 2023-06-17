import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { UserService } from 'src/app/domain/services/user.service';
import { UserDto } from '../../../domain/model/schemas';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
})
export class UsersComponent implements OnInit {
  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder
    ) {

    }

  @Input() users: UserDto[] = [];
  phoneNumber: string[] = [''];
  operatorName: string[] = [''];
  firstName: string = '';
  lastName: string = '';
  jmbg: string = '';
  divs: number[] = [1];

  addNewUserForm!: FormGroup;

  createDiv() {
    if(this.divs.length < 5)
      this.divs.push(this.divs.length);
  }

  ngOnInit() {
    this.addNewUserForm = this.formBuilder.group({
      phoneNumber: this.formBuilder.array(this.divs, Validators.required),
      operatorName: this.formBuilder.array(this.divs, Validators.required),
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      jmbg: ['', Validators.required],
    });

    let after: Date = new Date();
    after.setMonth(3);

    this.userService.allUsers(after).subscribe({
      next: (users: UserDto[]) =>
        (this.users = users instanceof HttpErrorResponse ? [] : users),
      complete: () => this.completeAllUsers(),
    });
  }

  createPhoneNumbers(): any {
    throw new Error('Method not implemented.');
  }

  completeAllUsers() {
    if (this.users.length === 0) {
      console.log('nema korisnika');
    }
  }

  submitNewUser() {

  }

  cancelNewUser() {
    this.divs = [1];
  }
}
