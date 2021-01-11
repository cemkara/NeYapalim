import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseService } from "./base.service";

@Injectable()
export class BlogService {
  constructor(private baseService: BaseService) {}

  getMainBlogs(): Observable<any> {
    return this.baseService.get("Blogs/GetMainBlogs");
  }
  getBlog(blogId): Observable<any> {
    return this.baseService.get("Blogs/GetBlog/" + blogId);
  }
  getAllBlogs(): Observable<any> {
    return this.baseService.get("Blogs/GetAllBlogs");
  }
}
