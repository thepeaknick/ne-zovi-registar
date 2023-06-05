import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class TokenService extends BaseService {

    constructor(http: HttpClient, private authenticationService: AuthenticationService) { 
        super(http)
    }

    public refreshToken() : void   {
        this.authenticationService.refreshToken();
    }
}