import { HttpEvent, HttpResponse } from "@angular/common/http";

export class OperationResult {
    public success: boolean = false;
    public data: any = null;
    public rawData: any = null;
    public message: string = "";
    public errorMessage: string = "";

    constructor(){
    }

    public createFromResponse(httpEvent: HttpEvent<any> | null) : OperationResult {
        if(httpEvent === null) {
            return new OperationResult;
        }

        if(httpEvent instanceof HttpResponse) {
            let httpResponse = httpEvent as HttpResponse<any>;

            this.success = true;
            this.rawData = httpEvent;
            this.data = httpResponse;
            this.errorMessage = "";
            this.message = "";
            

        }

    }
}