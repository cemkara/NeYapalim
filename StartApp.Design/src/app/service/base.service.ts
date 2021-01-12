import {
  HttpClient,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { NgxSpinnerService } from "ngx-spinner";
import { Observable } from "rxjs";
import { finalize, tap } from "rxjs/operators";
import { environment } from "src/environments/environment";

@Injectable()
export class BaseService implements HttpInterceptor {
  constructor(private http: HttpClient, private spinner: NgxSpinnerService) {}
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

  count = 0;

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    this.spinner.show();

    this.count++;

    return next.handle(req).pipe(
      tap(
        // (event) => console.log(event),
        // (error) => console.log(error)
      ),
      finalize(() => {
        this.count--;
        if (this.count == 0) this.spinner.hide();
      })
    );
  }
}
