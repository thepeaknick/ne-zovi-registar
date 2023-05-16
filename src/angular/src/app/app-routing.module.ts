import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DefaultLayoutComponent } from './containers';
import { LoginComponent } from './views/pages/login/login.component';
import { SearchComponent } from './views/pages/search/search.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: '',
    component: DefaultLayoutComponent,
    data: {
      title: 'Dobro došli u registar "Ne zovi"'
    },
    children: [
      {
        path: 'regusers',
        loadChildren: () =>
          import('./views/registry/regusers/regusers.module').then((m) => m.RegUsersModule)
      },
      {
        path: 'users',
        loadChildren: () =>
          import('./views/registry/users/users.module').then((m) => m.UsersModule)
      },
      {
        path: 'admin',
        loadChildren: () =>
          import('./views/admin/admin.module').then((m) => m.AdminModule)
      },
      {
        path: 'settings',
        loadChildren: () =>
          import('./views/settings/settings.module').then((m) => m.SettingsModule)
      },
    ]
  },
  {
    path: 'login',
    component: LoginComponent,
    data: {
      title: 'Prijava'
    }
  },
  {
    path: 'search',
    component: SearchComponent,
    data: {
      title: 'Pretraga telefonskog broja'
    }
  },
  {
    path: '**', 
    redirectTo: 'search'
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: 'top',
      anchorScrolling: 'enabled',
      initialNavigation: 'enabledBlocking'
      // relativeLinkResolution: 'legacy'
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
