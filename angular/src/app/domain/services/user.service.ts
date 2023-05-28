import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';

import { map, catchError, retry } from 'rxjs/operators';
import { AddUserRequest, ModifyUserRequest, UserDto } from '../model/schemas';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {

    constructor(http: HttpClient) { 
        super(http);
        this.path = 'users';
    }

    addUser(addUserRequest: AddUserRequest): Observable<UserDto> {
        return this.post<string>('add', addUserRequest);
    }

    modifyUser(phoneNumber: string, modifyUserRequest: ModifyUserRequest): Observable<UserDto> {
        return this.patch<string>(phoneNumber, modifyUserRequest);
    }

    removeUser(phoneNumber: string): Observable<string> {
        return this.deleteText<string>(phoneNumber);
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