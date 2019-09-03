import { Component, OnInit } from '@angular/core';
import {NgForm} from '@angular/forms';
import { ToastController, NavController } from '@ionic/angular';
import {AuthService} from '../services/auth.service';
import { User } from '../models/User';
import {ToastService} from '../services/toast.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {

  constructor(public authService: AuthService, public navCtrl: NavController, public toast: ToastService) {
  }

  ngOnInit() {}

  async register(form: NgForm) {
    if(form.value.password === form.value.confirmPassword) {
      const user = new User();
      user.Email = form.value.email;
      user.FirstName = form.value.firstName;
      user.LastName = form.value.lastName;
      this.authService.Register(user, form.value.password).subscribe( async result => {
        this.toast.PresentToast("Création du compte réussie");
        form.resetForm();
        this.navCtrl.navigateRoot('Auth');
      },async error => {
        this.toast.PresentToast("Une Erreur est survenue");
      });
    } else {
      this.toast.PresentToast("Les mots de passe sont différents !");
    }
  }

  GoBack() {
    this.navCtrl.back();
  }
}
