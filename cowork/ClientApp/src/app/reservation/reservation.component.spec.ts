import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { ReservationTabPage } from './reservation.component';

describe('ReservationTabPage', () => {
  let component: ReservationTabPage;
  let fixture: ComponentFixture<ReservationTabPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ReservationTabPage],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(ReservationTabPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
