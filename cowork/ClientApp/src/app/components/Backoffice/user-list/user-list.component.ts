import {Component, OnInit} from '@angular/core';
import {User} from '../../../models/User';
import {UserService} from '../../../services/user.service';
import {UserType} from '../../../models/UserType';
import List from 'linqts/dist/src/list';
import {ModalController} from '@ionic/angular';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import {TableDataHandler} from '../TableDataHandler';


@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss'],
})
export class UserListComponent implements OnInit {
  Fields: Field[] = [];
  DataHandler: TableDataHandler<User>;

  constructor(private userService: UserService, private modalCtrl: ModalController) {
    this.Fields = [
      new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
      new Field(FieldType.Text, "FirstName", "Prénom", ""),
      new Field(FieldType.Text, "LastName", "Nom", ""),
      new Field(FieldType.Select, "Type", "Type d'utilisateur", 0, [
        { Label: "User", Value: 0 },
        { Label: "Staff", Value: 1 },
        { Label: "Admin", Value: 2 }
      ]),
      new Field(FieldType.CheckBox, "IsAStudent", "Est Étudiant", false)
    ];
    this.DataHandler = new TableDataHandler<User>(this.userService, this.modalCtrl, this.Fields, this.CreateModelFromFields);
  }

  ngOnInit() {
    this.DataHandler.Refresh();
  }

  GetUserType(id: number) {
    return UserType[id];
  }
  
  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    const user = new User();
    user.LastName = fieldDic["LastName"][0].Value as string;
    user.FirstName = fieldDic["FirstName"][0].Value as string;
    user.Id = fieldDic["Id"][0].Value as number;
    user.IsAStudent = fieldDic["IsAStudent"][0].Value as boolean;
    user.Type = fieldDic["Type"][0].Value as number;
    return user;
  }
}
