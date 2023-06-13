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
  phoneNumber: string | null = null;
  divs: number[] = [1];

  addNewUserForm!: FormGroup;

  createDiv() {
    this.divs.push(this.divs.length);
  }

  ngOnInit() {
    this.addNewUserForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });

    let after: Date = new Date();
    after.setMonth(3);

    this.userService.allUsers(after).subscribe({
      next: (users: UserDto[]) =>
        (this.users = users instanceof HttpErrorResponse ? [] : users),
      complete: () => this.completeAllUsers(),
    });
  }

  completeAllUsers() {
    if (this.users.length === 0) {
      console.log('nema korisnika');
    }
  }

  submitNewUser()
  {

  }
}
