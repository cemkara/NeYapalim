import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { DetailsService } from '../service/details.service';

@Component({
  selector: 'app-place-comments',
  templateUrl: './place-comments.component.html',
  styleUrls: ['./place-comments.component.css']
})
export class PlaceCommentsComponent implements OnInit {

  placeComments: Observable<any>;
  placeId;

  constructor(private detailService:DetailsService, private route: ActivatedRoute) {
      this.route.queryParams.subscribe(params => {
        this.placeId = params['placeId'];
      });

      detailService.getPlaceComments(this.placeId).subscribe(
       res => {
         this.placeComments = res;
       },
       err => {
           console.error(err);
      });

   }

  ngOnInit(): void {
  }

}
