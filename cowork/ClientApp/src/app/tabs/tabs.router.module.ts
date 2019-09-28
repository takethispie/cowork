import { NgModule } from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import { TabsPage } from './tabs.page';
import {UserLoggedInGuard} from '../user-logged-in.guard';

const routes: Routes = [
  {
    path: 'tabs',
    component: TabsPage,
    children: [
      {
        path: 'account',
        loadChildren: "../pages/account/account.module#AccountModule",
        canActivate: [UserLoggedInGuard]
      },
      {
        path: 'meal',
        loadChildren: "../pages/meal/meal.module#MealModule",
        canActivate: [UserLoggedInGuard]
      },
      {
        path: 'reservation',
        loadChildren: "../pages/reservation/reservation.module#ReservationModule",
        canActivate: [UserLoggedInGuard]
      },
      {
        path: 'ticket',
        loadChildren: "../pages/ticket/ticket.module#TicketModule",
        canActivate: [UserLoggedInGuard]
      },
      {
        path: 'ware',
        loadChildren: "../pages/ware/ware.module#WareModule",
        canActivate: [UserLoggedInGuard]
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
