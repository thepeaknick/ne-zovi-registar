import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { NeZoviService } from 'src/app/domain/services/nezovi.service';
import { User } from '../../../domain/model/user';


@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  constructor(private neZoviService: NeZoviService) {

  }

  ngOnInit() {
    let users: User[] = this.neZoviService.getAllUsers();
  }

}
