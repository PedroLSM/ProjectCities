import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { PointsValidators } from 'src/common/validators/PointsValidators';
import { CityService } from 'src/services/city.service';
import { WindowVisibleService } from 'src/services/window-visible.service';
import { AppError } from 'src/common/app-error';
import { BadRequest } from 'src/common/bad-request';

@Component({
  selector: 'app-create-point-of-interest',
  templateUrl: './create-point-of-interest.component.html',
  styleUrls: ['./create-point-of-interest.component.css']
})
export class CreatePointOfInterestComponent implements OnInit {

  @Input('cityId') cityId;
  createPOI: boolean;

  form = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    description: new FormControl('', [Validators.required, Validators.maxLength(100)]),
  }, PointsValidators.NameCannotEqualsDescription);

  constructor(private service: CityService, private showWindows: WindowVisibleService) { }

  ngOnInit() {
    this.showWindows.emitirShowWindow
      .subscribe(
        show => {
          this.createPOI = show;
        }
      )
  }

  submit() {

    if (this.form.invalid) return;

    let pointOfInterest = {
      name: this.form.get("name").value,
      description: this.form.get("description").value
    }

    this.service.createPointOfInterest(this.cityId, pointOfInterest)
      .subscribe(
        (point) => {
          this.showWindows.ShowWindow;

          this.form.get("name").setValue("");
          this.form.get("description").setValue("");

          this.service.createdSuccess(point);
        },
        (error: AppError) => {
          if (error instanceof BadRequest) {
            this.form.setErrors(error.originalError);
          } else throw error;
        }
      );

  }

}
