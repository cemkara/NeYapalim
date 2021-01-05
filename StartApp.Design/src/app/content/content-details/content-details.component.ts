import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { ContentsService } from "src/app/service/contents.service";

@Component({
  selector: "app-content-details",
  templateUrl: "./content-details.component.html",
  styleUrls: ["./content-details.component.css"],
})
export class ContentDetailsComponent implements OnInit {
  contentId;
  content;
  constructor(
    private contentService: ContentsService,
    private route: ActivatedRoute
  ) {
    this.route.queryParams.subscribe((params) => {
      if (params["contentId"] == "" || params["contentId"] == undefined) {
        this.contentId = "0";
      } else {
        this.contentId = params["contentId"];
      }
    });

    contentService.getContent(this.contentId).subscribe(
      (res) => {
        this.content = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  ngOnInit(): void {}
}
