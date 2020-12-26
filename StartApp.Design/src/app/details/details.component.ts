import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { DetailsService } from '../service/details.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  place: Observable<any>;
  placeProducts: Observable<any>;
  placeId;

  constructor(private detailService:DetailsService, private route: ActivatedRoute) { 
    this.route.queryParams.subscribe(params => {
      if(params['placeId']=="" || params['placeId']==undefined)
      {
        this.placeId = "0";
      }
      else{
        this.placeId = params['placeId'];
      }
    });

    detailService.getPlace(this.placeId).subscribe(
      res => {
        this.place = res;
      },
      err => {
          console.error(err);
      });

      detailService.getPlaceProductMain(this.placeId).subscribe(
        res => {
          this.placeProducts = res;
        },
        err =>{
          console.error(err);
        }
      )
  }

  openMaps()
  {
    window.open("https://maps.google.com/maps?daddr={{place.Latitude}},{{place.Longitude}}&amp;ll=");
  }

  ngOnInit(): void {
  }

}
