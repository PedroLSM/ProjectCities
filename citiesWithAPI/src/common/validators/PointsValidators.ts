import { AbstractControl, ValidationErrors } from "@angular/forms";

export class PointsValidators{
    static NameCannotEqualsDescription(control: AbstractControl) : ValidationErrors | null{
        var name = control.get("name").value;
        var description = control.get("description").value;
        
        if(name === description){
            return { NameCannotEqualsDescription: true }
        }
        
        return null;
    }
}