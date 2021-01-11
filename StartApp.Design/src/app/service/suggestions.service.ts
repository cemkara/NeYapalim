import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseService } from "./base.service";

@Injectable()
export class SuggestionsService {
  constructor(private baseService: BaseService) {}
  getSuggestions(categories, properties, districtId): Observable<any> {
    return this.baseService.post("Places/GetPlacesofRecommendations", {
      categories: categories,
      properties: properties,
      districtId: districtId,
      cityId: "",
    });
  }

  getSuggestionsCount(categories, properties, districtId): Observable<any> {
    return this.baseService.post("Places/GetPlacesofRecommendationsCount", {
      categories: categories,
      properties: properties,
      districtId: districtId,
      cityId: "",
    });
  }
}
