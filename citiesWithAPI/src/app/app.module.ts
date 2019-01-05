import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


import { AppComponent } from './app.component';
import { HttpModule } from '@angular/http';
import { CityService } from 'src/services/city.service';
import { AppErrorHandler } from 'src/common/app-error-handler';
import { CityComponent } from './city/city.component';
import { PointOfInterestComponent } from './point-of-interest/point-of-interest.component';
import { CreatePointOfInterestComponent } from './create-point-of-interest/create-point-of-interest.component';
import { EditPointOfInterestComponent } from './edit-point-of-interest/edit-point-of-interest.component';

@NgModule({
  declarations: [
    AppComponent,
    CityComponent,
    PointOfInterestComponent,
    CreatePointOfInterestComponent,
    EditPointOfInterestComponent,
  ],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [CityService, 
    {provide: ErrorHandler, useClass: AppErrorHandler}],
  bootstrap: [AppComponent]
})
export class AppModule { }
