import { Order } from "./Order";

export class User{
    username:string
    email:string
    balance:number
    orders: Order[]

    constructor(username:string, email:string, balance:number, orders: Order[]){
        this.username=username;
        this.email=email;
        this.balance=balance;
        this.orders=orders;
    }
}