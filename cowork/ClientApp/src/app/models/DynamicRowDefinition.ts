import {Type} from '@angular/core';

export class DynamicRowDefinition {
    constructor(public Component: Type<any>, public Data: any, public Name: string) {}
}