import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { UsersService } from '../service/users.service';

@Component({
  selector: 'app-user-visited',
  templateUrl: './user-visited.component.html',
  styleUrls: ['./user-visited.component.css']
})
export class UserVisitedComponent implements OnInit {

  visitedPlaces: Observable<any>;
  userId;

  constructor(private route: ActivatedRoute, private userService:UsersService) { 
    userService.userLoginControl();
    this.userId = userService.getActiveUser().Id;
    userService.getVisitedPlaces(this.userId).subscribe(
      res => {
        this.visitedPlaces = res;
        console.log(res);
      },
      err => {
          console.error(err);
     });
  }

  ngOnInit(): void {
  }

}
