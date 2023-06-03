import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { map, catchError, retry } from 'rxjs/operators';
import { LoginResultDto } from '../model/schemas';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

    static auth_token: LoginResultDto = {
        accessToken: "",
        refreshToken: "" 
    };

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
                url, 
                { 
                    body: body,
                    responseType: responseType
                }
            );

        response.subscribe({ 
            next: result => { return result; },
            error: error => { handleHttpError(error) },
            complete: () => this.finishCall() 
        });

        return response;
    }


    public getTextResponse<T>(url: string) : any {
        return this.request<T>('GET', url, undefined, 'text');
    }

    public get<T>(url: string) : any {
        return this.request<T>('GET', url, undefined, 'json');
    }

    public deleteWithTextResponse<T>(url: string) : any {
        return this.request<T>('DELETE', url, undefined, 'text');
    }

    public delete<T>(url: string, data: Object) : any {
        return this.request<T>('DELETE', url, JSON.stringify(data), 'json');
    }

    public post<T>(url: string, data: Object) : any {
        return this.request<T>('POST', url, JSON.stringify(data), 'json');
    }

    public patch<T>(url: string, data: Object) : any {
        return this.request<T>('PATCH', url, JSON.stringify(data), 'json');
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

    // public get<T>(url: string) : any {
        
    //     this.startCall();

    //     let response: Observable<T> = this.http
    //         .get<T>(
    //             this.Url(url), 
    //             { 
    //                 headers: this.headers,
    //             });

    //     response.subscribe({ 
    //         next: result => { return result; },
    //         error: err => { return null; },
    //         complete: () => this.finishCall() 
    //     });
        
    //     return response;
    // }

    // public post<T>(
    //     url: string, 
    //     data: Object,
    //     next?: (r: T) => void, 
    //     error?: (r: T) => void) : Observable<T> {
        
    //     this.startCall();

    //     const httpParams: HttpParams = new HttpParams({
    //         fromString:  JSON.stringify(data)
    //     })

    //     let response: Observable<T> = this.http
    //         .post<T>(
    //             this.Url(url), 
    //             httpParams,
    //             { 
    //                 headers: this.headers,
    //             });

    //     response.subscribe({ 
    //         next: result => { return result; },
    //         error: err => { return null; },
    //         complete: () => this.finishCall() 
    //     });
        
    //     return response;
    // }
}

function handleHttpError(error: any) {
    throw new Error('Function not implemented.');
}
