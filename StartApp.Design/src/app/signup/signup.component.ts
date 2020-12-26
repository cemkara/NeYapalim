import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { GoogleLoginProvider, SocialAuthService, SocialUser } from 'angularx-social-login';
import { LocaldataService } from '../service/localdata.service';
import { LoginService } from '../service/login.service';
import { UsersService } from '../service/users.service';

@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {
    focus;
    focus1;
    focus2;
    user: SocialUser;
    loggedIn: boolean;
    registerForm: FormGroup;
    emailRegx = /^(([^<>+()\[\]\\.,;:\s@"-#$%&=]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,3}))$/;
    notValid = true;
    loginError = true;
    ErrorMessage = "";

    constructor(private formBuilder: FormBuilder, 
        private loginService: LoginService, 
        private router:Router, 
        private localData:LocaldataService, 
        private userService: UsersService,
        private authService: SocialAuthService) { }

    ngOnInit() {
        this.registerForm = this.formBuilder.group({
            name: [null,Validators.required],
            email: [null, [Validators.required, Validators.pattern(this.emailRegx)]],
            password: [null, Validators.required]
          });
      
          this.authService.authState.subscribe((user) => {
            this.user = user;
            this.loggedIn = (user != null);
            if(this.loggedIn)
            {
              this.loginError = false;
              this.googleRegisterFunction(user.name,user.email,user.authToken);
            }
            else{
              this.loginError = false;
            }
          });
    }
    submit() {
        if (!this.registerForm.valid) {
          this.notValid=false;
          return;
        }
        this.notValid=true;
        this.loginService.registerUser(this.registerForm.value.name,this.registerForm.value.email,this.registerForm.value.password).subscribe(
          res=>{
            if(res==null){
              this.loginError = false;
              return;
            }
            this.localData.set("user",JSON.stringify(res));
            this.router.navigate(['/default']);
          },
          err => {
            this.loginError=false;
            this.ErrorMessage = err.error.Message;
          }
        );
      }
    
      googleRegisterFunction(name, email, token)
      {
        this.notValid=true;
        this.loginService.googleRegisterUser(name,email).subscribe(
          res=>{
            if(res==null){
              this.loginError = false;
              return;
            }
            this.localData.set("user",JSON.stringify(res));
            this.router.navigate(['/default']);
          },
          err => {
            this.loginError=false;
            this.ErrorMessage = err.error.Message;
          }
        );
      }
       // signInWithFB(): void {
       //   this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
       // }
       
       signOut(): void {
         this.authService.signOut();
       }
   
       googleRegister()
       {
         this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
       }
   
       // facebookLogin(){
       //   this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
       // }
}
