import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { UsersService } from '../service/users.service';

@Component({
  selector: 'app-user-favorite-places',
  templateUrl: './user-favorite-places.component.html',
  styleUrls: ['./user-favorite-places.component.css']
})
export class UserFavoritePlacesComponent implements OnInit {

  favoritePlaces: Observable<any>;
  userId;

  constructor(private route: ActivatedRoute, private userService:UsersService) { 
    userService.userLoginControl();
    this.userId = userService.getActiveUser().Id;
    userService.getFavoritePlaces(this.userId).subscribe(
      res => {
        this.favoritePlaces = res;
      },
      err => {
          console.error(err);
     });
  }

  ngOnInit(): void {
  }

}
