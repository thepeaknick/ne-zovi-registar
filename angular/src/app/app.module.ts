import { RECAPTCHA_SETTINGS, RecaptchaFormsModule, RecaptchaModule, RecaptchaSettings } from 'ng-recaptcha';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { LocationStrategy, PathLocationStrategy } from '@angular/common';
import { BrowserModule, Title } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
// import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PopoverModule } from '@coreui/angular';
import { TableModule } from '@coreui/angular';

import {
  HttpClientModule,
  HTTP_INTERCEPTORS,
  HttpClient,
} from '@angular/common/http';
import { CommonModule } from '@angular/common';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

// Import containers
import {
  DefaultFooterComponent,
  DefaultHeaderComponent,
  DefaultLayoutComponent,
  PageFooterComponent,
  PageHeaderComponent,
  PageLayoutComponent,
} from './containers';

import { UsersComponent } from './views';

import {
  AvatarModule,
  BadgeModule,
  BreadcrumbModule,
  ButtonGroupModule,
  ButtonModule,
  CardModule,
  DropdownModule,
  FooterModule,
  FormModule,
  GridModule,
  HeaderModule,
  ListGroupModule,
  NavModule,
  ProgressModule,
  SharedModule,
  SidebarModule,
  TabsModule,
  UtilitiesModule,
  OffcanvasModule,
  ModalModule,
} from '@coreui/angular';

import { IconModule, IconSetService } from '@coreui/icons-angular';
import { UserService } from './domain/services/user.service';
import { RegUserService } from './domain/services/reguser.service';
import { NeZoviHttpInterceptor } from './domain/services/http-interceptor';
import { AppConfiguration } from './domain/services/app-configuration.service';
import { AngularSvgIconModule } from 'angular-svg-icon';
import { SearchComponent } from './views/pages/search/search.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './views/pages/login/login.component';
import { MerchantsComponent } from './views/registry/merchants/merchants.component';
import { ContactComponent } from './views/contact/contact.component';
import { SettingsComponent } from './views/settings/settings.component';

const APP_CONTAINERS = [
  DefaultFooterComponent,
  DefaultHeaderComponent,
  DefaultLayoutComponent,

  PageLayoutComponent,
  PageHeaderComponent,
  PageFooterComponent,
];

@NgModule({
  declarations: [
    AppComponent,
    UsersComponent,
    ...APP_CONTAINERS,
    HomeComponent,
    SearchComponent,
    LoginComponent,
    MerchantsComponent,
    ContactComponent,
    SettingsComponent
  ],
  imports: [
    RecaptchaModule,
    RecaptchaFormsModule,
    HttpClientModule,
    AngularSvgIconModule.forRoot(),
    BrowserModule,
    CommonModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    AvatarModule,
    BreadcrumbModule,
    FooterModule,
    DropdownModule,
    GridModule,
    HeaderModule,
    SidebarModule,
    IconModule,
    NavModule,
    ButtonModule,
    FormModule,
    UtilitiesModule,
    OffcanvasModule,
    ModalModule,
    ButtonGroupModule,
    ReactiveFormsModule,
    FormsModule,
    SidebarModule,
    SharedModule,
    TabsModule,
    ListGroupModule,
    ProgressModule,
    BadgeModule,
    ListGroupModule,
    CardModule,
    PopoverModule,
    TableModule
  ],
  providers: [
    {
      provide: RECAPTCHA_SETTINGS,
      useValue: {
        siteKey: '6LdNNmQmAAAAAKEU4pIxQ33-eNhyyGeZ1_CT2IO6',
      } as RecaptchaSettings,
    },
    { provide: LocationStrategy, useClass: PathLocationStrategy },
    { provide: UserService, useClass: UserService },
    { provide: RegUserService, useClass: RegUserService },
    { provide: 'BASE_URL', useFactory: getBaseUrl },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: NeZoviHttpInterceptor,
      multi: true,
    },
    IconSetService,
    AppConfiguration,
    {
      provide: APP_INITIALIZER,
      useFactory: AppConfigurationFactory,
      deps: [AppConfiguration, HttpClient],
      multi: true,
    },
    Title,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

export function AppConfigurationFactory(appConfig: AppConfiguration) {
  return () => appConfig.ensureInit();
}
