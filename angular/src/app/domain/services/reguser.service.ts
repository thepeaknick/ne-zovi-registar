import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';

import { map, catchError, retry, tap } from 'rxjs/operators';
import { ChangeRegUserPasswordRequest, LoginRequest, ModifyRegUserRequest, RegUserDto, RegisterRegUserRequest, RoleType, TokenResult, UserDto } from '../model/schemas';
import { BaseService } from './base.service';
import { Token } from '@angular/compiler';
import { NeZoviHttpInterceptor } from './http-interceptor';

@Injectable({
  providedIn: 'root'
})
export class RegUserService extends BaseService {

    constructor(http: HttpClient) { 
        super(http);
    }

    loginRegUser(request: LoginRequest): Observable<TokenResult> {
        return this
            .post<TokenResult>('/regusers/login', { request: request })
            .pipe(
                tap((result: TokenResult) => NeZoviHttpInterceptor.auth_token = result)
            );
    }

    registerRegUser(request: RegisterRegUserRequest): Observable<RegUserDto[]> {
        return this
            .post<RegUserDto>('/regusers/register', { request: request });
    }

    modifyRegUser(request: ModifyRegUserRequest): Observable<RegUserDto[]> {
        return this
            .patch<RegUserDto>('/regusers/modify', { request: request });
    }

    modifyRegUserByGuid(regUserId: string, request: ModifyRegUserRequest): Observable<RegUserDto[]> {
        return this
            .patch<RegUserDto>(`/regusers/${regUserId}`, { request: request });
    }

    removeRegUser(regUserId: string): Observable<RegUserDto> {
        return this
            .deleteWithTextResponse<RegUserDto>(`/regusers/${regUserId}`);
    }

    changeRegUserPassword(request: ChangeRegUserPasswordRequest): Observable<RegUserDto[]> {
        return this
            .patch<RegUserDto>('/regusers/forgotpassword', { request: request });
    }

    getRegUsers(role: RoleType): Observable<RegUserDto[]> {
        return this
            .get<RegUserDto[]>(`/regusers/role/${role}`);
    }
}