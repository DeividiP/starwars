import { StarshipResupplyStopResult } from './../../model/starship-resupply-stops-result.model';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ResupplyStopsCalculatorComponent } from './resupply-stops-calculator.component';
import { ChangeDetectorRef, DebugElement } from '@angular/core';
import { ResupplyStopCalculatorService } from 'src/app/services/resupply-stop-calculator/resupply-stop-calculator.service';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';

fdescribe('ResupplyStopsCalculatorComponent', () => {
  let component: ResupplyStopsCalculatorComponent;
  let fixture: ComponentFixture<ResupplyStopsCalculatorComponent>;

  let cdrSpy: any;
  let serviceSpy: any;
  const observableSpy = jasmine.createSpyObj('observable', ['subscribe', 'pipe' ]);
  const calculationSubscriptionSpy = jasmine.createSpyObj('calculationSubscription', ['unsubscribe']);
  let distanceInput: DebugElement;
  let calculateButton: DebugElement;
  let changeDetectorRef: ChangeDetectorRef;

  beforeEach(async(() => {
    cdrSpy = jasmine.createSpyObj('cdr', ['detectChanges']);
    serviceSpy = jasmine.createSpyObj('service', ['calculate']);

    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        MaterialModule,
        BrowserAnimationsModule
      ],
      declarations: [ ResupplyStopsCalculatorComponent ],
      providers: [
        { provide: ResupplyStopCalculatorService, useValue: serviceSpy }
      ]
    });
  }));


  beforeEach(async(() => {
    TestBed.compileComponents().then(() => {
      fixture = TestBed.createComponent(ResupplyStopsCalculatorComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
      distanceInput = fixture.debugElement.query(By.css('.distance-input'));
      calculateButton = fixture.debugElement.query(By.css('.calculate-button'));
      changeDetectorRef = fixture.debugElement.injector.get(ChangeDetectorRef);
    });
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('ngOnInit: should initialize variables', async done => {
    component.ngOnInit();
    expect(component.formGroup).toBeTruthy();
    expect(component.isCalculating).toBeFalsy();
    done();
  });

  it('distance-input is binded with distance FormControl', async done => {
    const validDistance = '234556';
    setDistanceInputValue(validDistance);

    fixture.whenStable().then(() => {
      expect(component.distance.value).toEqual(Number(validDistance));
      done();
    });
  });

  describe('When distance value is greater than int32 max value', () => {
    const intMaxOutOfRange = '2147483648';

    it('input should be INvalid', async done => {
      setDistanceInputValue(intMaxOutOfRange);
      fixture.whenStable().then(() => {
        expect(distanceInput.classes['ng-invalid']).not.toBeNull();
        done();
      });
    });

    it('formGroup should be INvalid', async done => {
      setDistanceInputValue(intMaxOutOfRange);
      fixture.whenStable().then(() => {
        expect(component.formGroup.valid).toBeFalsy();
        done();
      });
    });
  });

  describe('When distance value is negative', () => {
    const negativeValue = '-222';

    it('input should be INvalid', async done => {
      setDistanceInputValue(negativeValue);
      fixture.whenStable().then(() => {
        expect(distanceInput.classes['ng-invalid']).not.toBeNull();
        done();
      });
    });

    it('formGroup should be INvalid', async done => {
      setDistanceInputValue(negativeValue);
      fixture.whenStable().then(() => {
        expect(component.formGroup.valid).toBeFalsy();
        done();
      });
    });
  });

  describe('When distance value is equal zero', () => {
    const zero = '0';

    it('input should be INvalid', async done => {
      setDistanceInputValue(zero);
      fixture.whenStable().then(() => {
        expect(distanceInput.classes['ng-invalid']).not.toBeNull();
        done();
      });
    });

    it('formGroup should be INvalid', async done => {
      setDistanceInputValue(zero);
      fixture.whenStable().then(() => {
        expect(component.formGroup.valid).toBeFalsy();
        done();
      });
    });
  });

  describe('When distance value is empty', () => {
    const empty = '';

    it('input should be INvalid', async done => {
      setDistanceInputValue(empty);
      fixture.whenStable().then(() => {
        expect(distanceInput.classes['ng-invalid']).not.toBeNull();
        done();
      });
    });

    it('formGroup should be INvalid', async done => {
      setDistanceInputValue(empty);
      fixture.whenStable().then(() => {
        expect(component.formGroup.valid).toBeFalsy();
        done();
      });
    });
  });

  describe('When distance value is valid', () => {
    const validDistance = '234556';

    it('input should be valid', async done => {
      setDistanceInputValue(validDistance);
      fixture.whenStable().then(() => {
        expect(distanceInput.classes['ng-valid']).not.toBeNull();
        done();
      });
    });

    it('formGroup should be valid', async done => {
      setDistanceInputValue(validDistance);
      fixture.whenStable().then(() => {
        expect(component.distance.value).toEqual(Number(validDistance));
        expect(component.formGroup.valid).toBeTruthy();
        done();
      });
    });
  });

  describe('When distance is valid and click on Calculate button', () => {
    const validDistance = '234556';

    it('Component.Calculate should be call', async done => {
      spyOn(component, 'calculate');
      setDistanceInputValue(validDistance);
      fireCalculateButtonClick();

      fixture.whenStable().then(() => {
        expect(component.calculate).toHaveBeenCalled();
        done();
      });
    });

    it('Spinner should be presented while process is running', async done => {
      expect(getSpinner()).toBeFalsy('Before click on calculate button, the sppiner should be hidden');

      serviceSpy.calculate.and.returnValue(observableSpy);
      component.calculationSubscription = calculationSubscriptionSpy;

      observableSpy.subscribe.and.callFake((next, _) => {
        changeDetectorRef.detectChanges();
        expect(getSpinner()).toBeTruthy('Before services returns, the sppiner should be showing');
        next();
        changeDetectorRef.detectChanges();
        expect(getSpinner()).toBeFalsy('Before services returns, the sppiner should be hidden');
        done();
      });

      setDistanceInputValue(validDistance);
      fireCalculateButtonClick();
    });

    it('Calculate Button should stay inactive while process is running', async done => {
      const disabled = 'disabled';
      expect(getCalculateButton().attributes[disabled]).toBeFalsy('Before click on calculate button, the button should be enable');

      serviceSpy.calculate.and.returnValue(observableSpy);
      component.calculationSubscription = calculationSubscriptionSpy;

      observableSpy.subscribe.and.callFake((next, _) => {
        changeDetectorRef.detectChanges();
        expect(getCalculateButton().attributes[disabled]).toBeTruthy('Before services returns, button should be disabled');
        next();
        changeDetectorRef.detectChanges();
        expect(getCalculateButton().attributes[disabled]).toBeFalsy('After services returns, button should be enable');
        done();
      });

      setDistanceInputValue(validDistance);
      fireCalculateButtonClick();
    });
  });

  it('When formGroup is INvalid it shoud NOT call service.calculate', async done => {
    const invalidDistance = '-1';
    component.distance.setValue(invalidDistance);

    component.calculate();

    expect(serviceSpy.calculate).not.toHaveBeenCalled();
    done();
  });


  it('When formGroup is valid it shoud call service.calculate', async done => {
    const validDistance = '1000000';
    component.distance.setValue(validDistance);
    serviceSpy.calculate.and.returnValue(observableSpy);
    component.calculationSubscription = calculationSubscriptionSpy;

    observableSpy.subscribe.and.callFake((next, _) => {
      expect(next).not.toBeNull();
      expect(component.isCalculating).toBeTruthy();
      expect(serviceSpy.calculate).toHaveBeenCalledWith(validDistance);

      next();

      expect(component.isCalculating).toBeFalsy();
      expect(calculationSubscriptionSpy.unsubscribe).toHaveBeenCalled();

      done();
    });

    component.calculate();
  });

  it('When service returns items it shoud be listed', async done => {
    const validDistance = '1000000';
    component.distance.setValue(validDistance);
    serviceSpy.calculate.and.returnValue(observableSpy);
    component.calculationSubscription = calculationSubscriptionSpy;

    const stubResult = {
      distance: Number(validDistance),
      results:
      [
        {
          name: 'Executor',
          stops: 4
        },
        {
          name: 'Sentinel-class landing craft',
          stops: 198
        }
      ]
    } as StarshipResupplyStopResult;

    observableSpy.subscribe.and.callFake((next, _) => {
      next(stubResult);

      changeDetectorRef.detectChanges();
      fixture.whenStable().then(() => {
        expect(component.starshipStopResult).toEqual(stubResult);
        const listItems = fixture.debugElement.queryAll(By.css('.mat-list-item'));
        expect(listItems).toBeTruthy();
        expect(listItems.length).toEqual(stubResult.results.length);
        done();
      });
    });

    component.calculate();
  });

  it('service.calculate: when throws an error => should display an alert message', async done => {
    const validDistance = '1000000';
    const expectedErrorMesage = 'You got me!';
    component.distance.setValue(validDistance);
    serviceSpy.calculate.and.returnValue(observableSpy);
    component.calculationSubscription = calculationSubscriptionSpy;
    spyOn(window, 'alert');

    observableSpy.subscribe.and.callFake((_, error) => {
      expect(error).not.toBeNull();
      expect(component.isCalculating).toBeTruthy();
      expect(serviceSpy.calculate).toHaveBeenCalledWith(validDistance);

      error(new Error(expectedErrorMesage));

      expect(window.alert).toHaveBeenCalledWith(expectedErrorMesage);
      expect(component.isCalculating).toBeFalsy();
      expect(calculationSubscriptionSpy.unsubscribe).toHaveBeenCalled();

      done();
    });

    component.calculate();
  });

  function setDistanceInputValue(value: string): void {
    const distanceInputNe = distanceInput.nativeElement;
    distanceInputNe.value = value;
    distanceInputNe.dispatchEvent(new Event('input'));
  }

  function fireCalculateButtonClick(): void {
    calculateButton.triggerEventHandler('click', null);
    fixture.detectChanges();
  }

  function getSpinner(): DebugElement{
    return  fixture.debugElement.query(By.css('.spinner'));
  }

  function getCalculateButton(): DebugElement{
    return  fixture.debugElement.query(By.css('.calculate-button'));
  }
});
