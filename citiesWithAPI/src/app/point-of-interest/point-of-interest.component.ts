import { Component, OnInit, Input } from '@angular/core';
import { CityService } from 'src/services/city.service';
import { AppError } from 'src/common/app-error';
import { NotFoundError } from 'src/common/not-found-error';
import { WindowVisibleService } from 'src/services/window-visible.service';

@Component({
  selector: 'app-point-of-interest',
  templateUrl: './point-of-interest.component.html',
  styleUrls: ['./point-of-interest.component.css']
})
export class PointOfInterestComponent implements OnInit {

  @Input("pointOfInterest") poi: any;
  @Input('cityId') cityId;

  editar: boolean;

  constructor(private service: CityService, private showEdit: WindowVisibleService) { }

  ngOnInit() {
    this.showEdit.emitirShowWindowEdit
      .subscribe(
        () => this.editar = false
      );
  }

  show(poi) {
    poi.showDescription = !poi.showDescription
  }

  edit() {
    this.editar = !this.editar;
  }

  delete(poi) {

    this.service.deletePointOfInterest(this.cityId, poi.id)
      .subscribe(() => {
        this.service.deletedSuccess(poi);
      },
        (error: AppError) => {
          if (error instanceof NotFoundError) {
            alert("This post has already been deleted.");
          } else throw error;
        }
      );

  }



}
