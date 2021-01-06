import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/service/category.service';

@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html',
  styleUrls: ['./categories-list.component.css']
})
export class CategoriesListComponent implements OnInit {
  allCategories;
  constructor(private categoryService: CategoryService) { 
    categoryService.getAllCategories().subscribe(
      (res) => {
        this.allCategories = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  ngOnInit(): void {
  }

}
