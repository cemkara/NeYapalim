import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class ContentsService {
  constructor(private http: HttpClient) {}

  getMainContents(): Observable<any> {
    return this.http.get(environment.apiUrl + "Contents/GetMainContents");
  }
  getContent(contentId): Observable<any> {
    return this.http.get(
      environment.apiUrl + "Contents/GetContent/" + contentId
    );
  }
  getAllContent(): Observable<any> {
    return this.http.get(
      environment.apiUrl + "Contents/GetAllContent"
    );
  }
}
