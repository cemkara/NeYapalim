import { Component, OnInit } from '@angular/core';
import { async } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { DetailsService } from '../service/details.service';

@Component({
  selector: 'app-place-details',
  templateUrl: './place-details.component.html',
  styleUrls: ['./place-details.component.css']
})
export class PlaceDetailsComponent implements OnInit {

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
