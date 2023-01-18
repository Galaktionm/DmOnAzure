import { OrderedProduct } from "./OrderedProduct";


export class Order {
    
    orderId : number
    status : string
    orderItems : OrderedProduct[]

    constructor(orderId:number, status:string, orderItems:OrderedProduct[]){
        this.orderId=orderId;
        this.status=status;
        this.orderItems=orderItems;
    }

}