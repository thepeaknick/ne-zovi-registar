import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../model/user';

import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';

import { map, catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class NeZoviService {

    private url = 'https://localhost:7058';  // URL to web api
    headers = new HttpHeaders().set('Content-Type', 'application/json');

    constructor(private http: HttpClient) { 

    }

    getAllUsers(): Observable<User[]> {
        return this.http
            .get<User[]>(this.url + "/v1/users/all")
            .pipe(
                map((users) => { return users; }));
    }

}