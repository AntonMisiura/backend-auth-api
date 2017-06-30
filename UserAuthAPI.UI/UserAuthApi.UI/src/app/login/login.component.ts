import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms'
import {UserService} from '../services/user.service'
import { CustomValidatorsService } from '../services/custom-validators.service'
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  providers: [UserService, CustomValidatorsService]
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  errorMessage:Array<string> = [];
  emailValid: boolean = false;
  loginPristine: boolean = true;
  forgotPasswordVisible: boolean = false;
  constructor(private userService: UserService, private route: Router, private customValidatorsService: CustomValidatorsService) {
    this.loginForm = new FormGroup({
      'login': new FormControl('',[Validators.required,  this.customValidatorsService.emailPatternValidator]),
      'password': new FormControl('', Validators.required)
    });

    this.userService.success = ()=>this.route.navigate(['/main-page']);
    
    this.userService.fail = (error)=> {
     let mythis = this;
      if(error.modelState['user.Login'] != undefined){
        error.modelState['user.Login'].forEach(function(item, i, arr){
             mythis.errorMessage.push(item)
        })
      }
      if(error.modelState['user.Password'] != undefined){
         error.modelState['user.Password'].forEach(function(item, i, arr){
             mythis.errorMessage.push(item)
        })
      }
};
    this.loginForm.controls['login'].statusChanges.subscribe((data) => {
            this.loginPristine = this.loginForm.controls['login'].pristine;
            (this.loginForm.hasError('emailInvalid', ['login'])) ? this.emailValid = false : this.emailValid = true;
      });
 
   }

  ngOnInit() {
  }
  
  onSubmit(){
        let loginObj = {
          'login': this.loginForm.controls['login'].value,
          'password': this.loginForm.controls['password'].value
        }
        this.userService.logIn(loginObj);
  }
  showForgotPasswordForm(){
    this.forgotPasswordVisible = true;
  }

}
