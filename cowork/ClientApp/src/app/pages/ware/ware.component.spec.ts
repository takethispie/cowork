import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WareComponent } from './ware.component';

describe('WareComponent', () => {
  let component: WareComponent;
  let fixture: ComponentFixture<WareComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WareComponent ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WareComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
