import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable()
export class BaseService {
  constructor(private http: HttpClient) {}

  get<T>(url: string): Observable<T> {
    return this.http.get<T>(environment.apiUrl + url, {
      headers: {
        "Content-Type": "application/json",
      },
    });
  }

  post<T>(url: string, request: any): Observable<T> {
    return this.http.post<T>(environment.apiUrl + url, request, {
      headers: {
        "Content-Type": "application/json",
      },
    });
  }
}
