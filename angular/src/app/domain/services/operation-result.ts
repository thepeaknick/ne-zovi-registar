// import { HttpErrorResponse, HttpEvent, HttpResponse } from "@angular/common/http";

// export class OperationResult {
//     public success: boolean = false;

//     public data: any = null;
//     public event: HttpEvent<any> | undefined = undefined;

//     constructor(httpEvent: HttpEvent<any> | null) {
//         if(httpEvent !== null) {
//             this.event = httpEvent;

//             if(httpEvent instanceof HttpResponse) {
//                 this.success = true;
//                 this.data = JSON.parse(httpEvent.body);
//             }
//             else if(httpEvent instanceof HttpErrorResponse) {
//                 this.success = false;
//                 this.data = null;
//             }
//         }
//     }
// }