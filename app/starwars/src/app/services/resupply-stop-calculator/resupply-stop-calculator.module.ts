import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ResupplyStopCalculatorService } from './resupply-stop-calculator.service';

@NgModule({
  imports: [
    HttpClientModule,
    ResupplyStopCalculatorService
  ],
  exports: [
    HttpClientModule,
    ResupplyStopCalculatorService
  ]
})
export class ResupplyStopCalculatorServiceModule {

}
