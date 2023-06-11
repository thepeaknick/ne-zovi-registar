import { ComponentFixture, TestBed } from '@angular/core/testing';

import {
  AvatarModule,
  BadgeModule,
  BreadcrumbModule,
  DropdownModule,
  GridModule,
  HeaderModule,
  NavModule,
  SidebarModule,
} from '@coreui/angular';
import { IconSetService } from '@coreui/icons-angular';
import { PageHeaderComponent } from './page-header.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('DefaultHeaderComponent', () => {
  let component: PageHeaderComponent;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PageHeaderComponent],
      imports: [
        GridModule,
        HeaderModule,
        NavModule,
        BadgeModule,
        AvatarModule,
        DropdownModule,
        BreadcrumbModule,
        RouterTestingModule,
        SidebarModule,
      ],
      providers: [IconSetService],
    }).compileComponents();
  });

  beforeEach(() => {});

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
