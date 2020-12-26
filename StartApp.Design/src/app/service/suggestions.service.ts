import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SuggestionsService {

  constructor(private http: HttpClient) { }
  getSuggestions(categories, properties, districtId): Observable<any> {
    return this.http.post( environment.apiUrl + 'Places/GetPlacesofRecommendations',
    {
      'categories' : categories,
      'properties' : properties,
      'districtId' : districtId,
      'cityId' : '' 
    })
  }

  getSuggestionsCount(categories, properties, districtId): Observable<any> {
    return this.http.post( environment.apiUrl + 'Places/GetPlacesofRecommendationsCount',
    {
      'categories' : categories,
      'properties' : properties,
      'districtId' : districtId,
      'cityId' : '' 
    })
  }

}
