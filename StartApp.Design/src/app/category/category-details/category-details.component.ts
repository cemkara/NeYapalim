import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from 'src/app/service/category.service';

@Component({
  selector: 'app-category-details',
  templateUrl: './category-details.component.html',
  styleUrls: ['./category-details.component.css']
})
export class CategoryDetailsComponent implements OnInit {

  categoryId;
  category;
  categoryPlace;
  constructor(
    private categoryService: CategoryService,
    private route: ActivatedRoute
  ) {
    this.route.queryParams.subscribe((params) => {
      if (params["categoryId"] == "" || params["categoryId"] == undefined) {
        this.categoryId = "0";
      } else {
        this.categoryId = params["categoryId"];
      }
    });

    categoryService.getPlacesByCategoryId(this.categoryId).subscribe(
      (res) => {
        this.categoryPlace = res;
      },
      (err) => {
        console.error(err);
      }
    )

    categoryService.getCategory(this.categoryId).subscribe(
      (res) => {
        this.category = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  ngOnInit(): void {
  }

}
