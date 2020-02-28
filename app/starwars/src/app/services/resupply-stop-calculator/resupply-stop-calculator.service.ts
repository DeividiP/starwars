import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { StarshipResupplyStopResult } from 'src/app/model/starship-resupply-stops-result.model';

@Injectable({
  providedIn: 'root'
})
export class ResupplyStopCalculatorService {

  constructor(private httpClient: HttpClient) {}

  calculate(distance: number): Observable<StarshipResupplyStopResult> {
    return this.httpClient
                  .get<StarshipResupplyStopResult>(`https://localhost:44394/api/resupply-stops?distance=${distance}`)
                  .pipe(catchError(error => throwError(error)));
  }
}
