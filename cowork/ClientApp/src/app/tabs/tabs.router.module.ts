import { NgModule } from '@angular/core';
import {CanActivate, RouterModule, Routes} from '@angular/router';
import { TabsPage } from './tabs.page';
import {UserLoggedInGuard} from '../user-logged-in.guard';

const routes: Routes = [
  {
    path: 'tabs',
    component: TabsPage,
    children: [
      {
        path: 'account',
        loadChildren: "../account/account.module#AccountModule"
      },
      {
        path: 'meal',
        loadChildren: "../meal/meal.module#MealModule",
      },
      {
        path: 'reservation',
        loadChildren: "../reservation/reservation.module#ReservationModule",
      },
      {
        path: 'ticket',
        loadChildren: "../ticket/ticket.module#TicketModule",
      },
      {
        path: 'ware',
        loadChildren: "../ware/ware.module#WareModule",
      },
      {
        path: 'backoffice',
        loadChildren: "../backoffice/backoffice.module#BackofficeModule",
      },
      {
        path: '',
        redirectTo: '/tabs/account',
        pathMatch: 'full'
      }
    ],
  },
  {
    path: '',
    redirectTo: '/tabs/account',
    pathMatch: 'full'
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TabsPageRoutingModule {}
