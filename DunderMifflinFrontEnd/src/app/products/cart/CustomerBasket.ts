import { BasketProduct } from "../BasketProduct";
import { Product } from "../Product";

export class CustomerBasket {
    
    userId: string
    items: BasketProduct[]

    constructor(userId:string, items:BasketProduct[]){
        this.userId=userId;
        this.items=items;
    }
}