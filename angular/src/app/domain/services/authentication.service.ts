import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { AppConfiguration } from './app-configuration.service';
import {
  LoginResultDto,
  RefreshTokenResultDto,
  RegUserDetailsDto,
  TokenResult,
} from '../model/schemas';
import { Observable, map, mergeMap } from 'rxjs';
import { BaseService } from './base.service';

@Injectable({ providedIn: 'root' })
export class AuthenticationService extends BaseService {
  constructor(
    private config: AppConfiguration,
    private router: Router,
    http: HttpClient
  ) {
    super(http);
  }

  login(username: string, password: string) {
    return this.http
      .post<LoginResultDto>(`${this.config.apiUrl}${this.config.apiLoginUrl}`, {
        username,
        password,
      })
      .pipe(
        mergeMap((loginResult: LoginResultDto) => {
          // set token don't bother with user
          AuthenticationService.Token = loginResult;
          this.startRefreshTokenTimer();
          return this.http
            .get<RegUserDetailsDto>(
              `${this.config.apiUrl}${this.config.apiRegUserDetailsUrl}/${loginResult.regUserId}`
            )
            .pipe(
              map((regUserDetail: RegUserDetailsDto) => {
                // set user
                if (regUserDetail !== undefined) {
                  AuthenticationService.CurrentUser = regUserDetail;
                  AuthenticationService.CurrentUserName = username;
                }
                return regUserDetail;
              })
            );
        })
      );
  }

  logout(): Observable<void> {
    return this.http
      .post<any>(`${this.config.apiUrl}${this.config.apiLogoutUrl}`, {})
      .pipe(
        map(() => {
          this.stopRefreshTokenTimer();
          AuthenticationService.Token = null;
          AuthenticationService.CurrentUser = null;
          this.router.navigate([`${this.config.loginPage}`]);
        })
      );
  }

  refreshToken(): Observable<void> {
    let token: LoginResultDto = AuthenticationService.Token;
    return this.http
      .post<RefreshTokenResultDto>(
        `${this.config.apiUrl}${this.config.apiRefreshTokenUrl}`,
        {
          accessToken: token.accessToken,
          refreshToken: token.refreshToken,
        }
      )
      .pipe(
        map((newToken: RefreshTokenResultDto) => {
          token.accessToken = newToken.accessToken;
          token.accessTokenExpTime = newToken.accessTokenExpTime;
          token.refreshToken = newToken.refreshToken;
          token.refreshTokenExpTime = newToken.refreshTokenExpTime;

          AuthenticationService.Token = token;
          this.startRefreshTokenTimer();
        })
      );
  }

  forgotPasswordSendEMail(email: string) {
    this.getTextResponse(
      `${this.config.apiUrl}${this.config.apiForgotPasswordUrl}/${email}`
    ).subscribe((token: any) => {
      if (token) {
        // display message OK
        this.router.navigate(['/']);
      } else {
        // display error message
      }
    });
  }

  forgotPasswordResetPassword(email: string, token: string, password: string) {
    return this.http.post<boolean>(
      `${this.config.apiUrl}${this.config.apiForgotPasswordUrl}`,
      {
        email,
        token,
        password,
      }
    );
  }

  resetPassword(password: string, newPassword: string) {
    return this.http.post<boolean>(
      `${this.config.apiUrl}${this.config.apiResetPasswordUrl}`,
      {
        password,
        newPassword,
      }
    );
  }

  // token store

  static _REGUSER_ITEM = '_REGUSER_ITEM';
  static _TOKEN_ITEM = '_TOKEN_ITEM';
  static _USERNAME = '_USERNAME';

  static set Token(token: LoginResultDto | null | string) {
    if (token)
      if (typeof token === 'string')
        localStorage.setItem(this._TOKEN_ITEM, token);
      else localStorage.setItem(this._TOKEN_ITEM, JSON.stringify(token));
    else localStorage.removeItem(this._TOKEN_ITEM);
  }

  static get Token(): LoginResultDto {
    let item = localStorage.getItem(this._TOKEN_ITEM);
    return item ? JSON.parse(item) : null;
  }

  static set CurrentUserName(currentUsername: string) {
    if (currentUsername)
      if (typeof currentUsername === 'string')
        localStorage.setItem(this._USERNAME, currentUsername);
      else
        localStorage.setItem(this._USERNAME, JSON.stringify(currentUsername));
    else localStorage.removeItem(this._USERNAME);
  }

  static get CurrentUserName(): string {
    let item = localStorage.getItem(this._USERNAME);
    return item ? item : '';
  }

  static set CurrentUser(regUserDetail: RegUserDetailsDto | null | string) {
    if (regUserDetail)
      if (typeof regUserDetail === 'string')
        localStorage.setItem(this._REGUSER_ITEM, regUserDetail);
      else
        localStorage.setItem(this._REGUSER_ITEM, JSON.stringify(regUserDetail));
    else localStorage.removeItem(this._REGUSER_ITEM);
  }

  static get CurrentUser(): RegUserDetailsDto | null {
    let item = localStorage.getItem(this._REGUSER_ITEM);
    return item ? JSON.parse(item) : null;
  }

  get CurrentUser(): RegUserDetailsDto | null {
    let item = localStorage.getItem(AuthenticationService._REGUSER_ITEM);
    return item ? JSON.parse(item) : null;
  }

  // helper methods
  private refreshTokenTimeout: any;

  private startRefreshTokenTimer() {
    let tokens: LoginResultDto = AuthenticationService.Token;
    // set a timeout to refresh the token a minute before it expires
    const expires = new Date(tokens.accessTokenExpTime);
    const timeout = expires.getTime() - Date.now() - 60 * 1000;
    this.refreshTokenTimeout = setTimeout(() => this.refreshToken(), timeout);
  }

  private stopRefreshTokenTimer() {
    clearTimeout(this.refreshTokenTimeout);
  }

  waitForCondition(ms: number, condition: Function) {
    const date = Date.now();
    let currentDate = null;
    do {
      currentDate = Date.now();
    } while (currentDate - date < ms || condition());
  }
}
