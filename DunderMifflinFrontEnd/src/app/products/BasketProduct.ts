export class BasketProduct{
    
    itemId: string;
    name:string;
    price:number;
    amount:number;

    constructor(itemId: string,name:string, price:number, amount:number){
        this.itemId=itemId;
        this.name=name;
        this.price=price;
        this.amount=amount;
    }

}