import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { map, catchError, retry } from 'rxjs/operators';
import { LoginResultDto } from '../model/schemas';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

    public baseUrl: string = 'https://localhost:7058';  
    public version: string = "v1";
    public path: string | null = null;

    static auth_token: LoginResultDto = {
        accessToken: "",
        refreshToken: "" 
    };

    public get headers(): HttpHeaders {
        return new HttpHeaders()
        .set('Content-T', 'application/json')
        .set('Authorization', `Bearer ${BaseService.auth_token.accessToken}`);
    }

    public Url(url: string): string { 
        if(this.path)
            return `${this.baseUrl}/${this.version}/${this.path}/${url}`;
        else
            return `${this.baseUrl}/${this.version}/${url}`;
    }

    constructor(public http: HttpClient) { 
    }

    private callInProgress: number = 0;
    public get isCallInProgress() { 
        return this.callInProgress > 0; 
    }

    startCall() {
        this.callInProgress++;
    }

    finishCall() {
        this.callInProgress--;
    }

    public get<T>(
        url: string, 
        next?: (r: T) => void, 
        error?: (r: T) => void) : any {
        
        this.startCall();

        let response: Observable<T> | null = null;


        let headers: HttpHeaders  = new HttpHeaders() 
            .set('Content-T', 'text/plain')
            .set('Authorization', `Bearer ${BaseService.auth_token.accessToken}`);

        response = this.http
            .get<T>(
                url, 
                { 
                    headers: headers,
                    observe: 'body' 
                });

        response.subscribe({
            next: (r : T) => 
            {
                if(next)
                    next(r);
            },
            error: (r: T) =>
            {
                if(error)
                    error(r);
                response = null;
            },
            complete: () => {
                this.finishCall();
            }
        });

        return response;
    }

    public post<T>(
        url: string, 
        data: Object,
        next?: (r: T) => void, 
        error?: (r: T) => void) : Observable<T> {
        
        this.startCall();

        const httpParams: HttpParams = new HttpParams({
            fromString:  JSON.stringify(data)
        })

        let response: Observable<T> | null = null;

        response = this.http
            .post<T>(
                this.Url(url), 
                httpParams,
                { 
                    headers: this.headers,
                    observe: 'body' 
                });

        response.subscribe({
            next: (r : T) => 
            {
                if(next)
                    next(r);
            },
            error: (r: T) => 
            {
                if(error)
                    error(r);
            },
            complete: () => {
                this.finishCall();
            }
        });

        return response;
    }

}