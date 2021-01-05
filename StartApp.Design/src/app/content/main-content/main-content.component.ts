import { Component, OnInit, ViewChild } from "@angular/core";
import {
  NgbCarousel,
  NgbSlideEvent,
  NgbSlideEventSource,
} from "@ng-bootstrap/ng-bootstrap";
import { ContentsService } from "src/app/service/contents.service";

@Component({
  selector: "app-main-content",
  templateUrl: "./main-content.component.html",
  styleUrls: ["./main-content.component.css"],
})
export class MainContentComponent implements OnInit {
  mainContents;

  constructor(private contentService: ContentsService) {
    contentService.getMainContents().subscribe(
      (res) => {
        this.mainContents = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }
  images = [944, 1011, 984].map((n) => `https://picsum.photos/id/${n}/500/150`);
  ngOnInit(): void {}
}
