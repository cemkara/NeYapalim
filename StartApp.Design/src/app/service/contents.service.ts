import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseService } from "./base.service";

@Injectable()
export class ContentsService {
  constructor(private baseService: BaseService) {}

  getMainContents(): Observable<any> {
    return this.baseService.get("Contents/GetMainContents");
  }
  getContent(contentId): Observable<any> {
    return this.baseService.get("Contents/GetContent/" + contentId);
  }
  getAllContent(): Observable<any> {
    return this.baseService.get("Contents/GetAllContent");
  }
}
