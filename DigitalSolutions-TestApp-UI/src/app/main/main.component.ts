import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountDTO } from '../services/AccountDTO';
import { DTOCreator } from '../services/DTOCreator';
import { FilterDTO } from '../services/FilterDTO';
import { HttpService } from '../services/HttpService';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
  providers: [HttpService]
})
export class MainComponent implements OnInit {
  
  public accountList: Array<AccountDTO> = new Array<AccountDTO>();
  public page : number = 0;
  public pageSize : number = 0;
  public collectionSize : number = 0;

  // Параметры фильтрации запроса
  public isCollapsed = true;
  public chk1: boolean = false;
  public chk2: boolean = false;
  public chk3: boolean = false;
  public chk4: boolean = false;
  public chk5: boolean = false;
  public chk6: boolean = false;
  public chk7: boolean = false;
  public chk8: boolean = false;

  constructor(private http: HttpService, private router: Router) { }

  ngOnInit(): void {
    this.http.GetAccountsByFilter(new FilterDTO()).subscribe((data:Array<AccountDTO>) => {
      this.accountList = data;
      this.accountList.forEach(dto => {
        dto.accountStatus = DTOCreator.returnAccountStatus(dto.accountStatus);
        dto.paymentMethod = DTOCreator.returnPaymentMethod(dto.paymentMethod);
        dto.createTime = dto.createTime.replace("T", " ");
        dto.changeTime = dto.changeTime.replace("T", " ");
      });
    });
    this.page = 1;
    this.pageSize = 8;
    this.collectionSize = this.accountList.length;
  }

  public goToCreate() {
    this.router.navigate(['/create']);
  }

  public sort(category: number) {
    switch (category) {
      case 1: {
        this.accountList = this.accountList.sort((n1, n2) => DTOCreator.convertDateTime(n1.createTime) - DTOCreator.convertDateTime(n2.createTime));
        break;
      }
      case 2: {
        this.accountList = this.accountList.sort((n1, n2) => n1.accountNumber-n2.accountNumber);
        break;
      }
      case 3: {
        this.accountList = this.accountList.sort((n1, n2) => DTOCreator.convertAccountStatus(n1.accountStatus) - DTOCreator.convertAccountStatus(n2.accountStatus));
        break;
      }
      case 4: {
        this.accountList = this.accountList.sort((n1, n2) => n1.balance-n2.balance);
        break;
      }
      case 5: {
        this.accountList = this.accountList.sort((n1, n2) => DTOCreator.convertPaymentMethod(n1.paymentMethod) - DTOCreator.convertPaymentMethod(n2.paymentMethod));
        break;
      }
    }
  }

  public applyFilters(i1:any,i2:any,i3:any,i4:any,i5:any,i6:any,i7:any,i8:any,s1:any,s2:any) {
    var filter : FilterDTO = new FilterDTO();
    if (this.regexCheck(i1) && i1 != "") {
      this.chk1 = true;
      return;
    }
    if (this.regexCheck(i2) && i2 != "") {
      this.chk2 = true;
      return;
    }
    if (this.regexCheck(i3) && i3 != "") {
      this.chk3 = true;
      return;
    }
    if (this.regexCheck(i4) && i4 != "") {
      this.chk4 = true;
      return;
    }
    if (!Number.isInteger(+i5.value) && i5 != "") {
      this.chk5 = true;
      return;
    }
    if (!Number.isInteger(+i6.value) && i6 != "") {
      this.chk6 = true;
      return;
    }
    if (!Number.isInteger(+i7.value) && i7 != "") {
      this.chk7 = true;
      return;
    }
    if (!Number.isInteger(+i8.value) && i8 != "") {
      this.chk8 = true;
      return;
    }
    if (i1.value != "") filter.minCreateTime = i1.value.replace(" ", "T");
    if (i2.value != "") filter.maxCreateTime = i2.value.replace(" ", "T");
    if (i3.value != "") filter.minChangeTime = i3.value.replace(" ", "T");
    if (i4.value != "") filter.maxChangeTime = i4.value.replace(" ", "T");
    if (i5.value != "") filter.minAccountNumber = i5.value;
    if (i6.value != "") filter.maxAccountNumber = i6.value;
    filter.accountStatus = s1.selectedIndex;
    if (i7.value != "") filter.minBalance = i7.value;
    if (i8.value != "") filter.maxBalance = i8.value;
    filter.paymentMethod = s2.selectedIndex;
    this.http.GetAccountsByFilter(filter).subscribe((data:Array<AccountDTO>) => {
      this.accountList = data;
      this.accountList.forEach(dto => {
        dto.accountStatus = DTOCreator.returnAccountStatus(dto.accountStatus);
        dto.paymentMethod = DTOCreator.returnPaymentMethod(dto.paymentMethod);
        dto.createTime = dto.createTime.replace("T", " ");
        dto.changeTime = dto.changeTime.replace("T", " ");
      });
    });
    this.chk1 = false;
    this.chk2 = false;
    this.chk3 = false;
    this.chk4 = false;
    this.chk5 = false;
    this.chk6 = false;
    this.chk7 = false;
    this.chk8 = false;
  }

  public showFilters() {
    if (this.isCollapsed) this.isCollapsed = false;
    else this.isCollapsed = true;
  }

  private regexCheck(date:string) : boolean {
    const regex = new RegExp("[0-9]+-[0-9]+-[0-9]+ [0-9]+:[0-9]+:[0-9]+");
    if (regex.test(date)) return true;
    else return false;
  }
}
