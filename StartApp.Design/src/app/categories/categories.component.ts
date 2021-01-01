import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { CategoryService } from '../service/category.service';
import { LocaldataService } from '../service/localdata.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {

  categories: Observable<any>;
  mainCategoryId;
  
  constructor(private categoryService:CategoryService, private route: ActivatedRoute, private localData: LocaldataService) {
    
    this.route.queryParams.subscribe(params => {
      this.mainCategoryId = params['mainCategoryId'];
    });
    categoryService.getCategories(this.mainCategoryId).subscribe(
      res => {
          this.categories = res;
      },
      err => {
          console.error(err);
      });
   }

  changeSelected(index)
  {
    var item = this.categories[index];
    if(item.tempSelect)
      item.color = "f0f0f0";
    else
      item.color = "2df309";
    
      item.tempSelect = !item.tempSelect;
      this.setSelectedCategories(item);
  }

  selectedCategories: string[] = [];
  setSelectedCategories(category)
  {
    const indexItem = this.selectedCategories.indexOf(category.Id); 
    if(category.tempSelect)
    {
      this.selectedCategories.push(category.Id);
    }
    else
    {
      this.selectedCategories.splice(indexItem, 1);
    }
  }
  
  ngOnInit(): void {
  }
}
