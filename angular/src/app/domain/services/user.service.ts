import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';

import { map, catchError, retry } from 'rxjs/operators';
import { UserDto } from '../model/schemas';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {

    constructor(http: HttpClient) { 
        super(http);
        this.path = 'users';
    }

    getUser(phoneNumber: string): Observable<string> {
        return this.getText<string>(phoneNumber);
    }


    all(after: Date): Observable<UserDto[]> {
    
        let params = {after: "2023-05-27T21:20:10.780Z"};

        return this
            .post<UserDto[]>(
                'all',
                params); //{ after : after.toUTCString() }); 
    }

}