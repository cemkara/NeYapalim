import { Component, OnInit } from "@angular/core";
import { async } from "@angular/core/testing";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { ModalDismissReasons, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { Observable } from "rxjs";
import { CommentsService } from "../service/comments.service";
import { DetailsService } from "../service/details.service";
import { PropertiesService } from "../service/properties.service";
import { UsersService } from "../service/users.service";

@Component({
  selector: "app-place-details",
  templateUrl: "./place-details.component.html",
  styleUrls: ["./place-details.component.css"],
})
export class PlaceDetailsComponent implements OnInit {
  place: Observable<any>;
  placeProducts: Observable<any>;
  placeCommentsTopTen: Observable<any>;
  placeId;
  user = null;
  userFavorite = false;
  userVisited = false;
  commentForm: FormGroup;
  closeResult: string;
  placeProperties: Observable<any>;
  placeCategories: Observable<any>;

  constructor(
    private detailService: DetailsService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private route: ActivatedRoute,
    private userService: UsersService,
    private commentService: CommentsService
  ) {
    this.route.queryParams.subscribe((params) => {
      if (params["placeId"] == "" || params["placeId"] == undefined) {
        this.placeId = "0";
      } else {
        this.placeId = params["placeId"];
      }
    });
    detailService.getPlace(this.placeId).subscribe(
      (res) => {
        this.place = res;
        if (userService.getActiveUser() != null) {
          this.user = userService.getActiveUser();
          userService
            .userFavoritePlaceControl(this.user.Id, this.placeId)
            .subscribe(
              (res) => {
                if (res) this.userFavorite = true;
              },
              (err) => {
                console.error(err);
              }
            );

          userService
            .userVisitedPlaceControl(this.user.Id, this.placeId)
            .subscribe(
              (res) => {
                if (res) this.userVisited = true;
              },
              (err) => {
                console.error(err);
              }
            );
        }

        detailService.getPlaceProductMain(this.placeId).subscribe(
          (res) => {
            this.placeProducts = res;
          },
          (err) => {
            console.error(err);
          }
        );

        detailService.getPlaceCommentsTopTen(this.placeId).subscribe(
          (res) => {
            this.placeCommentsTopTen = res;
          },
          (err) => {
            console.error(err);
          }
        );

        detailService.getProperties(this.placeId).subscribe(
          (res) => {
            if (res) this.placeProperties = res;
          },
          (err) => {
            console.error(err);
          }
        );

        detailService.getCategories(this.placeId).subscribe(
          (res) => {
            if (res) this.placeCategories = res;
          },
          (err) => {
            console.error(err);
          }
        );
      },
      (err) => {
        console.error(err);
      }
    );
  }

  openMaps() {
    window.open(
      "https://maps.google.com/maps?daddr={{place.Latitude}},{{place.Longitude}}&amp;ll="
    );
  }

  ngOnInit(): void {
    this.commentForm = this.formBuilder.group({
      point: [null, Validators.required],
      messages: [null, Validators.required],
    });

    // this.form = fb.group({
    //   gender: ['', Validators.required]
    // });
  }

  addToFavorite() {
    if (this.user != null) {
      this.userService.addToFavorite(this.user.Id, this.placeId).subscribe(
        (res) => {
          this.userFavorite = true;
        },
        (err) => {
          console.error(err);
        }
      );
    } else {
      window.location.href = "#/login";
    }
  }
  removeToFavorite() {
    if (this.user != null) {
      this.userService.removeToFavorite(this.user.Id, this.placeId).subscribe(
        (res) => {
          this.userFavorite = false;
        },
        (err) => {
          console.error(err);
        }
      );
    } else {
      window.location.href = "#/login";
    }
  }
  addToVisited() {
    if (this.user != null) {
      this.userService
        .addUserVisitedPlace(this.user.Id, this.placeId)
        .subscribe(
          (res) => {
            this.userVisited = true;
          },
          (err) => {
            console.error(err);
          }
        );
    } else {
      window.location.href = "#/login";
    }
  }

  //modal eklenecek
  open(content, type, modalDimension) {
    if (modalDimension === "sm" && type === "modal_mini") {
      this.modalService
        .open(content, {
          windowClass: "modal-mini",
          size: "sm",
          centered: true,
        })
        .result.then(
          (result) => {
            this.closeResult = "Closed with: $result";
          },
          (reason) => {
            this.closeResult = "Dismissed $this.getDismissReason(reason)";
          }
        );
    } else if (modalDimension === "" && type === "Notification") {
      this.modalService
        .open(content, { windowClass: "modal-danger", centered: true })
        .result.then(
          (result) => {
            this.closeResult = "Closed with: $result";
          },
          (reason) => {
            this.closeResult = "Dismissed $this.getDismissReason(reason)";
          }
        );
    } else {
      this.modalService.open(content, { centered: true }).result.then(
        (result) => {
          this.closeResult = "Closed with: $result";
        },
        (reason) => {
          this.closeResult = "Dismissed $this.getDismissReason(reason)";
        }
      );
    }
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return "by pressing ESC";
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return "by clicking on a backdrop";
    } else {
      return "with: $reason";
    }
  }

  notValid = true;
  submitComment() {
    if (!this.commentForm.valid) {
      this.notValid = false;
      return;
    }
    this.notValid = true;

    if (this.user != null) {
      this.commentService
        .addComment(
          this.placeId,
          this.user.Id,
          this.commentForm.value.messages,
          this.commentForm.value.point
        )
        .subscribe(
          (res) => {
            if (res != null) {
              document.getElementById("btnCancel").click();
            }
          },
          (err) => {
            console.error(err);
          }
        );
    } else {
      window.location.href = "#/login";
    }
  }
}
