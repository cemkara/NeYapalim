import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class LoginService {
  constructor(private http: HttpClient) {}

  loginUser(email, password): Observable<any> {
    return this.http.post(environment.apiUrl + "Users/Login", {
      Email: email,
      Password: password,
    });
  }

  googleLoginUser(email): Observable<any> {
    return this.http.post(environment.apiUrl + "Users/GoogleLogin", {
      Email: email,
    });
  }

  registerUser(name, email, password): Observable<any> {
    return this.http.post(environment.apiUrl + "Users/Register", {
      Name: name,
      Email: email,
      Password: password,
    });
  }

  googleRegisterUser(name, email): Observable<any> {
    return this.http.post(environment.apiUrl + "Users/GoogleRegister", {
      Name: name,
      Email: email,
    });
  }
}
