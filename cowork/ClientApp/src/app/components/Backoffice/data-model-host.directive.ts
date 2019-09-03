import {Directive, ViewContainerRef} from '@angular/core';

@Directive({
  selector: '[appRowDataModel]'
})
export class DataModelHostDirective {

  constructor(public viewContainerRef: ViewContainerRef) { }

}
