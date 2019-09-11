import { Pipe, PipeTransform } from '@angular/core';
import {DateTime} from 'luxon';

@Pipe({
  name: 'ToFormatedDateTimePipe'
})
export class ToFormatedDateTimePipePipe implements PipeTransform {

  transform(value: string, ...args: any[]): any {
    return DateTime.fromISO(value).setLocale('fr').toLocaleString(DateTime.DATE_FULL);
  }

}
