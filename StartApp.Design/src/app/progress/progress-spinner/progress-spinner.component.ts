import { Component, OnInit } from "@angular/core";
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: "app-progress-spinner",
  templateUrl: "./progress-spinner.component.html",
  styleUrls: ["./progress-spinner.component.css"],
})
export class ProgressSpinnerComponent implements OnInit {
  constructor(private SpinnerService: NgxSpinnerService) {}

  ngOnInit(): void {}

  show() {
    this.SpinnerService.show();
  }
  hide() {
    this.SpinnerService.hide();
  }
}
