import { BasketProduct } from "../BasketProduct";
import { Product } from "../Product";

export class CustomerBasketRequest {
    userId: string
    product: BasketProduct

    constructor(userId:string, product:BasketProduct){
        this.userId=userId;
        this.product=product;
    }
}