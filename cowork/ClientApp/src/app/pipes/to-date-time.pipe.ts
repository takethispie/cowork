import { Pipe, PipeTransform } from '@angular/core';
import {DateTime} from 'luxon';

@Pipe({
  name: 'toDateTime'
})
export class ToDateTimePipe implements PipeTransform {

  transform(value: string, ...args: any[]): any {
    return DateTime.fromISO(value,{zone: 'utc'});
  }

}
