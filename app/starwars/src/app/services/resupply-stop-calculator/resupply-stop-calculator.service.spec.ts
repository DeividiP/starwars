import { TestBed } from '@angular/core/testing';
import { ResupplyStopCalculatorService } from './resupply-stop-calculator.service';
import { HttpClientModule } from '@angular/common/http';

describe('ResupplyStopCalculatorService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientModule,
    ]
  }));

  it('should be created', () => {
    const service: ResupplyStopCalculatorService = TestBed.get(ResupplyStopCalculatorService);
    expect(service).toBeTruthy();
  });
});
