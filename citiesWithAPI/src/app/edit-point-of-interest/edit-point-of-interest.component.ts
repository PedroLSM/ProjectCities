import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { PointsValidators } from 'src/common/validators/PointsValidators';
import { WindowVisibleService } from 'src/services/window-visible.service';
import { CityService } from 'src/services/city.service';
import { AppError } from 'src/common/app-error';
import { BadRequest } from 'src/common/bad-request';

@Component({
  selector: 'app-edit-point-of-interest',
  templateUrl: './edit-point-of-interest.component.html',
  styleUrls: ['./edit-point-of-interest.component.css']
})
export class EditPointOfInterestComponent implements OnInit {
  @Input("cityId") cityId;
  @Input("pointOfInterest") poi: any;
  form: FormGroup;  

  constructor(private service: CityService, private show: WindowVisibleService) { }

  ngOnInit() {

    this.form = new FormGroup({
      name: new FormControl(this.poi.name, [Validators.required, Validators.maxLength(50)]),
      description: new FormControl(this.poi.description, [Validators.required, Validators.maxLength(100)]),
    }, PointsValidators.NameCannotEqualsDescription);

  }

  submit(){
    
    if(this.form.invalid) return;
    
    let pointOfInterest = {
      name: this.form.get("name").value,
      description: this.form.get("description").value
    }
    
    this.service.updatePointOfInterest(this.cityId, this.poi.id, pointOfInterest)
      .subscribe(
        () => {
          this.show.CloseEdit;
          pointOfInterest['id'] = this.poi.id;
          this.service.updatedSuccess(pointOfInterest);
        }, 
        (error: AppError) => 
          {
            if (error instanceof BadRequest) {            
              this.form.setErrors(error.originalError);
            } else throw error;
          }
      );

  }

}
