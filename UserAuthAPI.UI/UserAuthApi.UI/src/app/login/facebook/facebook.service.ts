import { Injectable } from '@angular/core';
import { InitParams } from './models/InitParams'
declare var FB: any;
export type ApiMethod = 'get' | 'post' | 'delete';
@Injectable()
export class FacebookService {

  constructor() { }
  init(params: InitParams): Promise<any> {
    try {
      return Promise.resolve(FB.init(params));
    } catch (e) {
      return Promise.reject(e);
    }
  }
   login(options?: any): Promise<any> {
    return new Promise<any>((resolve, reject) => {
      try {
        FB.login((response: any) => {
          if (response.authResponse) {
            resolve(response);
          }else{
            reject();
          }
        }, options);
      } catch (e) {
        reject(e);
      }

    });
  }
  getLoginStatus(): Promise<any>{
    return new Promise <any>((resolve, reject)=>{
      try{
        FB.getLoginStatus((response) =>{
          this.statusChangeCallback(response);
          resolve(response);
      });
      }
      catch(e){
        reject(e)
      }
    })
  }
  statusChangeCallback(response) {
    console.log('statusChangeCallback');
    console.log(response);
    if (response.status === 'connected') {
      // Logged into your app and Facebook.
      this.fetchUserInfo();
    } else {
      // The person is not logged into your app or we are unable to tell.
      document.getElementById('status').innerHTML = 'Please log ' +
        'into this app.';
    }
  }
  fetchUserInfo() {
    console.log('Welcome!  Fetching your information.... ');
    this.api('/me').then((res)=>{
       console.log('Successful login for: ' + res.name);
    });
  }
  api(url:string){
      return new Promise<any>((resolve, reject)=>{
        try{
          FB.api(url, (response)=>{
            resolve(response)
          })
        }
        catch(e){
          reject(e);
        }
      })
  }
    
}
