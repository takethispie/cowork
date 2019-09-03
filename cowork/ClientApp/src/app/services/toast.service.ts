import { Injectable } from '@angular/core';
import {ToastController} from '@ionic/angular';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  constructor(public toast: ToastController) { }

  public async PresentToast(message: string, duration: number = 3000, showCloseButton: boolean = true, closeButtonText: string = "Fermer") {
    const toast = await this.toast.create({
      message,
      duration,
      showCloseButton,
      closeButtonText
    });
    toast.present();
  }
}
