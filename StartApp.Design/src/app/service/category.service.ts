import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseService } from "./base.service";

@Injectable()
export class CategoryService {
  constructor(private baseService: BaseService) {}

  getCategories(mainCategoryId): Observable<any> {
    return this.baseService.get("Categories/GetCategories/" + mainCategoryId);
  }

  getMainCategories(): Observable<any> {
    return this.baseService.get("Categories/GetMainCategories");
  }

  getAllCategories(): Observable<any> {
    return this.baseService.get("Categories/GetAllCategories");
  }

  getCategory(categoryId): Observable<any> {
    return this.baseService.get("Categories/GetCategory/" + categoryId);
  }

  getPlacesByCategoryId(categoryId): Observable<any> {
    return this.baseService.get(
      "Categories/GetPlacesByCategoryId/" + categoryId
    );
  }
}
