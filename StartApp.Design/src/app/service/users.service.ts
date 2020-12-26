import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { LocaldataService } from './localdata.service';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private localData: LocaldataService, private router: Router) { }
  activeUser;

  getActiveUser()
  {
      return JSON.parse(this.localData.get("user"));
  }

  activeUserControl()
  {
      if(this.localData.get("user") != null){
        this.router.navigate(['/default']);
      }
      return false;
  }
}
