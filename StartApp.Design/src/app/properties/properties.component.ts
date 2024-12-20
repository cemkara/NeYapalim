import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Observable } from "rxjs";
import { LocaldataService } from "../service/localdata.service";
import { PropertiesService } from "../service/properties.service";
import { Location } from '@angular/common';

@Component({
  selector: "app-properties",
  templateUrl: "./properties.component.html",
  styleUrls: ["./properties.component.css"],
})
export class PropertiesComponent implements OnInit {
  properties: Observable<any>;
  selectedCategories;

  constructor(
    private propertiesService: PropertiesService,
    private route: ActivatedRoute,
    private localData: LocaldataService,
    private location: Location
  ) {
    this.route.queryParams.subscribe((params) => {
      if (params["selectedCategories"] == "") this.selectedCategories = "0";
      else this.selectedCategories = params["selectedCategories"];
    });

    propertiesService
      .getProperties()
      .subscribe(
        (res) => {
          if (res != null) this.properties = res;
        },
        (err) => {
          console.error(err);
        }
      );
  }

 
  ngOnInit(): void {}

  changeSelected(index) {
    var item = this.properties[index];
    if (item.tempSelect) item.color = "";
    else item.color = "active-";

    item.tempSelect = !item.tempSelect;
    this.setSelectedProperties(item);
  }

  selectedProperties: string[] = [];
  setSelectedProperties(category) {
    const indexItem = this.selectedProperties.indexOf(category.Id);
    if (category.tempSelect) {
      this.selectedProperties.push(category.Id);
    } else {
      this.selectedProperties.splice(indexItem, 1);
    }
  }

  backPage(){
    this.location.back();
  }
}
