import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { ResupplyStopsCalculatorComponent } from './components/resupply-stops-calculator/resupply-stops-calculator.component';

const appRoutes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent, data: { title: 'Project Description' } },
    { path: 'resupply-stops-calculator', component: ResupplyStopsCalculatorComponent, data: { title: 'Resupply Stops Calculator' } },
  ];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule {}