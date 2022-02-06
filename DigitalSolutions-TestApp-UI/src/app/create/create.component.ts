import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountDTO } from '../services/AccountDTO';
import { HttpService } from '../services/HttpService';
import { OperationDTO } from '../services/OperationDTO';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css'],
  providers: [HttpService]
})
export class CreateComponent implements OnInit {

  public numCheck : boolean = false;
  public balCheck : boolean = false;

  constructor(private http: HttpService, private router: Router) { }

  ngOnInit(): void {
  }

  public goToMain() {
    this.router.navigate(['/main']);
  }

  public saveAccount(numb: any, balance: any, type: any) {
    if (!Number.isInteger(+numb.value) || +numb.value == 0) {
      this.numCheck = true;
      return;
    }
    if (!Number.isInteger(+balance.value) || +balance.value == 0) {
      this.balCheck = true;
      return;
    }
    this.numCheck = false;
    this.balCheck = false;
    var account = new AccountDTO();
    account.accountNumber = numb.value;
    account.balance = balance.value;
    account.paymentMethod = type.selectedIndex + 1;
    this.http.AddAccount(account).subscribe((data:OperationDTO) => {
      if (data.successful == false) {
        this.numCheck = true;
        alert("Счёт с указанным идентификатором уже существует! Введите другие данные!");
        return;
      }
    });
  }
}
