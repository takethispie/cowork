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

  constructor(private alertCtrl: AlertController, private navCtrl: NavController, public auth: AuthService) {}

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
              localStorage.clear();
              this.auth.User = null;
              this.auth.UserType = null;
              this.auth.Subscription = null;
              this.auth.UserId = -1;
              this.auth.PlaceId = -1;
              this.navCtrl.navigateRoot('Auth');
            }
          },
        ],
      }).then(alert => alert.present())
    }
}
