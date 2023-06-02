import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { User } from '../model/user';

import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';

import { map, catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class NeZoviService {

    private url = 'https://localhost:7058';  // URL to web api
    public headers = new HttpHeaders().set('Content-Type', 'application/json');

    constructor(private http: HttpClient) { 

    }

    getAllUsers(): User[] {
        const auth_token ="JWT Bearer token";

        const url: string = this.url + "/v1/users/all";
        const headers: HttpHeaders = new HttpHeaders()
            .set('Content-Type', 'application/json')
            .set('Authorization', 'Bearer ${auth_token}');
            
        const httpParams: HttpParams = new HttpParams({
            fromObject: { after: "2023-05-23T21:31:37.150Z" }
        })

        const observe = "body";

        let data: User[] = [];

        this.http
            .post<User[]>(url, httpParams, { 
                    headers: headers,
                    observe: observe 
                })
            .forEach((users: User[]) => data = users);

        return data;
    }
}