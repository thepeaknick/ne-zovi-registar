import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { SearchComponent } from './search/search.component';



@NgModule({
  declarations: [
    LoginComponent,
    SearchComponent
  ],
  imports: [
    CommonModule
  ]
})
export class PagesModule { }
