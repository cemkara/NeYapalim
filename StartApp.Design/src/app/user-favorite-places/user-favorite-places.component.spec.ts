import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserFavoritePlacesComponent } from './user-favorite-places.component';

describe('UserFavoritePlacesComponent', () => {
  let component: UserFavoritePlacesComponent;
  let fixture: ComponentFixture<UserFavoritePlacesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserFavoritePlacesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserFavoritePlacesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
