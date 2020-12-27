import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';
import { UsersService } from '../service/users.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  activeUser;
  lastComments:Observable<any>;

  constructor(private userService:UsersService) { 
    userService.userLoginControl(); 
    this.activeUser = userService.getActiveUser();
  }

  ngOnInit(): void {
  }

}
