import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { MainCategoryService } from '../service/main-category.service';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {

  showNavigationArrows = false;
  showNavigationIndicators = false;
  
  constructor(private http: HttpClient, private mainCategoryService:MainCategoryService) { 
    
  } 
  images = [944, 1011, 984].map((n) => `https://picsum.photos/id/${n}/900/500`);

  ngOnInit(): void { 
    
  } 
  
}
