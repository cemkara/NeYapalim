import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.css']
})
export class CommentListComponent implements OnInit {

  @Input()
  comments: Observable<any>;
  
  constructor() {
    console.log('myCustomComponent');
   }

  ngOnInit(): void {
  }

}
