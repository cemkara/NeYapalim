import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-autocomplate',
  templateUrl: './autocomplate.component.html',
  styleUrls: ['./autocomplate.component.css']
})
export class AutocomplateComponent implements OnInit {

  keyword = 'name';
  data = [{
       id: 1,
       name: 'Usa'
     },
     {
       id: 2,
       name: 'England'
     }];
 
 
  selectEvent(item) {
    // do something with selected item
  }
 
  onChangeSearch(val: string) {
    // fetch remote data from here
    // And reassign the 'data' which is binded to 'data' property.
  }
  
  onFocused(e){
    // do something when input is focused
  }

  constructor() { }

  ngOnInit() {
  }
 
  

}
