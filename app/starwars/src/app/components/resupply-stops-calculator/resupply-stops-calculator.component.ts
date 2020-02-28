import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { ResupplyStopCalculatorService } from 'src/app/services/resupply-stop-calculator/resupply-stop-calculator.service';
import { StarshipResupplyStopResult } from 'src/app/model/starship-resupply-stops-result.model';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-resupply-stops-calculator',
  templateUrl: './resupply-stops-calculator.component.html',
  styleUrls: ['./resupply-stops-calculator.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ResupplyStopsCalculatorComponent implements OnInit {
  starshipStopResult: StarshipResupplyStopResult;
  formGroup: FormGroup;
  calculationSubscription: Subscription;
  isCalculating: boolean;

  get distance() { return this.formGroup.get('distance'); }

  constructor(
    private service: ResupplyStopCalculatorService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit() {
    this.formGroup = new FormGroup({
      distance: new FormControl('', [Validators.required, Validators.min(1), Validators.max(2147483647)]),
    });

    this.isCalculating = false;
  }

  calculate(): void {
    if (this.formGroup.valid) {
      this.isCalculating = true;

      this.calculationSubscription
        = this.service
                .calculate(this.distance.value)
                  .subscribe(
                    result => {
                      this.starshipStopResult = result;
                      this.finalizeCalculateProcess();
                    },
                    err => {
                      alert(err.message);
                      this.finalizeCalculateProcess();
                    }
                  );
    }
  }

  finalizeCalculateProcess(): void {
    this.calculationSubscription.unsubscribe();
    this.isCalculating = false;
    this.cdr.markForCheck();
  }
}
