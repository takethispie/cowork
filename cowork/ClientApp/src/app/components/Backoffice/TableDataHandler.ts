import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ModalController} from '@ionic/angular';
import {User} from '../../models/User';
import {Field} from '../dynamic-form-builder/Field';
import List from 'linqts/dist/src/list';
import {DynamicFormModalComponent} from './dynamic-form-modal/dynamic-form-modal.component';

export class TableDataHandler<T> {
    Data: T[];
    Page: number;
    Amount: number;
    private EmptyFields: Field[];

    constructor(public apiService: { http: HttpClient, Create: (model: T) => Observable<number>,
                Update: (model: T) => Observable<number>, Delete: (id: number) => Observable<object>,
                AllWithPaging: (page: number, amount: number) => Observable<T[]>},
                public ModalCtrl: ModalController, public Fields: Field[], public CreateModelFromFields: (field: Field[]) => T) {
        const tempFields = new List(this.Fields);
        this.EmptyFields = tempFields.ToArray();
        this.Page = 0;
        this.Amount = 30;
    }


    async UpdateItem(model: T, fields: Field[]) {
        const fieldList = new List(fields).Select(field => {
            field.Value = model[field.Name];
            return field;
        });
        await this.OpenModal("Update", { Fields: fieldList.ToArray()});
    }


    async AddItem() {
        this.Fields.forEach(field => field.SetValueToDefault());
        await this.OpenModal("Create", { Fields: this.Fields });
    }


    async OpenModal(mode: "Update" | "Create" ,componentProps: any) {
        const modal = await this.ModalCtrl.create({
            component: DynamicFormModalComponent,
            componentProps
        });
        modal.onDidDismiss().then(res => {
            if(res.data == null) return;
            const user = this.CreateModelFromFields(this.Fields);
            const observer = {
                next: value => this.Refresh(),
                error: err => console.log(err)
            };
            mode === "Update" ? this.apiService.Update(user).subscribe(observer) : this.apiService.Create(user).subscribe(observer);
        });
        await modal.present();
    }


    async Delete(id: number) {
        this.apiService.Delete(id).subscribe({
            next: value => this.Refresh(),
            error: err => {}
        });
    }


    Refresh() {
        this.Page = 0;
        this.Data = [];
        this.LoadData(null);
    }


    LoadData(event: any) {
        this.apiService.AllWithPaging(this.Page, this.Amount).subscribe({
            next: value => {
                if(value.length === 0) return;
                this.Data = this.Data.concat(value);
                this.Page++;
            },
            error: err => {
                console.log(err);
                if(event != null)  event.target.complete();
            },
            complete: () => { if(event != null)  event.target.complete(); }
        });
    }
}