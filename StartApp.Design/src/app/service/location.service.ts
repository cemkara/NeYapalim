import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LocaldataService } from './localdata.service';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';

@Injectable()
export class LocationService {

  constructor(private http: HttpClient, private localdata: LocaldataService,private baseService: BaseService) { }
  
  findMe() {
    if(!this.localdata.control("districtName"))
    {
      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition((position) => {
          this.getMyDistrictByGoogle(position.coords.latitude,position.coords.longitude);
        });
      } else {
        console.log("Geolocation is not supported by this browser.");
      }
    }
  }

  getMyDistrictByGoogle(lat,lng)
  {
    this.http.get('https://maps.googleapis.com/maps/api/geocode/json?latlng='+lat+','+lng+'&key=' + environment.googleApiKey)
    .subscribe(
      res => {
          var district = res['results'][6]['address_components'][0]['long_name'];
          var city = res['results'][6]['address_components'][1]['long_name'];
          console.log(district,city);
          this.districtSearch(district, city);
          
        },
      err => {
          console.error(err); 
      });;
  }

  districtSearch(district,city)
  {
    this.baseService.post('Districts/SearchDistricts',
        {
          'District' : district,
          'City' : city
        }).subscribe(
        res => {
          if(res['isDistrict'])
          {
            localStorage.setItem("districtId",res['district']['Id']);
            localStorage.setItem("districtName",res['district']['Name']+", "+res['city']['Name']);
          }
          else{
            localStorage.setItem("districtName",res['district']['Name']);
            localStorage.setItem("cityId",res['city']['Id']);
          }

          localStorage.setItem("isDistrict",res['isDistrict']);
        },
        err => {
           console.error(err);
        });;
  }
  
  getDistrict(): Observable<any> {
    return this.baseService.get('Districts/GetDistrict');
  }

}
