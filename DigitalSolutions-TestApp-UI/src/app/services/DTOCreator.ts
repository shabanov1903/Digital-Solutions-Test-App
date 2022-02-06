import { Injectable } from "@angular/core";

@Injectable()
export class DTOCreator
{
    static returnAccountStatus(value:string) : string {
        var result : string = "";
        switch (+value) {
            case 1: {
                result = "Новый";
                break;
            }
            case 2: {
                result = "Оплачен";
                break;
            }
            case 3: {
                result = "Отменён";
                break;
            }
        }
        return result;
    }
    static returnPaymentMethod(value:string) : string {
        var result : string = "";
        switch (+value) {
            case 1: {
                result = "Кредитная карта";
                break;
            }
            case 2: {
                result = "Дебетовая карта";
                break;
            }
            case 3: {
                result = "Электронный чек";
                break;
            }
        }
        return result;
    }
    static convertAccountStatus(value:string) : number {
        var result : number = 0;
        switch (value) {
            case "Новый": {
                result = 1;
                break;
            }
            case "Оплачен": {
                result = 2;
                break;
            }
            case "Отменён": {
                result = 3;
                break;
            }
        }
        return result;
    }
    static convertPaymentMethod(value:string) : number {
        var result : number = 0;
        switch (value) {
            case "Кредитная карта": {
                result = 1;
                break;
            }
            case "Дебетовая карта": {
                result = 2;
                break;
            }
            case "Электронный чек": {
                result = 3;
                break;
            }
        }
        return result;
    }
    static convertDateTime(value:string) : number {
        value = value.replaceAll("-", "");
        value = value.replaceAll(" ", "");
        value = value.replaceAll(":", "");
        return +value;
    }
}