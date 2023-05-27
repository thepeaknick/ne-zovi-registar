import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';

import { map, catchError, retry } from 'rxjs/operators';
import { RegUserDto, RoleType, UserDto } from '../model/schemas';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class RegUserService extends BaseService {

    constructor(http: HttpClient) { 
        super(http);
        this.path = 'regusers';
    }

    roles(role: RoleType): RegUserDto[] {
        const httpParams: HttpParams = new HttpParams({
            fromObject: {
                role : role
            }
        })

        const observe = "body";

        let data: RegUserDto[] = [];

        this.http
            .post<RegUserDto[]>(this.Url('role'), httpParams, { 
                    headers: this.headers,
                    observe: observe 
                })
            .subscribe((regUsers: RegUserDto[]) => data = regUsers);

        return data;
    }
}