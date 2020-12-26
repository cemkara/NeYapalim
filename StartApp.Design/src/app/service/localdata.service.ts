import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocaldataService {

  constructor() { }
  
  get(name) {
    return localStorage.getItem(name);
  }

  set(name,data){
    localStorage.setItem(name, data);
  }

  control(name){
    return localStorage.getItem(name)==null ? false : true;
  }

  remove(name)
  {
    localStorage.removeItem(name);
  }

  allClear()
  {
    localStorage.clear();
  }
  
}
