import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseService } from "./base.service";

@Injectable()
export class PropertiesService {
  constructor(private baseService: BaseService) {}

  getPropertiesOfCategories(selectedCategories): Observable<any> {
    return this.baseService.get(
      "CategoryProperties/GetPropertiesOfCategories/" + selectedCategories
    );
  }
}
