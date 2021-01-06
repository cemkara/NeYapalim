import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class CategoryService {
  constructor(private http: HttpClient) {}
  getCategories(mainCategoryId): Observable<any> {
    return this.http.get(
      environment.apiUrl + "Categories/GetCategories/" + mainCategoryId
    );
  }

  getMainCategories(): Observable<any> {
    return this.http.get(environment.apiUrl + "Categories/GetMainCategories");
  }

  getAllCategories(): Observable<any> {
    return this.http.get(environment.apiUrl + "Categories/GetAllCategories");
  }

  getCategory(categoryId): Observable<any> {
    return this.http.get(
      environment.apiUrl + "Categories/GetCategory/" + categoryId
    );
  }

  getPlacesByCategoryId(categoryId): Observable<any> {
    return this.http.get(
      environment.apiUrl + "Categories/GetPlacesByCategoryId/" + categoryId
    );
  }
  
}
