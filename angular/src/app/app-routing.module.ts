import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DefaultLayoutComponent, PageLayoutComponent } from './containers';
import { LoginComponent } from './views/pages/login/login.component';
import { SearchComponent } from './views/pages/search/search.component';
import { UsersComponent } from './views/registry/users/users.component';
import { RegUsersComponent } from './views/registry/regusers/regusers.component';
import { MerchantsComponent } from './views/registry/merchants/merchants.component';
import { AdminComponent } from './views/admin/admin.component';
import { HelppageComponent } from './views/helppage/helppage.component';
import { SettingsComponent } from './views/settings/settings.component';
import { ContactComponent } from './views/contact/contact.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'search',
    pathMatch: 'full',
  },
  {
    path: '',
    component: DefaultLayoutComponent,
    data: {
      title: 'Dobro došli u registar "Ne zovi"',
    },
    children: [
      {
        path: 'registry/users',
        component: UsersComponent,
      },
      {
        path: 'registry/regusers',
        component: RegUsersComponent,
      },
      {
        path: 'registry/merchants',
        component: MerchantsComponent,
      },
      {
        path: 'admin',
        component: AdminComponent,
      },
      {
        path: 'settings',
        component: SettingsComponent,
      },
      {
        path: 'help',
        component: HelppageComponent,
      },
      {
        path: 'contact',
        component: ContactComponent,
      },
    ],
  },
  {
    path: '',
    component: PageLayoutComponent,
    data: {
      title: 'Dobro došli u registar "Ne zovi"',
    },
    children: [
      {
        path: 'login',
        component: LoginComponent,
        data: {
          title: 'Prijava',
        },
      },
      {
        path: 'search',
        component: SearchComponent,
        data: {
          title: 'Pretraga telefonskog broja',
        },
      },
    ],
  },
  {
    path: '**',
    redirectTo: 'search',
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: 'top',
      anchorScrolling: 'enabled',
      initialNavigation: 'enabledBlocking',
      // relativeLinkResolution: 'legacy'
    }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
