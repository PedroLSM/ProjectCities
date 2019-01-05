import { Component, OnInit } from '@angular/core';
import { CityService } from 'src/services/city.service';
import { WindowVisibleService } from 'src/services/window-visible.service';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css']
})
export class CityComponent implements OnInit {

  cities: any[];
  pointsOfInterest: any[];
  create;

  constructor(private service: CityService, private showWindows: WindowVisibleService) { }

  ngOnInit() {

    this.showWindows.emitirShowWindow
      .subscribe(
        show => {
          this.create = show;
        }
      )

    this.service.getAllCity()
      .subscribe(
        cities => {
          this.cities = cities
        }
      );

    this.service.emitirPointCriado
      .subscribe(
        pointCriado => {
          this.pointsOfInterest.push(pointCriado);
        }
      );

    this.service.emitirPointDeletado
      .subscribe(
        poiDeletado => {
          let index = this.pointsOfInterest.indexOf(poiDeletado);
          this.pointsOfInterest.splice(index, 1);
        }
      )

    this.service.emitirPointAtualizado
      .subscribe(
        poiAtualizado => {
          let point = this.pointsOfInterest.filter(poi => poi.id == poiAtualizado.id)[0];

          point.name = poiAtualizado.name;
          point.description = poiAtualizado.description;
        }
      )

  }

  getPointsOfInterest(cityId) {
    this.create = false;

    if (!cityId) {
      this.pointsOfInterest = null;
      return;
    }

    this.service.getAllPointsOfInterest(cityId)
      .subscribe(
        poi => this.pointsOfInterest = poi
      );

  }

  createPOI() {
    this.create ? this.showWindows.ShowWindow : this.showWindows.CloseWindow;
  }

}
