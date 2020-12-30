import { Component, OnInit } from '@angular/core';
import { async } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { DetailsService } from '../service/details.service';
import { UsersService } from '../service/users.service';

@Component({
  selector: 'app-place-details',
  templateUrl: './place-details.component.html',
  styleUrls: ['./place-details.component.css']
})
export class PlaceDetailsComponent implements OnInit {

  place: Observable<any>;
  placeProducts: Observable<any>;
  placeCommentsTopTen: Observable<any>;
  placeId;
  user=null;
  userFavorite = false;
  userVisited = false;
  

  constructor(private detailService:DetailsService, private route: ActivatedRoute, private userService:UsersService) { 
    this.route.queryParams.subscribe(params => {
      if(params['placeId']=="" || params['placeId']==undefined)
      {
        this.placeId = "0";
      }
      else{
        this.placeId = params['placeId'];
      }
    });

    if(userService.getActiveUser() != null)
    {
      this.user = userService.getActiveUser();
      userService.userFavoritePlaceControl(this.user.Id,this.placeId).subscribe(
        res=>{
          if(res)
            this.userFavorite =true;
        },
        err=>{
          console.error(err);
        }
      );

      userService.userVisitedPlaceControl(this.user.Id,this.placeId).subscribe(
        res=>{
          if(res)
            this.userVisited = true;
        },
        err=>{
          console.error(err);
        }
      )
    }

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
      });

    detailService.getPlaceCommentsTopTen(this.placeId).subscribe(
      res => {
        this.placeCommentsTopTen = res;
      },
      err =>{
        console.error(err);
    });
  }

  openMaps()
  {
    window.open("https://maps.google.com/maps?daddr={{place.Latitude}},{{place.Longitude}}&amp;ll=");
  }
  ngOnInit(): void {
  }

  addToFavorite()
  {
      if(this.user!=null){
      this.userService.addToFavorite(this.user.Id,this.placeId).subscribe(
        res=>{
          this.userFavorite = true;
        },
        err=>{
          console.error(err);
        }
      );
    }
    else{
      console.log("giriş yap");
    }
  }
  removeToFavorite()
  {
    if(this.user!=null){
      this.userService.removeToFavorite(this.user.Id,this.placeId).subscribe(
        res=>{
          this.userFavorite = false;
        },
        err=>{
          console.error(err);
        }
      );
    }
    else{
      console.log("giriş yap");
    }
  }
  addToVisited()
  {
      if(this.user!=null){
      this.userService.addUserVisitedPlace(this.user.Id,this.placeId).subscribe(
        res=>{
          this.userVisited = true;
        },
        err=>{
          console.error(err);
        }
      );
    }
    else{
      console.log("giriş yap");
    }
  }
}
