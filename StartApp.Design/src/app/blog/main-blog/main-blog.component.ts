import { Component, OnInit } from '@angular/core';
import { BlogService } from 'src/app/service/blog.service';

@Component({
  selector: 'app-main-blog',
  templateUrl: './main-blog.component.html',
  styleUrls: ['./main-blog.component.css']
})
export class MainBlogComponent implements OnInit {

  mainBlogs;
  constructor(private blogService:BlogService) { 

    blogService.getMainBlogs().subscribe(
      (res) => {
        this.mainBlogs = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  ngOnInit(): void {
  }

}
