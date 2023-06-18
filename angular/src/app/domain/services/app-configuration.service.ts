import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AppConfiguration {
  constructor(private http: HttpClient) {}

  public apiUrl: string = '';
  public apiLoginUrl: string = '';
  public apiLogoutUrl: string = '';
  public apiResetPasswordUrl: string = '';
  public apiForgotPasswordUrl: string = '';
  public apiRegUserDetailsUrl: string = '';
  public apiRefreshTokenUrl: string = '';
  public apiRegisterRegUser: string = '';
  public loginPage: string = '';

  ensureInit(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.get('./assets/config.json').subscribe({
        next: (content: object) => {
          Object.assign(this, content);
          resolve(this);
        },
        error: (reason: any) => {
          reject(reason);
        },
      });
    });
  }
}
