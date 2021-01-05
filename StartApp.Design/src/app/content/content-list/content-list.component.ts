import { Component, OnInit } from '@angular/core';
import { ContentsService } from 'src/app/service/contents.service';

@Component({
  selector: 'app-content-list',
  templateUrl: './content-list.component.html',
  styleUrls: ['./content-list.component.css']
})
export class ContentListComponent implements OnInit {
  contents;
  constructor(private contentService: ContentsService) { 
    contentService.getAllContent().subscribe(
      (res) => {
        this.contents = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  ngOnInit(): void {
  }

}
