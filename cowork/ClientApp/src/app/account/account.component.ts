import {Component} from '@angular/core';
import {AuthService} from '../services/auth.service';
import {SubscriptionService} from '../services/subscription.service';
import {Subscription} from '../models/Subscription';
import {SubscriptionType} from '../models/SubscriptionType';
import {DateTime} from 'luxon';
import {ModalController} from '@ionic/angular';
import {SubscriptionPickerComponent} from '../components/Subscription/subscription-picker/subscription-picker.component';
import {ToastService} from '../services/toast.service';
import {SubscriptionCreatorComponent} from '../components/Subscription/subscription-creator/subscription-creator.component';
import {Place} from '../models/Place';
import {PlacePickerComponent} from '../components/Place/place-picker/place-picker.component';
import {MealBookingService} from '../services/meal-booking.service';
import {MealBooking} from '../models/MealBooking';
import {RoomBooking} from '../models/RoomBooking';
import {RoomBookingService} from '../services/room-booking.service';
import {TimeSlotService} from '../services/time-slot.service';
import {TimeSlot} from '../models/TimeSlot';
import {UserType} from '../models/UserType';

@Component({
    selector: 'app-tab1',
    templateUrl: 'account.component.html',
    styleUrls: ['account.component.scss']
})
export class AccountComponent {

    public userSub: Subscription;
    public userMeals: MealBooking[];
    public roomBookings: RoomBooking[];

    constructor(public auth: AuthService, public sub: SubscriptionService, public modal: ModalController, public toast: ToastService,
                public mealReservationService: MealBookingService, public roomBookingService: RoomBookingService,
                public timeSlotService: TimeSlotService) {
    }

    ionViewWillEnter() {
        if(this.auth.User.Type === UserType.User) {
            //recupere l'abonnement et le type d'abonnement de l'utilisateur ainsi que son espace de coworking
            this.sub.OfUser(this.auth.User.Id).subscribe(res => this.userSub = res);
            this.roomBookingService.AllOfUser(this.auth.User.Id).subscribe(res => {
                this.roomBookings = res.filter(booking => booking.Start >= DateTime.local());
            });
            this.mealReservationService.AllFromUser(this.auth.User.Id).subscribe(res => {
                this.userMeals = res.filter(meal => meal.Meal.Date >= DateTime.local());
            });
        }
    }

    public GetWeekDayName(id: number) {
        switch (id) {
            case 0: return "Dimanche";
            case 1: return "Lundi";
            case 2: return "Mardi";
            case 3: return "Mercredi";
            case 4: return "Jeudi";
            case 5: return "Vendredi";
            case 6: return "Samedi";
        }
    }


    public GetSubscriptionExpirationDate(subDate: string, subType: SubscriptionType) {
        const now = DateTime.local().set({ second: 0, millisecond: 0});
        const subDateParsed = DateTime.fromISO(subDate);
        const expirationDate = subDateParsed.plus({ months: subType.FixedContractDurationMonth });
        return expirationDate.diff(now).as('days');
    }

    async ChangeSubscription() {
        const modal = await this.modal.create({
            component: SubscriptionPickerComponent,
            componentProps: { CurrentPlace: this.userSub.Place},
            backdropDismiss: false
        });
        modal.onDidDismiss().then(async (res: { data: { SubscriptionType: SubscriptionType, ContractType: "FixedContract" | "ContractFree" }})  => {
            if(res.data == null) return;
            if(this.userSub != null) {
                this.userSub.TypeId = res.data.SubscriptionType.Id;
                this.userSub.Type = res.data.SubscriptionType;
                this.userSub.FixedContract = res.data.ContractType === "FixedContract";
                this.sub.Update(this.userSub).subscribe(subId => {
                    if(subId !== -1) this.toast.PresentToast("Abonnement enrengistré avec succès");
                    else this.toast.PresentToast("Erreur lors de l'abonnement");
                });
            } else this.toast.PresentToast("Erreur: il n'y aucun abonnement à votre nom !");
        });
        await modal.present();
    }
    

    async CreateSubscription() {
        const modal = await this.modal.create({component: SubscriptionCreatorComponent, backdropDismiss: false});
        modal.onDidDismiss().then((res: {data: { Place: Place, SubscriptionType: SubscriptionType, ContractType: "FixedContract" | "ContractFree" }}) => {
            if(res.data == null) return;
            this.userSub = new Subscription(-1, res.data.SubscriptionType.Id, this.auth.User.Id, res.data.Place.Id,
                DateTime.local(), res.data.ContractType === "FixedContract", res.data.Place, this.auth.User, res.data.SubscriptionType);
            this.sub.Create(this.userSub).subscribe(subId => {
                if(subId !== -1) {
                    this.userSub.Id = subId;
                    this.toast.PresentToast("Abonnement enrengistré avec succès");
                } else this.toast.PresentToast("Erreur lors de l'abonnement");
            });
        });
        await modal.present();
    }

    async ChangePlace() {
        const modal = await this.modal.create({
            component: PlacePickerComponent,
            componentProps: { SelectedId: this.userSub.Place.Id },
            backdropDismiss: false
        });
        modal.onDidDismiss().then((res: { data: Place }) => {
           if(res.data == null) return;
           this.userSub.PlaceId = res.data.Id;
           this.userSub.Place = res.data;
           this.sub.Update(this.userSub).subscribe(subId => {
               if(subId !== -1) this.toast.PresentToast("Changement d'espace Co'Work enrengistré avec succès");
               else this.toast.PresentToast("Erreur lors du changement d'espace Co'Work");
           });
        });
        await modal.present();
    }

    OrderByDay(OpenedTimes: TimeSlot[]) {
        if(OpenedTimes == null || OpenedTimes.length === 0) return [];
        return TimeSlot.OrderByDay(OpenedTimes);
    }
}
