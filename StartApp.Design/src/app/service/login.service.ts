import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseService } from "./base.service";

@Injectable()
export class LoginService {
  constructor(private baseService: BaseService) {}
  isLogin;

  loginUser(email, password): Observable<any> {
    return this.baseService.post("Users/Login", {
      Email: email,
      Password: password,
    });
  }

  googleLoginUser(email): Observable<any> {
    return this.baseService.post("Users/GoogleLogin", {
      Email: email,
    });
  }

  registerUser(name, email, password): Observable<any> {
    return this.baseService.post("Users/Register", {
      Name: name,
      Email: email,
      Password: password,
    });
  }

  googleRegisterUser(name, email): Observable<any> {
    return this.baseService.post("Users/GoogleRegister", {
      Name: name,
      Email: email,
    });
  }
}
