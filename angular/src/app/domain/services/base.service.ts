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


    public get isCallInProgress() { 
        return this.callInProgress > 0; 
    }

    private callInProgress: number = 0;

    startCall() {
        this.callInProgress++;
    }

    finishCall() {
        this.callInProgress--;
    }

    request<T>(verb: string, url: string, body?: string, responseType?: 'json' | 'text' | 'blob' | 'arraybuffer') : any {
        
        this.startCall();

        let response: Observable<string> = this.http
            .request(
                verb, 
                this.Url(url), 
                { 
                    headers: this.headers, 
                    responseType: responseType
                }
            );

        response.subscribe({ 
            next: result => { return result; },
            error: err => { return null; },
            complete: () => this.finishCall() 
        });

        return response;
    }


    public getText<T>(url: string) : any {
        return this.request<T>('GET', url);
    }


    // public getText<T>(url: string) : any {
        
    //     this.startCall();

    //     let response: Observable<string> = this.http
    //         .request(
    //             'GET', 
    //             this.Url(url), 
    //             { 
    //                 headers: this.headers, 
    //                 responseType: "text"
    //             }
    //         );

    //     response.subscribe({ 
    //         next: result => { return result; },
    //         error: err => { return null; },
    //         complete: () => this.finishCall() 
    //     });

    //     return response;
    // }

    public get<T>(url: string) : any {
        
        this.startCall();

        let response: Observable<T> = this.http
            .get<T>(
                this.Url(url), 
                { 
                    headers: this.headers,
                });

        response.subscribe({ 
            next: result => { return result; },
            error: err => { return null; },
            complete: () => this.finishCall() 
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

        let response: Observable<T> = this.http
            .post<T>(
                this.Url(url), 
                httpParams,
                { 
                    headers: this.headers,
                });

        response.subscribe({ 
            next: result => { return result; },
            error: err => { return null; },
            complete: () => this.finishCall() 
        });
        
        return response;
    }
}