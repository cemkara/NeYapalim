import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaceProductsComponent } from './place-products.component';

describe('PlaceProductsComponent', () => {
  let component: PlaceProductsComponent;
  let fixture: ComponentFixture<PlaceProductsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlaceProductsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlaceProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
