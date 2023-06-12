import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
  HttpParams,
  HttpResponse,
} from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';

import { map, catchError, retry, tap } from 'rxjs/operators';
import {
  ChangeRegUserPasswordRequest,
  LoginRequest,
  ModifyRegUserRequest,
  RegUserDto,
  RegisterRegUserRequest,
  RoleType,
  TokenResult,
  UserDto,
} from '../model/schemas';
import { BaseService } from './base.service';
import { Token } from '@angular/compiler';
import { NeZoviHttpInterceptor } from './http-interceptor';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root',
})
export class RegUserService extends BaseService {
  constructor(
    http: HttpClient,
    private authenticationService: AuthenticationService
  ) {
    super(http);
  }

  registerRegUser(request: RegisterRegUserRequest): Observable<RegUserDto[]> {
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
}
