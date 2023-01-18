import { Component, OnInit } from '@angular/core';
import { Product } from '../Product';
import { HttpClient } from '@angular/common/http';
import { CustomerBasket } from '../cart/CustomerBasket';
import { CustomerBasketRequest } from './CustomerBasketRequest';
import { FormControl, FormGroup } from '@angular/forms';
import { BasketProduct } from '../BasketProduct';
import { CartComponent } from '../cart/cart.component';
import { URLGlobal } from 'src/app/URLGlobal';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  products!:Product[]
  searchedProducts!:Product[]
  searchForm!:FormGroup
  amountForm!:FormGroup;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
      this.fetchData();
      this.searchForm=new FormGroup({
        query:new FormControl('')
      });
      this.amountForm=new FormGroup({
        amount:new FormControl('')
      })
  }

  fetchData(){
    var url=URLGlobal.productService+"api/products";
    this.http.get<Product[]>(url).subscribe({
      next: (result)=>{
        console.log(result),
        this.products=result
      },
      error: (error)=>{
        console.log(error);
      }
    })
  }

  addToCart(product: Product){
    var url=URLGlobal.aggregator+"api/cart/items/add/";
    var amount=this.amountForm.controls["amount"].value;
    console.log(amount);
    var productRequest=new BasketProduct(product.id, product.name, product.price, amount);
    var userId=sessionStorage.getItem("userId");
    if(userId!=null){
    try{
    this.http.post<string>(url+userId, productRequest).subscribe({
      next: (result)=>{
        console.log(result);
        window.location.reload();
      },
      error: (error)=>{
        console.log(error);
      }
    })
    }catch (error){
      console.log(error);
    }
    }
  }

  searchProduct(){
    var query=this.searchForm.controls["query"].value;
    var request=new ProductsSearchRequest(query);
    var url=URLGlobal.productService+"api/products";
    this.http.post<Product[]>(url, request).subscribe({
      next: (result)=>{
        this.searchedProducts=result;
        console.log(result);
        console.log("Searched");
      },
      error: (error)=>{
        console.log(error);
      }
    })
  }
}

export class ProductsSearchRequest {
  query:string
  constructor(query:string){
    this.query=query;
  }
}
