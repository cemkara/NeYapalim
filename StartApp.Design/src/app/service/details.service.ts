import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseService } from "./base.service";

@Injectable()
export class DetailsService {
  constructor(private baseService: BaseService) {}

  getPlace(placeId): Observable<any> {
    return this.baseService.get("Places/GetPlace/" + placeId);
  }

  getPlaceProductMain(placeId): Observable<any> {
    return this.baseService.get("Places/GetProductMain/" + placeId);
  }

  getPlaceProducts(placeId): Observable<any> {
    return this.baseService.get("Places/GetProducts/" + placeId);
  }

  getPlaceComments(placeId): Observable<any> {
    return this.baseService.get("Places/GetComments/" + placeId);
  }

  getPlaceCommentsTopTen(placeId): Observable<any> {
    return this.baseService.get("Places/GetCommentsTopTen/" + placeId);
  }

  getProperties(placeId): Observable<any> {
    return this.baseService.get("Places/GetProperties/" + placeId);
  }

  getCategories(placeId): Observable<any> {
    return this.baseService.get("Places/GetCategories/" + placeId);
  }
}
