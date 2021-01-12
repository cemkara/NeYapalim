import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { LocaldataService } from '../service/localdata.service';
import { SuggestionsService } from '../service/suggestions.service';

@Component({
  selector: 'app-suggestions',
  templateUrl: './suggestions.component.html',
  styleUrls: ['./suggestions.component.css']
})
export class SuggestionsComponent implements OnInit {

  suggestionPlaces: Observable<any>;
  categories;
  properties;
  suggestionCount: 0;
  
  constructor(private suggestionService:SuggestionsService, private route: ActivatedRoute, private localData: LocaldataService) {
    this.route.queryParams.subscribe(params => {
      if(params['categories']=="")
      {
        this.categories=0;
      }
      else{
        this.categories = params['categories'];
      }

      if(params['properties'] == "")
      {
        this.properties=0;
      }
      else{
        this.properties = params['properties'];  
      }
    });

    suggestionService.getSuggestions(this.categories,this.properties,localData.get("districtId")).subscribe(
      res => {
          this.suggestionPlaces = res;
      },
      err => {
          console.error(err);
      });
   }

  ngOnInit(): void {
  }

}
