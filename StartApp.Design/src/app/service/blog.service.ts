import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BlogService {

  constructor(private http:HttpClient) { }

  getMainBlogs(): Observable<any> {
    return this.http.get(environment.apiUrl + "Blogs/GetMainBlogs");
  }
  getBlog(blogId): Observable<any> {
    return this.http.get(
      environment.apiUrl + "Blogs/GetBlog/" + blogId
    );
  }
  getAllBlogs(): Observable<any> {
    return this.http.get(
      environment.apiUrl + "Blogs/GetAllBlogs"
    );
  }
}
