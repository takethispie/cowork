import {Component, Input, OnInit} from '@angular/core';
import {Subscription} from '../../../models/Subscription';
import {MealBooking} from '../../../models/MealBooking';
import {RoomBooking} from '../../../models/RoomBooking';
import {AuthService} from '../../../services/auth.service';
import {SubscriptionService} from '../../../services/subscription.service';
import {ModalController} from '@ionic/angular';
import {ToastService} from '../../../services/toast.service';
import {MealBookingService} from '../../../services/meal-booking.service';
import {RoomBookingService} from '../../../services/room-booking.service';
import {TimeSlotService} from '../../../services/time-slot.service';
import {LoadingService} from '../../../services/loading.service';
import {DateTime} from 'luxon';
import {SubscriptionType} from '../../../models/SubscriptionType';
import {SubscriptionPickerComponent} from '../../Subscription/subscription-picker/subscription-picker.component';
import {SubscriptionCreatorComponent} from '../../Subscription/subscription-creator/subscription-creator.component';
import {PlacePickerComponent} from '../../Place/place-picker/place-picker.component';
import {Place} from '../../../models/Place';
import {TimeSlot} from '../../../models/TimeSlot';
import {Observable} from 'rxjs';

@Component({
    selector: 'account-user',
    templateUrl: './account-user.component.html',
    styleUrls: ['./account-user.component.scss'],
})
export class AccountUserComponent implements OnInit{

    @Input() Refresher: Observable<object> = new Observable<object>();
    public userSub: Subscription;
    public userMeals: MealBooking[];
    public roomBookings: RoomBooking[];
    public DaysBeforeExpiration: number;

    constructor(public auth: AuthService, public sub: SubscriptionService, public modal: ModalController, public toast: ToastService,
                public mealReservationService: MealBookingService, public roomBookingService: RoomBookingService,
                public timeSlotService: TimeSlotService, public loading: LoadingService) {
        this.userSub = this.auth.Subscription;
    }


    ngOnInit(): void {
        this.Refresher.subscribe({
            next: () => this.load()
        });
    }


    load() {
        this.DaysBeforeExpiration = this.GetSubscriptionExpirationDate(this.userSub.LatestRenewal as unknown as string, this.userSub.Type);
        if(this.DaysBeforeExpiration < 30) {
            this.toast.PresentToast("Attention votre abonnement sera expiré à la fin du mois !")
        }
        this.loading.Loading = true;
        this.roomBookingService.AllOfUser(this.auth.UserId).subscribe({
            next: res => {
                this.roomBookings = res.filter(booking => booking.Start >= DateTime.local());
            },
            error: err => {
                this.toast.PresentToast("Une erreur est survenue lors de la récupération des réservations de salles");
                this.loading.Loading = false;
            },
            complete: () => this.loading.Loading = false
        });
        this.loading.Loading = true;
        this.mealReservationService.AllFromUser(this.auth.UserId).subscribe({
            next: res => {
                this.userMeals = res.filter(meal => meal.Meal.Date >= DateTime.local());
            },
            error: err => {
                this.toast.PresentToast("Une erreur est survenue lors de la récupération des réservations de repas");
                this.loading.Loading = false;
            },
            complete: () => this.loading.Loading = false
        });
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
        const now = DateTime.local().set({hour: 0, minute: 0, second: 0, millisecond: 0});
        const subDateParsed = DateTime.fromISO(subDate);
        const expirationDate = subDateParsed.plus({ month: subType.FixedContractDurationMonth });
        return expirationDate.diff(now.set({hour: 0, minute: 0, second: 0, millisecond: 0})).as('day');
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
                this.userSub.LatestRenewal = DateTime.local();
                this.loading.Loading = true;
                this.sub.Update(this.userSub).subscribe({
                    next: subId => {
                        if(subId !== -1) this.toast.PresentToast("Abonnement enrengistré avec succès");
                        else this.toast.PresentToast("Erreur lors de l'abonnement");
                        this.loading.Loading = false;
                    },
                    error: err => {
                        this.toast.PresentToast("Erreur lors de la communication avec le serveur");
                        this.loading.Loading = false;
                    }
                });
            } else this.toast.PresentToast("Erreur: il n'y aucun abonnement à votre nom !");
        });
        await modal.present();
    }


    async CreateSubscription() {
        const modal = await this.modal.create({component: SubscriptionCreatorComponent, backdropDismiss: false});
        modal.onDidDismiss().then((res: {data: { Place: Place, SubscriptionType: SubscriptionType, ContractType: "FixedContract" | "ContractFree" }}) => {
            if(res.data == null) return;
            this.userSub = new Subscription(-1, res.data.SubscriptionType.Id, this.auth.UserId, res.data.Place.Id,
                DateTime.local(), res.data.ContractType === "FixedContract", res.data.Place, this.auth.User, res.data.SubscriptionType);
            this.sub.Create(this.userSub).subscribe(subId => {
                if(subId !== -1) {
                    this.userSub.Id = subId;
                    this.auth.Subscription = this.userSub;
                    this.auth.PlaceId = res.data.Place.Id;
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
                if(subId !== -1) {
                    this.toast.PresentToast("Changement d'espace Co'Work enrengistré avec succès");
                    this.auth.PlaceId = res.data.Id;
                    this.auth.Subscription.PlaceId = res.data.Id;
                    this.auth.Subscription.Place = res.data;
                }
                else this.toast.PresentToast("Erreur lors du changement d'espace Co'Work");
            });
        });
        await modal.present();
    }

    OrderByDay(OpenedTimes: TimeSlot[]) {
        if(OpenedTimes == null || OpenedTimes.length === 0) return [];
        return TimeSlot.OrderByDay(OpenedTimes);
    }

    async OnRenewClick() {
        await this.ChangeSubscription();
    }
}
