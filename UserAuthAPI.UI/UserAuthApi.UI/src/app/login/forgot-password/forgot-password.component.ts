import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { UserService } from '../../services/user.service'
import { CustomValidatorsService } from '../../services/custom-validators.service'
@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  providers: [UserService, CustomValidatorsService]
})
export class ForgotPasswordComponent implements OnInit {
  emailValid: boolean = false;
  emailPristine: boolean = true;
  forgotPasswordForm: FormGroup;

  constructor(private userService: UserService, private customValidatorsService: CustomValidatorsService) {
    this.forgotPasswordForm = new FormGroup({
                          'email': new FormControl('', [Validators.required, this.customValidatorsService.emailPatternValidator])});

    this.forgotPasswordForm.controls['email'].statusChanges.subscribe((data) => {
            this.emailPristine = this.forgotPasswordForm.controls['email'].pristine;
            (this.forgotPasswordForm.hasError('emailInvalid', ['email'])) ? this.emailValid = false : this.emailValid = true;
      });
   }

  ngOnInit() {
  }
  
  onSubmit(){
    let email = {
      'email': this.forgotPasswordForm.controls['email'].value
    }
    this.userService.getRestorePasswordLink(email).subscribe((res)=>{});
  }
}
