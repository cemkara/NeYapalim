import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserVisitedComponent } from './user-visited.component';

describe('UserVisitedComponent', () => {
  let component: UserVisitedComponent;
  let fixture: ComponentFixture<UserVisitedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserVisitedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserVisitedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
