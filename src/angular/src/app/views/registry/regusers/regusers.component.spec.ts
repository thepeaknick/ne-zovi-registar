import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegUsersComponent } from './regusers.component';

describe('RegusersComponent', () => {
  let component: RegUsersComponent;
  let fixture: ComponentFixture<RegUsersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegUsersComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
