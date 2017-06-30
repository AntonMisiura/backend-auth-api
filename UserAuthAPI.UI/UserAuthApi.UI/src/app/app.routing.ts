import {Routes, RouterModule} from '@angular/router';
import { LoginComponent } from './login/login.component';
import { FacebookComponent } from './login/facebook/facebook.component';
import { MainPageComponent } from './main-page/main-page.component';
import { RegisterComponent } from './register/register.component';
import { NotFoundComponent } from './not-found/not-found.component';


const APP_ROUTES: Routes = [
    {path: '', redirectTo:'mainpage', pathMatch: 'full'},
    {path: 'mainpage', component: MainPageComponent},
    {path: 'register', component: RegisterComponent},
    {path: 'login', component: LoginComponent}, 
    {path: 'fb', component: FacebookComponent}, 
    {path: '**', component: NotFoundComponent}
]

export const Routing = RouterModule.forRoot(APP_ROUTES);