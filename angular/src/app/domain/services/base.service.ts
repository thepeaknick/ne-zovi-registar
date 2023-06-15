import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { map, catchError, retry } from 'rxjs/operators';
import { LoginResultDto } from '../model/schemas';

@Injectable({
  providedIn: 'root',
})
export class BaseService {
  constructor(public http: HttpClient) {}

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

  request<T>(
    verb: string,
    url: string,
    body?: string,
    responseType?: 'json' | 'text' | 'blob' | 'arraybuffer',
    withCredentials?: boolean
  ): any {
    this.startCall();

    console.log(body);
    let response: Observable<string> = this.http.request(verb, url, {
      body: body,
      responseType: responseType,
      withCredentials: withCredentials,
    });

    console.log(body);

    this.finishCall();

    return response;
  }

  public getTextResponse<T>(url: string): any {
    return this.request<T>('GET', url, undefined, 'text');
  }

  public get<T>(url: string): any {
    return this.request<T>('GET', url, undefined, 'json');
  }

  public deleteWithTextResponse<T>(url: string): any {
    return this.request<T>('DELETE', url, undefined, 'text');
  }

  public delete<T>(url: string, data: Object): any {
    return this.request<T>('DELETE', url, JSON.stringify(data), 'json');
  }

  public post<T>(url: string, data: Object, withCredentials?: boolean): any {
    return this.request<T>('POST', url, JSON.stringify(data), 'json');
  }

  public patch<T>(url: string, data: Object): any {
    return this.request<T>('PATCH', url, JSON.stringify(data), 'json');
  }
}

function handleHttpError(error: any) {
  throw new Error('Function not implemented.');
}
