import { Component, OnInit, Input } from '@angular/core';
import { MainCategoryService } from '../service/main-category.service';
import { Observable } from 'rxjs';
import { LocationService } from '../service/location.service';
import { LocaldataService } from '../service/localdata.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-default',
  templateUrl: './default.component.html',
  styleUrls: ['./default.component.css']
})
export class DefaultComponent implements OnInit {

  model = {
    left: true,
    middle: false,
    right: false
};

focus;
focus1;

@Input()
mainCategories: Observable<any>;
keyword = 'name';
data;


selectEvent(item) {
  localStorage.setItem("districtId",item["id"]);
  localStorage.setItem("districtName",item["name"]);
}

onChangeSearch(val: string) {
  // fetch remote data from here
  // And reassign the 'data' which is binded to 'data' property.
}

onFocused(e){
  // do something when input is focused
}

constructor(private mainCategoryService: MainCategoryService, private locationService: LocationService, private localdata: LocaldataService) {

    // localdata.allClear();
    this.locationService.findMe();
    mainCategoryService.getMainCategories().subscribe(
        res => {
            this.mainCategories = res;
        },
        err => {
            console.error(err);
        }
    );

    locationService.getDistrict().subscribe(
        res => {
            this.data=res;
        },
        err => {
            console.error(err);
        }
    );

    
}

ngOnInit() {
 }

}
