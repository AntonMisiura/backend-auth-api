import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { AppComponent } from './app.component';
import { CookieService } from 'angular2-cookie/services/cookies.service';
import { LoginComponent } from './login/login.component';
import { UserService } from './services/user.service';
import { RegisterComponent } from './register/register.component';
import {Routing} from './app.routing';
import { MainPageComponent } from './main-page/main-page.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ForgotPasswordComponent } from './login/forgot-password/forgot-password.component';
import { FacebookComponent } from './login/facebook/facebook.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    MainPageComponent,
    NotFoundComponent,
    ForgotPasswordComponent,
    FacebookComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [
    BrowserModule,
    Routing,
    ReactiveFormsModule,
    HttpModule, 
  ],
  providers: [{provide: LocationStrategy, useClass: HashLocationStrategy}, CookieService, UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
