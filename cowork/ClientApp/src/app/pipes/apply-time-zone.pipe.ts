import { Pipe, PipeTransform } from '@angular/core';
import {DateTime, Zone} from 'luxon';
import {tz} from '../Utils';

@Pipe({
  name: 'applyTimeZone'
})
export class ApplyTimeZonePipe implements PipeTransform {

  transform(value: DateTime, ...args: any[]): any {
    const res = value.setZone(tz).setLocale("fr-FR");
    return res.toFormat("dd/MM/yyyy HH:mm");
  }

}
