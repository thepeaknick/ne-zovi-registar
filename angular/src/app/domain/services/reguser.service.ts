import { HttpClient } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import {
  ChangeRegUserPasswordRequest,
  ModifyRegUserRequest,
  RegUserDetailsDto,
  RegUserDto,
  RegisterRegUserRequest,
  RoleType,
} from '../model/schemas';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root',
})
export class RegUserService extends BaseService {
  constructor(http: HttpClient) {
    super(http);
  }

  registerRegUser(request: RegisterRegUserRequest): Observable<RegUserDto> {
    return this.post<RegUserDto>('/regusers/register', request);
  }

  modifyRegUser(request: ModifyRegUserRequest): Observable<RegUserDto[]> {
    return this.patch<RegUserDto>('/regusers/modify', request);
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

  getRegUserData(guidId: string): Observable<RegUserDetailsDto> {
    return this.get<RegUserDetailsDto>(`/regusers/${guidId}`);
  }
}
