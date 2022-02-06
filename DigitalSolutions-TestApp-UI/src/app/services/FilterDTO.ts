import { Injectable } from "@angular/core";

@Injectable()
export class FilterDTO
{
    minCreateTime : string = "2000-01-01T00:00:00.0000000";
    maxCreateTime : string = "2100-01-01T00:00:00.0000000";
    minChangeTime : string = "2000-01-01T00:00:00.0000000";
    maxChangeTime : string = "2100-01-01T00:00:00.0000000";
    minAccountNumber : number = 0;
    maxAccountNumber : number = 9999;
    accountStatus : string = "0";
    minBalance : number = 0;
    maxBalance : number = 9999;
    paymentMethod : string = "0";
}