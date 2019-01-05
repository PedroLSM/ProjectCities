import { Injectable, EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class WindowVisibleService {
  emitirShowWindow = new EventEmitter<boolean>();
  emitirShowWindowEdit = new EventEmitter<boolean>();

  constructor() { }

  get ShowWindow(){
    this.emitirShowWindow.emit(false);
    return true;
  }

  get CloseWindow(){
    this.emitirShowWindow.emit(true);
    return false;
  }

  get CloseEdit(){
    return this.emitirShowWindowEdit.emit(false);
  }
}
