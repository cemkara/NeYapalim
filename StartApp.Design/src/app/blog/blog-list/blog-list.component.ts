import { Component, OnInit } from '@angular/core';
import { BlogService } from 'src/app/service/blog.service';

@Component({
  selector: 'app-blog-list',
  templateUrl: './blog-list.component.html',
  styleUrls: ['./blog-list.component.css']
})
export class BlogListComponent implements OnInit {
  blogs;

  constructor(private blogService: BlogService) {
    blogService.getAllBlogs().subscribe(
      (res) => {
        this.blogs = res;
      },
      (err) => {
        console.error(err);
      }
    );
   }

  ngOnInit(): void {
  }

}
