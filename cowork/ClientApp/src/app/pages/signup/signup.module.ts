import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import {SignupComponent} from './signup.component';

@NgModule({
    declarations: [SignupComponent],
    entryComponents: [SignupComponent],
    imports: [
        CommonModule,
        FormsModule,
        IonicModule
    ],
    exports: [SignupComponent]
  })
  export class SignupComponentModule {}
