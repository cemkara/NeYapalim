import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';

@Injectable()
export class MainCategoryService {
  
 constructor(private baseService: BaseService) { }
 getMainCategories() :Observable<any> {
     return this.baseService.get('MainCategories/GetMainCategories');
 }
}
