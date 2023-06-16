import { HttpClient } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { AddUserRequest, ModifyUserRequest, UserDto } from '../model/schemas';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root',
})
export class UserService extends BaseService {
  constructor(http: HttpClient) {
    super(http);
  }

  addUser(addUserRequest: AddUserRequest): Observable<UserDto> {
    return this.post<string>('/users/add', addUserRequest);
  }

  modifyUser(
    phoneNumber: string,
    modifyUserRequest: ModifyUserRequest
  ): Observable<UserDto> {
    return this.patch<string>('/users/' + phoneNumber, modifyUserRequest);
  }

  removeUser(phoneNumber: string): Observable<string> {
    return this.deleteWithTextResponse<string>('/users/' + phoneNumber);
  }

  getUser(phoneNumber: string): Observable<string> {
    return this.getTextResponse<string>('/users/' + phoneNumber);
  }

  allUsers(after: Date): Observable<UserDto[]> {
    return this.get<UserDto[]>('/users/all?after=' + after);
  }
}
