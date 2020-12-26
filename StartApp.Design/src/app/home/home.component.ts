import { Component, OnInit, Input } from '@angular/core';
import { MainCategoryService } from '../service/main-category.service';
import { Observable } from 'rxjs';
import { LocationService } from '../service/location.service';
import { LocaldataService } from '../service/localdata.service';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {
    model = {
        left: true,
        middle: false,
        right: false
    };

    focus;
    focus1;
    
    @Input()
    mainCategories: Observable<any>;
  
    constructor(private mainCategoryService: MainCategoryService, private locationService: LocationService, private localdata: LocaldataService) {

        this.locationService.findMe();
        mainCategoryService.getMainCategories().subscribe(
            res => {
                this.mainCategories = res;
            },
            err => {
                console.error(err);
            }
        );
    }

    ngOnInit() {
     }
   
}
