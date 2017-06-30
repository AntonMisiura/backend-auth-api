import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms'
import {Router} from '@angular/router';
import { CustomValidatorsService } from '../services/custom-validators.service'
import {UserService, RegError} from '../services/user.service'
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  providers: [UserService,CustomValidatorsService]
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  emailValid: boolean = false;
  loginPristine: boolean = true;
  loginLength: boolean = false;
  passwordMatched: boolean = true;
  passwordPristine: boolean = true;
  errorMessage:Array<string> = [];
  repeatPristine: boolean = true;
  passwordsMatched: boolean = false;
  serverErrorMessages: RegServerError = new RegServerError();

  constructor(private userService: UserService, private router: Router, private customValidatorsService: CustomValidatorsService) {
     
      this.registerForm = new FormGroup({
                          'email': new FormControl('', [Validators.required, this.customValidatorsService.emailPatternValidator]),
                          'passwords': new FormArray([
                            new FormControl('', [Validators.required, this.customValidatorsService.passwordValidator]),
                            new FormControl('', Validators.required),
                            
                          ], this.customValidatorsService.matchValidator)});

      this.userService.regSuccess = ()=>this.router.navigate(['/login']);

      this.userService.regFail = (error)=> {
              let mythis = this;
              if(error.modelState['user.Login'] != undefined){
                        error.modelState['user.Login'].forEach(function(item, i, arr){
                           mythis.serverErrorMessages.loginErrors.push(item)
                        });
              }
            
              if(error.modelState['user.Password'] != undefined){
                        error.modelState['user.Password'].forEach(function(item, i, arr){
                           mythis.serverErrorMessages.passworErrors.push(item)
                        });
              }
              if(error.modelState['user.RepeatPassword'] != undefined){
                         error.modelState['user.RepeatPassword'].forEach(function(item, i, arr){
                         mythis.serverErrorMessages.confirmPasswordErrors.push(item)
                        });
              }
        };

           this.registerForm.controls['email'].statusChanges.subscribe((data) => {
            this.loginPristine = this.registerForm.controls['email'].pristine;
            (this.registerForm.hasError('emailInvalid', ['email'])) ? this.emailValid = false : this.emailValid = true;
      });


    (<FormArray>this.registerForm.controls['passwords']).controls[0].valueChanges.subscribe((data) => {
      this.passwordPristine = (<FormArray>this.registerForm.controls['passwords']).controls[0].pristine;
      ((<FormArray>this.registerForm.controls['passwords']).hasError('patternNotMatched', ["0"])) ? this.passwordMatched = false : this.passwordMatched = true;
      (this.registerForm.hasError('passwordsDontMatch', ['passwords'])) ? this.passwordsMatched = false : this.passwordsMatched = true;

    });
    (<FormArray>this.registerForm.controls['passwords']).valueChanges.subscribe((data) => {
      this.repeatPristine = (<FormArray>this.registerForm.controls['passwords']).controls[1].pristine;
      (this.registerForm.hasError('passwordsDontMatch', ['passwords'])) ? this.passwordsMatched = false : this.passwordsMatched = true;
    })
  }

      signIn() {
        let objToSend = {
           login: this.registerForm.controls['email'].value,
           password: (<FormArray>this.registerForm.controls['passwords']).controls[0].value,
           repeatPassword: (<FormArray>this.registerForm.controls['passwords']).controls[1].value,
        };
        this.userService.sendRegData(objToSend).subscribe((res)=>{this.userService.regSuccess();}, 
              (error)=>{
                 
                    if(error !== undefined){
                     this.userService.regFail(JSON.parse(error._body)); 
                  }
              });
     
        }
  
          ngOnInit() {
        
          }


}

class RegServerError{
  loginErrors = Array<string>(); 
  fullNameErrors = Array<string>();
  passworErrors = Array<string>();
  confirmPasswordErrors = Array<string>();
}