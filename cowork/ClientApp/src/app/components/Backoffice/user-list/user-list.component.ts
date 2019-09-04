import { Component, OnInit } from '@angular/core';
import {User} from '../../../models/User';
import {UserService} from '../../../services/user.service';
import {UserType} from '../../../models/UserType';
import {Field} from "../../dynamic-form-builder/Field";
import List from "linqts/dist/src/list";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";
import {ModalController} from "@ionic/angular";

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss'],
})
export class UserListComponent implements OnInit {
    data: User[] = [];
    fields: Field[] = [];

  constructor(private userService: UserService, private modalCtrl: ModalController) {
    this.fields = [
      { Type: "Text", Name: "FirstName", Value: "", Label: "Prénom" },
      { Type: "Text", Name: "LastName", Value: "", Label: "Nom"},
      { Type: "Text", Name: "Email", Value: "", Label: "Email"},
      { Type: "Select", Name: "Type", Value: "User", Label: "Type d'utilisateur", Options: [
          { Label: "User", Value: 0 },
          { Label: "Staff", Value: 1 },
          { Label: "Admin", Value: 2 }
        ]
      },
      { Type: "Checkbox", Name: "IsAStudent", Value: false, Label: "Est Étudiant"}
    ];
  }

  ngOnInit() {
    this.userService.All().subscribe({
      next: res => {
        this.data = res;
      }
    })
  }

  Refresh() {

  }

  DoInifiniteLoad(event) {

  }

  GetUserType(id: number) {
    return UserType[id];
  }
  
  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    let user = new User();
    user.LastName = fieldDic["LastName"][0].Value as string;
    user.FirstName = fieldDic["FirstName"][0].Value as string;
    user.Id = -1;
    user.IsAStudent = fieldDic["IsAStudent"][0].Value as boolean;
    user.Email = fieldDic["Email"][0].Value as string;
    return user;
  }

  async AddItem() {
    const modal = await this.modalCtrl.create({
      component: DynamicFormModalComponent,
      componentProps: { Fields: this.fields }
    });
    modal.onDidDismiss().then(res => {
      if(res.data == null) return;
      const user = this.CreateModelFromFields(this.fields);
      this.userService.Create(user).subscribe({
        next: value => this.ngOnInit(),
        error: err => console.log(err)
      });
    });
    await modal.present();
  }

  async Delete(id: number) {
    this.userService.Delete(id).subscribe({
      next: value => this.ngOnInit(),
      error: err => {}
    });
  }
}
