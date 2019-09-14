import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import {UserLoggedInGuard} from './user-logged-in.guard';
import {SignupComponent} from './pages/signup/signup.component';

const routes: Routes = [
  { path: '', loadChildren: './tabs/tabs.module#TabsPageModule',  canActivate: [UserLoggedInGuard]},
  { path: 'Auth', loadChildren: './pages//login/login.module#LoginPageModule' },
  { path: 'Register', component: SignupComponent}
];
@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
