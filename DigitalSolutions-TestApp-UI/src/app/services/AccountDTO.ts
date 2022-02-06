import { Injectable } from "@angular/core";

@Injectable()
export class AccountDTO
{
    createTime : string = "2000-01-01T00:00:00.0000000";
    changeTime : string = "2000-01-01T00:00:00.0000000";
    accountNumber : number = 0;
    accountStatus : string = "0";
    balance : number = 0;
    paymentMethod : string = "0";
}