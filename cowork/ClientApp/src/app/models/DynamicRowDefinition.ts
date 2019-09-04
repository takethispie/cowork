import {Type} from '@angular/core';
import {Table} from "./Table";

export class DynamicRowDefinition {
    constructor(public Component: Type<any>, public Data: any, public Name: Table) {}
}