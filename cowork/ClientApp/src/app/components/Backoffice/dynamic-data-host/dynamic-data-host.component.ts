import {Component, ComponentFactoryResolver, Input, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {DataModelHostDirective} from '../data-model-host.directive';
import {DynamicRowDefinition} from '../../../models/DynamicRowDefinition';
import {IDynamic} from '../../../models/IDynamic';
import {IModel} from '../../../models/IModel';
import {Observable, Subscription} from 'rxjs';
import {ToastService} from '../../../services/toast.service';
import {UserListComponent} from '../user-list/user-list.component';

@Component({
  selector: 'dynamic-data-host',
  templateUrl: './dynamic-data-host.component.html',
  styleUrls: ['./dynamic-data-host.component.scss'],
})
export class DynamicDataHostComponent implements OnInit, OnDestroy {
  @Input() DataModelComponents: DynamicRowDefinition[];
  @Input() SelectedComponent: string;
  @Input() Refresher: Observable<any>;
  @ViewChild(DataModelHostDirective, {static: true}) dataModelHostDirective: DataModelHostDirective;
  private sub: Subscription;

  constructor(private componentFactoryResolver: ComponentFactoryResolver, public toastService: ToastService) {
  }

  ngOnInit() {
    this.loadComponent();
    this.sub = this.Refresher.subscribe(() => this.loadComponent());
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  loadComponent() {
    let dataModel = this.DataModelComponents.find(c => c.Name === this.SelectedComponent);
    if(dataModel == null) {
      this.toastService.PresentToast("Erreur lors du chargement de la table");
      dataModel = new DynamicRowDefinition(UserListComponent, {}, "User");
    }
    //TODO add fallback component if dataModel == null
    const componentFactory = this.componentFactoryResolver.resolveComponentFactory(dataModel.Component);

    const viewContainerRef = this.dataModelHostDirective.viewContainerRef;
    viewContainerRef.clear();

    const componentRef = viewContainerRef.createComponent(componentFactory);
    (componentRef.instance as IDynamic).Data = dataModel.Data;
  }

}
