import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseService } from "./base.service";

@Injectable()
export class CommentsService {
  constructor(private baseService: BaseService) {}

  addComment(PlaceId, UserId, Text, Point): Observable<any> {
    return this.baseService.post("Comments/AddComment", {
      PlaceId: PlaceId,
      UserId: UserId,
      Text: Text,
      Point: Point,
    });
  }
}
