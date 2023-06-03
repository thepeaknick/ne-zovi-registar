
import {finalize, tap} from 'rxjs/operators';
import { Injectable, Inject } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpEvent, HttpHandler, HttpResponse, HttpResponseBase } from "@angular/common/http";
import { Observable } from "rxjs";
import { HttpErrorResponse } from "@angular/common/http";
import { Router } from "@angular/router";
import { LoginResultDto, TokenResult } from '../model/schemas';
import { AppConfiguration } from './app-configuration.service';

const _LOGIN_URL = "/login";
const _LOGIN_PAGE = "./login";

const _LOGIN = "/login";
const _LOGOUT = "/logout";

@Injectable()
export class NeZoviHttpInterceptor implements HttpInterceptor {

    constructor(private config: AppConfiguration, private router: Router) {
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const started = Date.now();
        console.debug("NeZoviHttpInterceptor: Intercepted " + request.url);
        
        let newUrl: string | undefined = undefined;
        if (!this.isAbsoluteUrl(request.url)) {
            newUrl = this.config.apiUrl + request.url;
        }

        //if we have a token - add it
        let setHeaders: { [name: string]: string | string[]; } | undefined = { };
        let tokenResult: TokenResult = NeZoviHttpInterceptor.tokenResult;
        if (tokenResult) {
            setHeaders = {
                Authorization: "Bearer " + tokenResult.accessToken
            };
        }

        setHeaders = {
            ...setHeaders,
            "Content-Type": "application/json"
        };

        let modifiedRequest: HttpRequest<any> = request.clone({
            setHeaders: setHeaders,
            url: newUrl,
        })

        //finally, perform the actual invoking of the http request
        return next
            .handle(modifiedRequest)
            .pipe(
                tap({
                    next : (event: HttpEvent<any>) => this.processOkResult(event),
                    error : (error: HttpEvent<any>) => this.processFailureResult(request, error)
                }),
                // Log when response observable either completes or errors
                finalize(() => {
                  const elapsed = Date.now() - started;
                })
            );
    }

    private processOkResult(event: HttpEvent<any>) : HttpEvent<any> {
        if (event instanceof HttpResponse) {
            if(event.status == 200) {
                if(event.url?.endsWith(_LOGIN)) {
                    NeZoviHttpInterceptor.tokenResult = event.body;
                }
                else if(event.url?.endsWith(_LOGOUT)) {
                    NeZoviHttpInterceptor.tokenResult = null;
                    this.router.navigate([_LOGIN_PAGE]);
                }
            }
        }

        return event;
    }

    private processFailureResult(request: HttpRequest<any>, error: HttpEvent<any>) : HttpEvent<any> {
        if (error instanceof HttpErrorResponse) {
            console.error("NeZoviHttpInterceptor: Received error from " + request.url + ":" + JSON.stringify(error));
            if (error.status === 401 && this.router.url !== _LOGIN_URL) {
                NeZoviHttpInterceptor.tokenResult = null;
                this.router.navigate([_LOGIN_PAGE]);
            }
            else if(error.status === 404)
            {
                return new HttpResponse<any>({
                    body: {},
                    headers: error.headers,
                    status: 200,
                    statusText: error.statusText,
                    url: error.url ?? ""
                });
            }
        }
        else {
            console.error("NeZoviHttpInterceptor: Received error: " + JSON.stringify(error));
        }

        return error;
    }

    private isAbsoluteUrl(urlString: string): boolean {
        return urlString.indexOf('http://') === 0 || urlString.indexOf('https://') === 0;
    }

    static _TOKEN_RESULT = "TokenResult";
    static set tokenResult(tokenResult: TokenResult | null | string) {
        if(tokenResult)
            if(typeof tokenResult === 'string')
                localStorage.setItem(this._TOKEN_RESULT, tokenResult);
            else
                localStorage.setItem(this._TOKEN_RESULT, JSON.stringify(tokenResult));
        else
            localStorage.removeItem(this._TOKEN_RESULT);
    }

    static get tokenResult() : TokenResult {
        return JSON.parse(localStorage.getItem("TokenResult") ?? "{}");
    }
}
