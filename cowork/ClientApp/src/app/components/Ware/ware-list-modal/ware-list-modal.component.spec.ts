import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WareListModalComponent } from './ware-list-modal.component';

describe('WareListModalComponent', () => {
  let component: WareListModalComponent;
  let fixture: ComponentFixture<WareListModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WareListModalComponent ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WareListModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
