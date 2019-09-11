import { Component } from '@angular/core';
import {AlertController, NavController} from '@ionic/angular';
import {AuthService} from '../services/auth.service';
import {UserType} from '../models/UserType';

@Component({
  selector: 'app-tabs',
  templateUrl: 'tabs.page.html',
  styleUrls: ['tabs.page.scss']
})
export class TabsPage {

  constructor(private alertCtrl: AlertController, private navCtrl: NavController, public auth: AuthService) {
    console.log(this.auth);
  }

  Disconnect() {
      this.alertCtrl.create({
        header: "Confirmer",
        message: "Se Deconnecter ?",
        buttons: [
          {
            text: "Annuler",
            role: "cancel",
          },
          {
            text: "Oui",
            handler: () => {
              localStorage.setItem('auth_token', null);
              localStorage.setItem('UserId', null);
              localStorage.setItem('UserType', null);
              localStorage.setItem('PlaceId', null);
              this.auth.User = null;
              this.navCtrl.navigateRoot('Auth');
            }
          },
        ],
      }).then(alert => alert.present())
    }
}
