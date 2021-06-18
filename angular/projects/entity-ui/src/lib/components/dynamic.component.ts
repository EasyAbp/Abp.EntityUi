import { Component, Injector, Input, OnInit } from '@angular/core';
import { EntityService, EntityUiProfile } from '@easy-abp.Abp/entity-ui';
import { EasyAbp, MvcSample } from '@easy-abp.Abp/entity-ui/proxy';

@Component({
  selector: 'lib-book',
  template: `
    <select class="form-control" (change)="onChange($event.target.value)">
      <ng-container *ngFor="let item of profiles; let i = index">
        <option>{{ item.entityKey }}</option>
      </ng-container>
    </select>
    <lib-entity-ui [entityUiProfile]="entityUiProfile"></lib-entity-ui>
  `,
  styles: [],
})
export class DynamicComponent implements OnInit {
  entityUiProfile: EntityUiProfile = null;
  profiles: EntityUiProfile[] = [];
  constructor(private injector: Injector) {
    this.profiles = [
      {
        entityKey: 'EasyAbp.Abp.EntityUi::Entity',
        entityService: this.injector.get(EasyAbp.Abp.EntityUi.Entities.EntityService),
      },
      {
        entityKey: 'MvcSample::Book',
        entityService: this.injector.get(MvcSample.Books.BookService),
      },
      {
        entityKey: 'EasyAbp.Abp.EntityUi::MenuItem',
        entityService: this.injector.get(EasyAbp.Abp.EntityUi.MenuItems.MenuItemService),
      },
      {
        entityKey: 'EasyAbp.Abp.EntityUi::Module',
        entityService: this.injector.get(EasyAbp.Abp.EntityUi.Modules.ModuleService),
      },
    ];
    this.entityUiProfile = this.profiles[0];
  }

  ngOnInit(): void {}

  onChange(entityKey) {
    var current=this.profiles.find(x=>x.entityKey===entityKey);
    this.entityUiProfile=current;
  }
}
