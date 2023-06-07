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
import { Observable, of } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { LoginResultDto, TokenResult } from '../model/schemas';
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
    let tokenResult: TokenResult = AuthenticationService.Token;
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

    //finally, perform the actual invoking of the http request
    return next.handle(modifiedRequest).pipe(
      tap({ next: (event: HttpEvent<any>) => this.processOkResult(event) }),
      catchError((error: any) => this.processFailureResult(request, error)),
      finalize(() => {
        const elapsed = Date.now() - started;
      })
    );
  }

  private processOkResult(event: HttpEvent<any>): HttpEvent<any> {
    if (event instanceof HttpResponse) {
      if (event.status == 200) {
      }
    }

    return event;
  }

  private processFailureResult(
    request: HttpRequest<any>,
    error: HttpEvent<any>
  ): Observable<HttpEvent<any>> {
    if (error instanceof HttpErrorResponse) {
      if (
        //TODO: Bad solution, but inevitable
        (error as unknown as HttpErrorResponse).error.includes('ne postoji')
      ) {
        return of(
          new HttpResponse<any>({
            body: '',
            status: 204, // no content
            statusText: 'OK',
          })
        );
      }

      console.error(
        'NeZoviHttpInterceptor: Received error from ' +
          request.url +
          ':' +
          JSON.stringify(error)
      );
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

      return of(error);
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
