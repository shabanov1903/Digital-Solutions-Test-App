import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AccountDTO } from './AccountDTO';
import { FilterDTO } from './FilterDTO';
import { OperationDTO } from './OperationDTO';

@Injectable()
export class HttpService{
    
    resourceUrl = "http://localhost:5000/account/";
    constructor(private http: HttpClient){ }

    GetAccountById(id: number){
        return this.http
                    .post<AccountDTO>(
                        this.resourceUrl + "get/" + id.toString,
                        { }
                    )
    }

    GetAccountsByFilter(filter: FilterDTO){
        return this.http
                    .post<AccountDTO[]>(
                        this.resourceUrl + "get/filter",
                        {
                            minCreateTime : filter.minCreateTime,
                            maxCreateTime : filter.maxCreateTime,
                            minChangeTime : filter.minChangeTime,
                            maxChangeTime : filter.maxChangeTime,
                            minAccountNumber : filter.minAccountNumber.toString(),
                            maxAccountNumber : filter.maxAccountNumber.toString(),
                            accountStatus : filter.accountStatus.toString(),
                            minBalance : filter.minBalance.toString(),
                            maxBalance : filter.maxBalance.toString(),
                            paymentMethod : filter.paymentMethod.toString()
                        }
                    )
    }

    AddAccount(account: AccountDTO){
        return this.http
                    .post<OperationDTO>(
                        this.resourceUrl + "add",
                        {
                            createTime : account.createTime,
                            changeTime : account.changeTime,
                            accountNumber : account.accountNumber.toString(),
                            accountStatus : account.accountStatus.toString(),
                            balance : account.balance.toString(),
                            paymentMethod : account.paymentMethod.toString()
                        }
                    )
    }

    ChangeAccount(account: AccountDTO){
        return this.http
                    .post(
                        this.resourceUrl + "change",
                        {
                            createTime : account.createTime,
                            changeTime : account.changeTime,
                            accountNumber : account.accountNumber,
                            accountStatus : account.accountStatus,
                            balance : account.balance,
                            paymentMethod : account.paymentMethod
                        }
                    )
    }
}
