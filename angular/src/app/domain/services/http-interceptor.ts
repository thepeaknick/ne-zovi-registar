import { catchError, finalize, tap } from 'rxjs/operators';
import { Injectable, Inject } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpEvent,
  HttpHandler,
  HttpResponse,
  HttpResponseBase,
} from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import {
  LoginResultDto,
  RegUserDetailsDto,
  RoleType,
  TokenResult,
} from '../model/schemas';
import { AppConfiguration } from './app-configuration.service';
import { AuthenticationService } from './authentication.service';

@Injectable()
export class NeZoviHttpInterceptor implements HttpInterceptor {
  constructor(
    private config: AppConfiguration,
    private router: Router,
    private authenticationService: AuthenticationService
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const started = Date.now();
    console.debug('NeZoviHttpInterceptor: Intercepted ' + request.url);

    let newUrl: string | undefined = undefined;
    if (!this.isAbsoluteUrl(request.url)) {
      newUrl = this.config.apiUrl + request.url;
    }

    //if we have a token - add it
    let setHeaders: { [name: string]: string | string[] } | undefined = {};
    let tokenResult: LoginResultDto = AuthenticationService.Token;
    if (tokenResult) {
      setHeaders = {
        Authorization: 'Bearer ' + tokenResult.accessToken,
      };
    }

    setHeaders = {
      ...setHeaders,
      'Content-Type': 'application/json',
    };

    let modifiedRequest: HttpRequest<any> = request.clone({
      setHeaders: setHeaders,
      url: newUrl,
    });

    console.log('123');
    //finally, perform the actual invoking of the http request
    return next.handle(modifiedRequest).pipe(
      tap({ next: (event: HttpEvent<any>) => this.processOkResult(event) }),
      catchError((error: any) => {
        return this.processFailureResult(request, error);
      }),
      finalize(() => {
        const elapsed = Date.now() - started;
        console.log('123');
      })
    );
  }

  private processOkResult(event: HttpEvent<any>): HttpEvent<any> {
    if (event instanceof HttpResponse) {
      if (event.status == 200) {
        console.log('1234');
      }
    }

    return event;
  }

  private processFailureResult(
    request: HttpRequest<any>,
    error: HttpEvent<any>
  ): Observable<HttpEvent<any>> {
    if (error instanceof HttpErrorResponse) {
      console.error(
        'NeZoviHttpInterceptor: Received error from ' +
          request.url +
          ':' +
          JSON.stringify(error)
      );

      // TEMP workaround
      console.debug(request.url);

      if (request.url.includes('/regusers/login')) {
        if (error.status == 400) {
          return throwError(() => error.error);
        }
      }

      // TEMP workaround END

      if (
        (error.status === 401 || error.status === 403) &&
        !this.isLoginPageUrl()
      ) {
        this.authenticationService.logout();
      } else if (error.status === 404) {
        return of(
          new HttpResponse<any>({
            body: error,
            status: 204, // no content
            statusText: 'OK',
          })
        );
      }

      // return throwError(() => error);
      return throwError(() => error);
      // return of(error);
    }

    console.error(
      'NeZoviHttpInterceptor: Received error: ' + JSON.stringify(error)
    );
    return of(
      new HttpResponse<any>({
        body: error,
        status: 0,
      })
    );
  }

  private isAbsoluteUrl(urlString: string): boolean {
    return (
      urlString.indexOf('http://') === 0 || urlString.indexOf('https://') === 0
    );
  }

  private isLoginPageUrl(): boolean {
    return this.router.url.endsWith(
      this.config.loginPage.substring(this.config.loginPage.lastIndexOf('/'))
    );
  }
}
