import { Component, OnInit } from '@angular/core';
import {User} from '../../../models/User';
import {UserService} from '../../../services/user.service';
import {UserType} from '../../../models/UserType';
import {Field} from "../../dynamic-form-builder/Field";
import List from "linqts/dist/src/list";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";
import {ModalController} from "@ionic/angular";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";

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
      { Type: "ReadonlyText", Name: "Id", Label: "Id", Value: "-1"},
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
    user.Id = fieldDic["Id"][0].Value as number;
    user.IsAStudent = fieldDic["IsAStudent"][0].Value as boolean;
    user.Email = fieldDic["Email"][0].Value as string;
    user.Type = fieldDic["Type"][0].Value as number;
    return user;
  }
  
  async UpdateItem(user: User, fields: Field[]) {
    const fieldlist = new List(fields).Select(field => {
      switch (field.Name) {
        case "Id": 
          field.Value = user.Id;
          return field;
          
        case "LastName":
          field.Value = user.LastName;
          return field;
          
        case "FirstName":
          field.Value = user.FirstName;
          return field;
          
        case "IsAStudent":
          field.Value = user.IsAStudent;
          return field;
          
        case "Email":
          field.Value = user.Email;
          return field;
          
        case "Type":
          field.Value = user.Type;
          return field;
          
        default: return field;
      }
    });
    await this.OpenModal("Update", { Fields: fieldlist.ToArray()}, this.userService);
  }

  async AddItem() {
    await this.OpenModal("Create", { Fields: this.fields }, this.userService);
  }
  
  async OpenModal(mode: "Update" | "Create" ,componentProps: any, apiService: { http: HttpClient, Create: (user: User) => Observable<number>, Update: (user: User) => Observable<number>}) {
    const modal = await this.modalCtrl.create({
      component: DynamicFormModalComponent,
      componentProps
    });
    modal.onDidDismiss().then(res => {
      if(res.data == null) return;
      const user = this.CreateModelFromFields(this.fields);
      const observer = {
        next: value => this.ngOnInit(),
        error: err => console.log(err)
      };
      mode === "Update" ? apiService.Update(user).subscribe(observer) : apiService.Create(user).subscribe(observer);
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
