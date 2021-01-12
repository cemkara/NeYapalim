import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { LocaldataService } from "../service/localdata.service";
import { LoginService } from "../service/login.service";
import { UsersService } from "../service/users.service";
import { SocialAuthService, SocialUser } from "angularx-social-login";
import {
  FacebookLoginProvider,
  GoogleLoginProvider,
} from "angularx-social-login";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
})
export class LoginComponent implements OnInit {
  focus;
  focus1;

  user: SocialUser;
  loggedIn: boolean;
  loginForm: FormGroup;
  emailRegx = /^(([^<>+()\[\]\\.,;:\s@"-#$%&=]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,3}))$/;
  notValid = true;
  loginError = true;

  constructor(
    private formBuilder: FormBuilder,
    private loginService: LoginService,
    private router: Router,
    private localData: LocaldataService,
    private userService: UsersService,
    private authService: SocialAuthService
  ) {
    userService.activeUserControl();
  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email: [null, [Validators.required, Validators.pattern(this.emailRegx)]],
      password: [null, Validators.required],
    });

    this.authService.authState.subscribe((user) => {
      this.user = user;
      this.loggedIn = user != null;
      if (this.loggedIn) {
        this.loginError = false;
        this.googleLoginFunction(user.email, user.authToken);
      } else {
        this.loginError = false;
      }
    });
  }

  submit() {
    if (!this.loginForm.valid) {
      this.notValid = false;
      return;
    }
    this.notValid = true;
    this.loginService
      .loginUser(this.loginForm.value.email, this.loginForm.value.password)
      .subscribe(
        (res) => {
          if (res == null) {
            this.loginError = false;
            return;
          }
          this.localData.set("user", JSON.stringify(res));
          this.loginService.isLogin = true;
          this.router.navigate(["/default"]);
        },
        (err) => {
          console.error(err);
        }
      );
  }

  googleLoginFunction(email, token) {
    this.notValid = true;
    this.loginService.googleLoginUser(email).subscribe(
      (res) => {
        if (res == null) {
          this.loginError = false;
          return;
        }
        this.localData.set("user", JSON.stringify(res));
        this.loginService.isLogin = true;
        this.router.navigate(["/default"]);
      },
      (err) => {
        console.error(err);
      }
    );
  }

  // signInWithFB(): void {
  //   this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
  // }

  signOut(): void {
    this.authService.signOut();
  }

  googleLogin() {
    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }

  goToHome()
  {
    window.location.href = "#/default";
  }

  // facebookLogin(){
  //   this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
  // }
}
