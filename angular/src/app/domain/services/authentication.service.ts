import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { AppConfiguration } from './app-configuration.service';
import { RefreshTokenResultDto, TokenResult } from '../model/schemas';
import { catchError, map } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  constructor(
    private config: AppConfiguration,
    private router: Router,
    private http: HttpClient
  ) {}

  login(username: string, password: string) {
    return this.http
      .post<TokenResult>(`${this.config.apiUrl}${this.config.apiLoginUrl}`, {
        username,
        password,
      })
      .subscribe((token: TokenResult) => {
        AuthenticationService.Token = token;
        this.startRefreshTokenTimer();
        console.log('DONE');
      });
  }

  logout() {
    this.http
      .post<any>(`${this.config.apiUrl}${this.config.apiLogoutUrl}`, {})
      .subscribe();

    this.stopRefreshTokenTimer();
    AuthenticationService.Token = null;
    this.router.navigate([`${this.config.loginPage}`]);
  }

  refreshToken() {
    let token: TokenResult = AuthenticationService.Token;
    return this.http
      .post<RefreshTokenResultDto>(
        `${this.config.apiUrl}${this.config.apiRefreshTokenUrl}`,
        {
          accessToken: token.accessToken,
          refreshToken: token.refreshToken.tokenString,
        }
      )
      .subscribe((token: RefreshTokenResultDto) => {
        AuthenticationService.Token = {
          accessToken: token.accessToken,
          refreshToken: token.refreshToken,
        };
        this.startRefreshTokenTimer();
      });
  }

  // token store

  static _TOKEN_ITEM = '_TOKEN_ITEM';
  static set Token(token: TokenResult | null | string) {
    if (token)
      if (typeof token === 'string')
        localStorage.setItem(this._TOKEN_ITEM, token);
      else localStorage.setItem(this._TOKEN_ITEM, JSON.stringify(token));
    else localStorage.removeItem(this._TOKEN_ITEM);
  }

  static get Token(): TokenResult {
    return JSON.parse(localStorage.getItem(this._TOKEN_ITEM) ?? '{}');
  }

  // helper methods

  private refreshTokenTimeout: any;

  private startRefreshTokenTimer() {
    let tokens: TokenResult = AuthenticationService.Token;
    // set a timeout to refresh the token a minute before it expires
    const expires = new Date(tokens.refreshToken.expireAt);
    const timeout = expires.getTime() - Date.now() - 60 * 1000;
    this.refreshTokenTimeout = setTimeout(() => this.refreshToken(), timeout);
  }

  private stopRefreshTokenTimer() {
    clearTimeout(this.refreshTokenTimeout);
  }
}
