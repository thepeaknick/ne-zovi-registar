import { HttpClient } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import {
  ChangeRegUserPasswordRequest,
  ContactEmailRequest,
  ModifyRegUserRequest,
  RegUserDetailsDto,
  RegUserAPRDetailsDto,
  RegUserDto,
  RegisterRegUserRequest,
  RoleType,
  RegUserAccountDto,
  RegUserAccountArrayDto,
} from '../model/schemas';
import { BaseService } from './base.service';
import { AppConfiguration } from './app-configuration.service';

@Injectable({
  providedIn: 'root',
})
export class RegUserService extends BaseService {
  constructor(http: HttpClient, private config: AppConfiguration) {
    super(http);
  }

  registerRegUser(request: RegisterRegUserRequest): Observable<RegUserDto> {
    return this.post<RegUserDto>('/regusers/register', request);
  }

  registerRegUserWithoutAuth(
    request: RegisterRegUserRequest
  ): Observable<RegUserDto> {
    return this.post<RegUserDto>('/regusers/register-trader', request);
  }

  modifyRegUser(request: ModifyRegUserRequest): Observable<RegUserDto[]> {
    return this.patch<RegUserDto>('/regusers/modify', request);
  }

  fetchCompanyData(regNumber: String): Observable<RegUserAPRDetailsDto> {
    return this.get<RegUserAPRDetailsDto>(`/regusers/apr/${regNumber}`);
  }

  modifyRegUserByGuid(
    regUserId: string,
    request: ModifyRegUserRequest
  ): Observable<RegUserDto[]> {
    return this.patch<RegUserDto>(`/regusers/${regUserId}`, request);
  }

  removeRegUser(regUserId: string): Observable<RegUserDto> {
    return this.deleteWithTextResponse<RegUserDto>(`/regusers/${regUserId}`);
  }

  changeRegUserPassword(
    request: ChangeRegUserPasswordRequest
  ): Observable<RegUserDto[]> {
    return this.patch<RegUserDto>('/regusers/forgotpassword', request);
  }

  getRegUsers(role: RoleType): Observable<RegUserDto[]> {
    return this.get<RegUserDto[]>(`/regusers/roles/${role}`);
  }

  getRegUserAccounts(userId: string): Observable<RegUserAccountArrayDto> {
    return this.get<RegUserAccountArrayDto>(`/regusers/accounts/${userId}`);
  }

  saveRegUserAccounts(
    userId: string,
    userAccounts: RegUserAccountArrayDto // TODO change any to type
  ): Observable<string> {
    return this.patch<RegUserAccountArrayDto>(
      `/regusers/accounts/${userId}`,
      userAccounts
    );
  }

  getRegUserData(guidId: string): Observable<RegUserDetailsDto> {
    return this.get<RegUserDetailsDto>(`/regusers/${guidId}`);
  }

  sendEmail(request: ContactEmailRequest): Observable<string> {
    return this.postTextResponseWithBody<string>(
      `${this.config.apiUrl}${this.config.apiSendEmailUrl}`,
      request
    );
  }
}
