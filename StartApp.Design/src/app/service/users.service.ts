import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LocaldataService } from './localdata.service';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private localData: LocaldataService, private router: Router, private http: HttpClient) { }
  activeUser;

  getActiveUser()
  {
      return JSON.parse(this.localData.get("user"));
  }

  activeUserControl()
  {
      if(this.localData.get("user") != null){
        this.router.navigate(['/default']);
      }
      return false;
  }

  userLoginControl()
  {
    if(this.localData.get("user") == null){
      this.router.navigate(['/login']);
    }
    return false;
  }

  getComments(id) :Observable<any> {
    return this.http.get(environment.apiUrl + 'Users/GetComments/' + id);
  }

  addToFavorite(userId, placeId)
  {
    return this.http.post( environment.apiUrl + 'UserFavoritePlaces/AddFavoritePlace',
    {
      'userId':userId,
      'placeId': placeId
    })
  }

  removeToFavorite(userId, placeId)
  {
    return this.http.post( environment.apiUrl + 'UserFavoritePlaces/RemoveFavoritePlace',
    {
      'userId':userId,
      'placeId': placeId
    })
  }

  userFavoritePlaceControl(userId,placeId)
  {
    return this.http.post( environment.apiUrl + 'UserFavoritePlaces/userFavoritePlaceControl',
    {
      'userId':userId,
      'placeId': placeId
    })
  }

  getFavoritePlaces(id) :Observable<any> {
    return this.http.get(environment.apiUrl + 'Users/GetFavoritePlaces/' + id);
  }

  getVisitedPlaces(id) :Observable<any> {
    return this.http.get(environment.apiUrl + 'Users/GetVisitedPlaces/' + id);
  }

  addUserVisitedPlace(userId, placeId)
  {
    return this.http.post( environment.apiUrl + 'UserVisitedPlaces/AddUserVisitedPlace',
    {
      'userId':userId,
      'placeId': placeId
    })
  }

  userVisitedPlaceControl(userId,placeId)
  {
    return this.http.post( environment.apiUrl + 'UserVisitedPlaces/UserVisitedPlaceControl',
    {
      'userId':userId,
      'placeId': placeId
    })
  }
}
