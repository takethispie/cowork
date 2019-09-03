import {Component, EventEmitter, OnInit, Output, Input} from '@angular/core';
import {SubscriptionTypeService} from '../../../services/subscription-type.service';
import {SubscriptionType} from '../../../models/SubscriptionType';
import {AlertController, ModalController} from '@ionic/angular';
import {ToastService} from '../../../services/toast.service';

@Component({
  selector: 'app-subscription-picker',
  templateUrl: './subscription-picker.component.html',
  styleUrls: ['./subscription-picker.component.scss'],
})
export class SubscriptionPickerComponent implements OnInit {
  public SubscriptionTypeList: SubscriptionType[];

  @Input() SelectedId: number;
  PartIndex: number = 0;
  ChosenSubscriptionType: SubscriptionType;
  ChosenContratType: "FixedContract" | "ContractFree" = "FixedContract";

  constructor(public subType: SubscriptionTypeService, public modal: ModalController,
              public alertController: AlertController, public toast: ToastService) { }

  ngOnInit() {
    this.subType.All().subscribe(res => this.SubscriptionTypeList = res);
  }

  Refresh() {
    this.subType.All().subscribe(res => this.SubscriptionTypeList = res);
  }

  public async Choose(item: SubscriptionType) {
    this.ChosenSubscriptionType = item;
  }

  retour() {
    this.modal.dismiss();
  }

  async GoNext() {
    if(this.PartIndex === 0 && this.ChosenSubscriptionType == null) {
      await this.toast.PresentToast("veuillez selectionner un espace de coworking pour continuer");
      return;
    }

    if(this.PartIndex === 1 && this.ChosenContratType == null) {
      await this.toast.PresentToast("Veuillez selectionner un abonnement pour continuer");
      return;
    }
    this.PartIndex++;
  }

  Finish() {
    this.modal.dismiss({ SubscriptionType: this.ChosenSubscriptionType, ContractType: this.ChosenContratType });
  }

  OnSubFormatChange(item: CustomEvent<any>) {
    this.ChosenContratType = item.detail.value;
  }
}
