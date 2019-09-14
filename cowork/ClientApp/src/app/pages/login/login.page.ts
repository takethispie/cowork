import { Component, OnInit } from '@angular/core';
import {NgForm} from "@angular/forms";
import {AuthService} from "../../services/auth.service";
import {Router} from "@angular/router";
import {ToastService} from '../../services/toast.service';
import {HttpErrorResponse} from '@angular/common/http';
import {LoadingService} from '../../services/loading.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {

  constructor(public auth: AuthService, public router: Router, public toast: ToastService, public loading: LoadingService) { }

  ngOnInit() {
  }

  login(form: NgForm) {
    this.auth.Login(form.value.email, form.value.password).subscribe({
      next: response => {
        if(response) {
          form.resetForm();
          this.router.navigate(['']);
        }
      }, error: (err: HttpErrorResponse & Error)=> {
        if(err.status === 404) this.toast.PresentToast("Erreur de connexion");
        if(err.status === 401 || err.status === 500) this.toast.PresentToast("Email ou Mot de passe invalide !");
        this.toast.PresentToast("Une erreur inconnue est survenue");
      }
    });
  }

  Register() {
    this.router.navigate(['Register']);
  }

  LoginTakethispie() {
    this.loading.Loading = true;
    this.auth.Login("takethispie", "ariba1").subscribe({
      next: response => {
        if(response) {
          this.router.navigate(['']);
        }
      }, error: (err: HttpErrorResponse)=> {
        if(err.status === 404) this.toast.PresentToast("Erreur de connexion");
        if(err.status === 401 || err.status === 500) this.toast.PresentToast("Email ou Mot de passe invalide !");
        this.toast.PresentToast("Une erreur inconnue est survenue");
      }, complete: () => this.loading.Loading = false
    });
  }
}
