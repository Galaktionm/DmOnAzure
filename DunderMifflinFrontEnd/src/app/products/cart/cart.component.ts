import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { CustomerBasket } from './CustomerBasket';
import { BasketProduct } from '../BasketProduct';
import { CustomerBasketRequest } from '../productsComponent/CustomerBasketRequest';
import { URLGlobal } from 'src/app/URLGlobal';
import { OrderResult } from './OrderResult';
import { Order } from 'src/app/authorization/account/Order';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  basketProducts!:BasketProduct[]

  constructor(private http:HttpClient){}

  ngOnInit(): void {
      this.getCart();
  }

  getCart(){
    var url=URLGlobal.aggregator+"api/cart/"+sessionStorage.getItem("userId");
    this.http.get<CustomerBasket>(url).subscribe({
      next: (result)=>{
        this.basketProducts=result.items;
        console.log(result);
      },
      error: (error)=>{
        console.log(error)
      }
    });
  }

  removeFromCart(product:BasketProduct){
    var url=URLGlobal.aggregator+"api/cart/items/remove/"+sessionStorage.getItem("userId");
    this.http.post(url, product).subscribe({
      next: (result)=>{
        console.log(result);
        window.location.reload();
      },
      error: (error)=>{
        console.log(error);
      }
    })

  }

  submitOrder(){
    var url=URLGlobal.aggregator+"api/order/"+sessionStorage.getItem("userId");
    this.http.post<OrderResult>(url, this.basketProducts).subscribe({
      next: (result)=>{
        console.log(result);
        var alert=document.createElement("div");
        alert.className="alert alert-success";
        var text=document.createTextNode(result.message);
        alert.appendChild(text);
        var body=document.getElementById("alert");
        body?.appendChild(alert);

      },
      error: (error)=>{
        console.log(error);
        var alert=document.createElement("div");
        alert.className="alert alert-danger";
        var text=document.createTextNode("Could not save order. Try again or try different products. Some of the products might not be available anymore. Sorry for the inconvenience");
        alert.appendChild(text);
        var body=document.getElementById("alert");
        body?.appendChild(alert);
      }
    })
  }

  

}
