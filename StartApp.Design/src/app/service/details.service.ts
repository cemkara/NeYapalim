import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DetailsService {

  constructor(private http: HttpClient) { }
  
  getPlace(placeId): Observable<any> {
    return this.http.get( environment.apiUrl + 'Places/GetPlace/' + placeId)
  }

  getPlaceProductMain(placeId): Observable<any>{
    return this.http.get(environment.apiUrl + "Places/GetProductMain/" + placeId);
  }

  getPlaceProducts(placeId): Observable<any>{
    return this.http.get(environment.apiUrl + "Places/GetProducts/" + placeId);
  }

  getPlaceComments(placeId): Observable<any>{
    return this.http.get(environment.apiUrl + "Places/GetComments/" + placeId);
  }

  getPlaceCommentsTopTen(placeId): Observable<any>{
    return this.http.get(environment.apiUrl + "Places/GetCommentsTopTen/" + placeId);
  }

}
