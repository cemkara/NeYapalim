import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { UsersService } from '../service/users.service';

@Component({
  selector: 'app-user-comments',
  templateUrl: './user-comments.component.html',
  styleUrls: ['./user-comments.component.css']
})
export class UserCommentsComponent implements OnInit {

  userComments: Observable<any>;
  userId;

  constructor(private route: ActivatedRoute, private userService:UsersService) {
    this.route.queryParams.subscribe(params => {
      this.userId = params['userId'];
    });

    userService.getComments(this.userId).subscribe(
      res => {
        this.userComments = res;
      },
      err => {
          console.error(err);
     });

  }

  ngOnInit(): void {
  }

}
