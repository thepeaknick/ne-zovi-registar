
import {tap} from 'rxjs/operators';
import { Injectable, Inject } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpEvent, HttpHandler, HttpResponse } from "@angular/common/http";
import { Observable } from "rxjs";
import { HttpErrorResponse } from "@angular/common/http";
import { Router } from "@angular/router";
import { LoginResultDto, TokenResult } from '../model/schemas';
import { AppConfiguration } from './app-configuration.service';


@Injectable()
export class NeZoviHttpInterceptor implements HttpInterceptor {
    
    static auth_token: TokenResult = {
        accessToken: "",
        refreshToken: {
            tokenString: "",
            expireAt: ""  
        } 
    };

    constructor(private config: AppConfiguration, private router: Router) {
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        console.debug("NeZoviHttpInterceptor: Intercepted " + request.url);
        
        let newUrl: string | undefined = undefined;
        if (!this.isAbsoluteUrl(request.url)) {
            newUrl = this.config.apiUrl + request.url;
        }

        //if we have a token - add it
        let setHeaders: { [name: string]: string | string[]; } | undefined = undefined;
        if (NeZoviHttpInterceptor.auth_token.accessToken) {
            setHeaders = {
                Authorization: "Bearer " + NeZoviHttpInterceptor.auth_token.accessToken
            };
        }

        let modifiedRequest: HttpRequest<any> = request.clone({
            setHeaders: setHeaders,
            url: newUrl,
        })

        //finally, perform the actual invoking of the http request
        return next
            .handle(modifiedRequest)
            .pipe(
                tap({
                    next : (event: HttpEvent<any>) => {
                        if (event instanceof HttpResponse) {
                            return event;
                        }
                        return event;
                    },
                    error : (error: any) => {
                        if (error instanceof HttpErrorResponse) {
                            console.error("NeZoviHttpInterceptor: Received error from " + request.url + ":" + JSON.stringify(error));
                            if (error.status === 401 && this.router.url !== '/login') {
                                NeZoviHttpInterceptor.auth_token.accessToken = "";
                                this.router.navigate(['./login']);
                            }
                        }
                        else {
                            console.error("NeZoviHttpInterceptor: Received error: " + JSON.stringify(error));
                        }
                    }}
                )
            );
    }

    private isAbsoluteUrl(urlString: string): boolean {
        return urlString.indexOf('http://') === 0 || urlString.indexOf('https://') === 0;
    }
}
