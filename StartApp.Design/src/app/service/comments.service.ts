import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class CommentsService {
  constructor(private http: HttpClient) {}

  addComment(PlaceId, UserId, Text, Point): Observable<any> {
    return this.http.post(environment.apiUrl + "Comments/AddComment", {
      PlaceId: PlaceId,
      UserId: UserId,
      Text: Text,
      Point: Point,
    });
  }
}
