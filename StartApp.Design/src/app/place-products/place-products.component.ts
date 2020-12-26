import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { DetailsService } from '../service/details.service';

@Component({
  selector: 'app-place-products',
  templateUrl: './place-products.component.html',
  styleUrls: ['./place-products.component.css']
})
export class PlaceProductsComponent implements OnInit {

  placeProducts: Observable<any>;
  placeId;

  constructor(private detailService:DetailsService, private route: ActivatedRoute) {
      this.route.queryParams.subscribe(params => {
        this.placeId = params['placeId'];
      });

      detailService.getPlaceProducts(this.placeId).subscribe(
       res => {
         this.placeProducts = res;
       },
       err => {
           console.error(err);
      });

   }

  ngOnInit(): void {
  }

}
