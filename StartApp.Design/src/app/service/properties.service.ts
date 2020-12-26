import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PropertiesService {

  constructor(private http: HttpClient) { }
  getPropertiesOfCategories(selectedCategories): Observable<any> {
    const headers =  {
      headers: new  HttpHeaders({ 
        'Content-Type': 'application/x-www-form-urlencoded'})
    };
    return this.http.get( environment.apiUrl + 'CategoryProperties/GetPropertiesOfCategories/' + selectedCategories,headers)
  }
}
