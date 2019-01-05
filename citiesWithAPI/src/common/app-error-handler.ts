import { ErrorHandler } from "@angular/core";

export class AppErrorHandler implements ErrorHandler{
    handleError(error){
        alert("An exception error occurred.");
        console.log(error);
    }
}