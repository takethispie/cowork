import { Component, OnInit } from '@angular/core';
import {User} from '../../../models/User';
import {UserService} from '../../../services/user.service';
import {UserType} from '../../../models/UserType';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss'],
})
export class UserListComponent implements OnInit {
    data: User[];

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userService.All().subscribe({
      next: res => {
        this.data = res;
      }
    })
  }

  Load() {

  }

  Refresh() {

  }

  DoInifiniteLoad(event) {

  }

  GetUserType(id: number) {
    return UserType[id];
  }

}
