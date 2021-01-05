import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { BlogService } from "src/app/service/blog.service";

@Component({
  selector: "app-blog-details",
  templateUrl: "./blog-details.component.html",
  styleUrls: ["./blog-details.component.css"],
})
export class BlogDetailsComponent implements OnInit {
  blogId;
  blog;
  constructor(private blogService: BlogService, private route: ActivatedRoute) {
    this.route.queryParams.subscribe((params) => {
      if (params["blogId"] == "" || params["blogId"] == undefined) {
        this.blogId = "0";
      } else {
        this.blogId = params["blogId"];
      }
    });
    blogService.getBlog(this.blogId).subscribe(
      (res) => {
        this.blog = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  ngOnInit(): void {}
}
