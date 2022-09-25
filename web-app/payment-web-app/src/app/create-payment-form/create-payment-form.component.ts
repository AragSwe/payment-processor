import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-create-payment-form',
  templateUrl: './create-payment-form.component.html',
  styleUrls: ['./create-payment-form.component.scss']
})
export class CreatePaymentFormComponent implements OnInit {
  fromAccount: string = "";
  toAccount: string = "";
  currency: string = "";
  amount: number = 0;

  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
  }

  createPayment() {
    this.httpClient.post('http://localhost:5269/payment',
    {
      fromAccount: this.fromAccount,
      toAccount: this.toAccount,
      currency: this.currency,
      amount: this.amount
    }).subscribe();
  }

}
