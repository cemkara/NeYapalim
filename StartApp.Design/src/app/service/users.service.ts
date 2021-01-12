import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { BaseService } from "./base.service";
import { LocaldataService } from "./localdata.service";

@Injectable()
export class UsersService {
  constructor(
    private localData: LocaldataService,
    private router: Router,
    private baseService: BaseService
  ) {}
  activeUser;

  getActiveUser() {
    return JSON.parse(this.localData.get("user"));
  }

  isActive() {
    if (JSON.parse(this.localData.get("user")) != null) return true;
    return false;
  }

  activeUserControl() {
    if (this.localData.get("user") != null) {
      this.router.navigate(["/default"]);
    }
    return false;
  }

  userLoginControl() {
    if (this.localData.get("user") == null) {
      this.router.navigate(["/login"]);
    }
    return false;
  }

  getComments(id): Observable<any> {
    return this.baseService.get("Users/GetComments/" + id);
  }

  addToFavorite(userId, placeId) {
    return this.baseService.post("UserFavoritePlaces/AddFavoritePlace", {
      userId: userId,
      placeId: placeId,
    });
  }

  removeToFavorite(userId, placeId) {
    return this.baseService.post("UserFavoritePlaces/RemoveFavoritePlace", {
      userId: userId,
      placeId: placeId,
    });
  }

  userFavoritePlaceControl(userId, placeId) {
    return this.baseService.post(
      "UserFavoritePlaces/userFavoritePlaceControl",
      {
        userId: userId,
        placeId: placeId,
      }
    );
  }

  getFavoritePlaces(id): Observable<any> {
    return this.baseService.get("Users/GetFavoritePlaces/" + id);
  }

  getVisitedPlaces(id): Observable<any> {
    return this.baseService.get("Users/GetVisitedPlaces/" + id);
  }

  addUserVisitedPlace(userId, placeId) {
    return this.baseService.post("UserVisitedPlaces/AddUserVisitedPlace", {
      userId: userId,
      placeId: placeId,
    });
  }

  userVisitedPlaceControl(userId, placeId) {
    return this.baseService.post("UserVisitedPlaces/UserVisitedPlaceControl", {
      userId: userId,
      placeId: placeId,
    });
  }
}
