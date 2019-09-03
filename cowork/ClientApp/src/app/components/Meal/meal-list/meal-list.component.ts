import {Component, Input, OnInit} from '@angular/core';
import {Meal} from '../../../models/Meal';
import {AuthService} from '../../../services/auth.service';
import {PlaceService} from '../../../services/place.service';
import {MealBookingService} from '../../../services/meal-booking.service';
import {MealBooking} from '../../../models/MealBooking';
import {ToastService} from '../../../services/toast.service';

@Component({
    selector: 'meal-list',
    templateUrl: './meal-list.component.html',
    styleUrls: ['./meal-list.component.scss'],
})
export class MealListComponent implements OnInit {
    @Input() Meals: Meal[];
    ReservedMealIds: number[];
    MealReservations: MealBooking[];

    constructor(public auth: AuthService, public placeService: PlaceService, public mealRes: MealBookingService, public toast: ToastService) { }

    ngOnInit() {
        this.ReservedMealIds = [];
        this.MealReservations = [];
    }

    ionViewWillEnter() {
        this.mealRes.AllFromUser(this.auth.User.Id).subscribe(res => {
            this.ReservedMealIds = res.map(mealRes => mealRes.MealId);
        });
    }

    BookMeal(item: Meal) {
        const reservation = new MealBooking(-1, item.Id, this.auth.User.Id, "");
        this.mealRes.Create(reservation).subscribe(res => {
            if(res === -1) this.toast.PresentToast("Erreur lors de la réservation !");
            reservation.Id = res;
            this.MealReservations.push(reservation);
            this.ReservedMealIds.push(item.Id);
        });
    }

    IsAlreadyBooked(item: Meal): boolean {
        return this.ReservedMealIds.some(id => item.Id === id);
    }

    Unbookmeal(item: Meal) {
        const reservation = this.MealReservations.find(mealRes => mealRes.MealId === item.Id);
        if(reservation == null) this.toast.PresentToast("Impossible de supprimmer la réservation");
        this.mealRes.Delete(reservation.Id).subscribe({
            next: () => {
                this.MealReservations = this.MealReservations.filter(mealRes => mealRes.Id !== reservation.Id);
                this.ReservedMealIds = this.ReservedMealIds.filter(meal => meal !== item.Id);
            },
            error: () => this.toast.PresentToast("Erreur lors de l'annulation de la réservation !")
        });
    }
}
