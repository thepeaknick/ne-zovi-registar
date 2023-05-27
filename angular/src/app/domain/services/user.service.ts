import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';

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

    getUser(phoneNumber: string): string | null {
        let resultPhoneNumber : string | null = "";
        
        this
            .get<string>(this.Url(phoneNumber))
            .subscribe({
                next: (response: string | null) => { 
                    resultPhoneNumber = response; 
                },
                error: (error: any) => {
                    resultPhoneNumber = null;
                }
            });

        return resultPhoneNumber;
    }

    // all(after: Date): Observable<UserDto[]> {
    //     const httpParams: HttpParams = new HttpParams({
    //         fromObject: {
    //             after : after.toUTCString()
    //         }
    //     })

    //     return this.http
    //         .post<UserDto[]>(
    //             this.Url('all'), 
    //             httpParams, 
    //             { 
    //                 headers: this.headers,
    //                 observe: 'body' 
    //             });
    // }

    all(after: Date): Observable<UserDto[]> {
    
        let params = {after: "2023-05-27T21:20:10.780Z"};

        return this
            .post<UserDto[]>(
                'all',
                params); //{ after : after.toUTCString() }); 
    }

}