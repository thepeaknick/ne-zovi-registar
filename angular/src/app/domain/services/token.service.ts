import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { map, catchError, retry } from 'rxjs/operators';
import { LoginResultDto, RefreshToken } from '../model/schemas';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class TokenService extends BaseService {

    constructor(http: HttpClient) { 
        super(http)
    }

    public refreshToken(username: string, password: string) : Observable<RefreshToken>  {
        const url: string = "refresh-token";
        return this.http.get<RefreshToken>(this.baseUrl + url);
    }

}