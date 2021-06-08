import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { EntityUiComponent } from './entity-ui.component';

describe('EntityUiComponent', () => {
  let component: EntityUiComponent;
  let fixture: ComponentFixture<EntityUiComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ EntityUiComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EntityUiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
