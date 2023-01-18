import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './User';
import { URLGlobal } from 'src/app/URLGlobal';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

  user!: User

  constructor(private http: HttpClient){}

  ngOnInit(): void {
      this.getUser();
  }

  getUser(){
    var url=URLGlobal.aggregator+"api/account/"+sessionStorage.getItem("userId");
    this.http.get<User>(url).subscribe({
      next: (result)=>{
        console.log(result);
        this.user=result;
      },
      error: (error)=>{
        console.log(error);
      }
    })
  }

}

export class UserRequest{
  userId:string

  constructor(userId:string){
    this.userId=userId;
  }
}
