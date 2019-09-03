import {Component, Input, OnInit} from '@angular/core';
import {Place} from '../../../models/Place';
import {SubscriptionType} from '../../../models/SubscriptionType';
import {ModalController} from '@ionic/angular';
import {PlaceService} from '../../../services/place.service';
import {SubscriptionTypeService} from '../../../services/subscription-type.service';
import {ToastService} from '../../../services/toast.service';

@Component({
    selector: 'app-subscription-creator',
    templateUrl: './subscription-creator.component.html',
    styleUrls: ['./subscription-creator.component.scss'],
})
export class SubscriptionCreatorComponent implements OnInit {
    @Input() CurrentPlace: Place;
    Places: Place[];
    PartIndex: number;
    SubTypeList: SubscriptionType[];
    ChosenPlace: Place;
    ChosenSubscriptionType: SubscriptionType;
    ChosenContratType: "FixedContract" | "ContractFree" = "FixedContract";

    constructor(public modal: ModalController, public place: PlaceService, public subType: SubscriptionTypeService, public toast: ToastService) {
        this.PartIndex = 0;
        if(this.place != null) this.ChosenPlace = this.CurrentPlace;
    }

    ngOnInit() {
        this.place.All().subscribe(res => this.Places = res);
        this.subType.All().subscribe(res => this.SubTypeList = res);
    }

    PlaceChosen(place: Place) {
        if(place == null) this.ChosenPlace = null;
        else this.ChosenPlace = place;
    }

    SubChosen(subType: SubscriptionType) {
        if(subType == null) this.ChosenSubscriptionType = null;
        else this.ChosenSubscriptionType = subType;
    }

    GoBack() {
        if(this.PartIndex === 0) this.modal.dismiss();
        this.PartIndex--;
    }

    async GoNext() {
        if(this.PartIndex === 0 && this.ChosenPlace == null) {
            await this.toast.PresentToast("veuillez selectionner un espace de coworking pour continuer");
            return;
        }

        if(this.PartIndex === 1 && this.ChosenSubscriptionType == null) {
            await this.toast.PresentToast("Veuillez selectionner un abonnement pour continuer");
            return;
        }
        this.PartIndex++;
    }

    Finish() {
        if(this.ChosenPlace == null || this.ChosenSubscriptionType == null) {
            console.log("erreur");
            return;
        }
        console.log("creating subscription at " + this.ChosenPlace.Name + " with sub type: " + this.ChosenSubscriptionType.Name);
        this.modal.dismiss({ Place: this.ChosenPlace, SubscriptionType: this.ChosenSubscriptionType, ContractType: this.ChosenContratType });
    }

    OnSubFormatChange(item: CustomEvent<any>) {
        this.ChosenContratType = item.detail.value;
    }

    retour = () => this.modal.dismiss(null);
}
