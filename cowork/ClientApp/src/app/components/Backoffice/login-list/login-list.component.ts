import {Component, OnInit} from '@angular/core';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import {TableDataHandler} from '../TableDataHandler';
import {UserLoginService} from '../../../services/user-login.service';
import {ModalController} from '@ionic/angular';
import {Login} from '../../../models/Login';
import List from 'linqts/dist/src/list';

@Component({
    selector: 'app-login-list',
    templateUrl: './login-list.component.html',
    styleUrls: ['./login-list.component.scss'],
})
export class LoginListComponent implements OnInit {

    Fields: Field[] = [];
    DataHandler: TableDataHandler<Login>;

    constructor(loginService: UserLoginService, modalCtrl: ModalController) {
        this.Fields = [
            new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
            new Field(FieldType.Text, "Email", "Email", ""),
            new Field(FieldType.Text, "Password", "Mot de passe", ""),
            new Field(FieldType.Number, "UserId", "Id utilisateur", -1)
        ];
        this.DataHandler = new TableDataHandler<Login>(loginService, modalCtrl, this.Fields, this.CreateModelFromFields);
    }

    ngOnInit() {
        this.DataHandler.Refresh();
    }

    CreateModelFromFields(fields: Field[]) {
        const fieldDic = new List(fields).GroupBy(f => f.Name);
        const login = new Login();
        login.Id = fieldDic["Id"][0].Value as number;
        login.Email = fieldDic["Email"][0].Value as string;
        login.UserId = fieldDic["UserId"][0].Value as number;
        login.Password = fieldDic["Password"][0].Value as string;
        if(login.Password == null) login.Password = "";
        return login;
    }

}
