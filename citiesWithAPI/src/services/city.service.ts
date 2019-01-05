import { Injectable, EventEmitter } from '@angular/core';
import { Http } from '@angular/http';

import { map } from 'rxjs/operators';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { BadRequest } from 'src/common/bad-request';
import { NotFoundError } from 'src/common/not-found-error';
import { AppError } from 'src/common/app-error';

@Injectable({
  providedIn: 'root'
})
export class CityService {

  url = "http://localhost:5000/api/cities";

  emitirPointCriado = new EventEmitter<any>();
  emitirPointDeletado = new EventEmitter<any>();
  emitirPointAtualizado = new EventEmitter<any>();

  constructor(private http: Http) {
  }

  getAllCity() {
    return this.http.get(this.url)
      .pipe(
        map(response => response.json()),
        catchError(this.handleError)
      );
  }

  getAllPointsOfInterest(cityId) {
    return this.http.get(this.url + "/" + cityId + "/pointsofinterest").
      pipe(
        map(response => response.json()),
        catchError(this.handleError)
      );
  }

  createPointOfInterest(cityId, pointsOfInterest) {

    return this.http.post(this.url + "/" + cityId + "/pointsofinterest", pointsOfInterest)
      .pipe(
        map(response => response.json()),
        catchError(this.handleError)
      );
  }

  updatePointOfInterest(cityId, id, pointsOfInterest){
    return this.http.put(this.url + "/" + cityId + "/pointsofinterest/" + id, pointsOfInterest)
      .pipe(
        map(response => response.json()),
        catchError(this.handleError)
      );
  }

  deletePointOfInterest(cityId, id) {
    return this.http.delete(this.url + "/" + cityId + "/pointsofinterest/" + id)
      .pipe(
        map(response => response.json()),
        catchError(this.handleError)
      );
  }


  // EMISSÃO DE EVENTOS

  deletedSuccess(point){
    this.emitirPointDeletado.emit(point);
  }

  createdSuccess(point){
    this.emitirPointCriado.emit(point);
  }

  updatedSuccess(point){
    this.emitirPointAtualizado.emit(point);
  }

  private handleError(error: Response) {

    if (error.status === 400) return throwError(new BadRequest(error.json()));
    if (error.status === 404) return throwError(new NotFoundError());

    return throwError(new AppError(error));
  }
  
}
