import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/service/category.service';

@Component({
  selector: 'app-main-categories',
  templateUrl: './main-categories.component.html',
  styleUrls: ['./main-categories.component.css']
})
export class MainCategoriesComponent implements OnInit {

  mainCategories;
  constructor(private categoryService: CategoryService) {
    categoryService.getMainCategories().subscribe(
      (res) => {
        this.mainCategories = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }
  ngOnInit(): void {
  }

}
