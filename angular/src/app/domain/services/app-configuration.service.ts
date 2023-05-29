import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
 
@Injectable()
export class AppConfiguration {
  constructor(private httpClient: HttpClient){

  }

  public apiUrl: string = '';

  ensureInit(): Promise<any> {
    return new Promise((resolve, reject) => {

      this.httpClient.get("./assets/config.json")
        .subscribe({
            next: (content: object) => {
                Object.assign(this, content);
                resolve(this);
            },
            error: (reason: any) => {
                reject(reason);
            }
        });
    });
  }
}