import { Component, OnInit, Input } from "@angular/core";
import { MainCategoryService } from "../service/main-category.service";
import { Observable } from "rxjs";
import { LocationService } from "../service/location.service";
import { LocaldataService } from "../service/localdata.service";
import { environment } from "src/environments/environment";
import { DeviceService } from "../service/device.service";

@Component({
  selector: "app-default",
  templateUrl: "./default.component.html",
  styleUrls: ["./default.component.css"],
})
export class DefaultComponent implements OnInit {
  model = {
    left: true,
    middle: false,
    right: false,
  };

  focus;
  focus1;

  @Input()
  mainCategories: Observable<any>;
  keyword = "name";
  data;
  randNumber = 1;

  selectEvent(item) {
    localStorage.setItem("districtId", item["id"]);
    localStorage.setItem("districtName", item["name"]);
  }

  onChangeSearch(val: string) {
    // fetch remote data from here
    // And reassign the 'data' which is binded to 'data' property.
  }

  onFocused(e) {
    // do something when input is focused
  }

  constructor(
    private mainCategoryService: MainCategoryService,
    private locationService: LocationService,
    private localdata: LocaldataService,
    private deviceService: DeviceService
  ) {
    this.getRandomInt();
    // localdata.allClear();
    this.locationService.findMe();
    mainCategoryService.getMainCategories().subscribe(
      (res) => {
        this.mainCategories = res;
      },
      (err) => {
        console.error(err);
      }
    );

    locationService.getDistrict().subscribe(
      (res) => {
        this.data = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }
  getRandomInt() {
    this.randNumber = Math.floor(Math.random() * (5 - 1) + 1); //The maximum is exclusive and the minimum is inclusive
    console.log(this.randNumber);
  }

  ngOnInit() {}
}
