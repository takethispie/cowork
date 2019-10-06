import { Component, OnInit } from '@angular/core';
import {NgForm} from '@angular/forms';
import { NavController } from '@ionic/angular';
import {AuthService} from '../../services/auth.service';
import { User } from '../../models/User';
import {ToastService} from '../../services/toast.service';
import {LoadingService} from '../../services/loading.service';
import { format } from 'date-fns';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {

  constructor(public authService: AuthService, public navCtrl: NavController, public toast: ToastService, public loading: LoadingService) {
  }

  ngOnInit() {}

  register(form: NgForm) {
    if(form.value.password.length < 5) {
      this.toast.PresentToast("le mot de pass est trop petit !")
    } else {
      if(form.value.password === form.value.confirmPassword) {
        const user = new User();
        user.FirstName = form.value.firstName;
        user.LastName = form.value.lastName;
        this.loading.Loading = true;
        this.authService.Register(user, form.value.password, form.value.email).subscribe( {
          next: async () => {
            this.toast.PresentToast("Création du compte réussie");
            form.resetForm();
            this.loading.Loading = false;
            this.authService.Login(form.value.email, form.value.password).subscribe({
              next: res => {
                this.navCtrl.navigateRoot('Auth');
              },
              error: err => console.log(err)
            });
          },
          error: async error => {
            this.toast.PresentToast("Une Erreur est survenue");
            form.value.password = "";
            this.loading.Loading = false;
          }
        });
      } else this.toast.PresentToast("Les mots de passe sont différents !");
    }
  }

  GoBack() {
    this.navCtrl.back();
  }
}
