import { Component, OnInit } from '@angular/core';
import { FacebookService } from './facebook.service';
import { InitParams } from './models/InitParams'
@Component({
  selector: 'app-facebook',
  templateUrl: './facebook.component.html',
  providers: [FacebookService]
})
export class FacebookComponent implements OnInit {
  
  constructor(private fb: FacebookService) { 
      
  }

  login(){
    this.fb.login().then((response: any) => console.log(response));
  }
  ngOnInit() {
    let facebookInitParams: InitParams = {
        appId      : '720460194825623',
        cookie     : true, 
        xfbml      : true,  
        version    : 'v2.8' 
      }
    this.fb.init(facebookInitParams);
     this.fb.getLoginStatus().then(((response: any) => console.log(response)));

  }
}
