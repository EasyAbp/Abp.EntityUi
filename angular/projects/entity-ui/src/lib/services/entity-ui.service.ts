import { Injectable, Injector } from '@angular/core';
import { RestService } from '@abp/ng.core';
import { EasyAbp } from '@easy-abp.Abp/entity-ui/proxy';
import { tap } from 'rxjs/operators';
import {
  EntityAction,
  EntityActionList,
  EntityProp,
  EntityPropOptions,
  ePropType,
  FormProp,
  ToolbarAction,
} from '@abp/ng.theme.shared/extensions';
import { GetPropType, notify } from '../utils/defaults.util';

export class EntityInfo implements EasyAbp.Abp.EntityUi.Entities.Dtos.EntityDto {
  moduleName?: string;
  name?: string;
  namespace?: string;
  belongsTo?: string;
  keys: string[];
  creationEnabled: boolean;
  creationPermission?: string;
  editEnabled: boolean;
  editPermission?: string;
  deletionEnabled: boolean;
  deletionPermission?: string;
  detailEnabled: boolean;
  detailPermission?: string;
  properties: EasyAbp.Abp.EntityUi.Entities.Dtos.PropertyDto[];
  id?: string;
  constructor(dto: EasyAbp.Abp.EntityUi.Entities.Dtos.EntityDto) {
    this.id = dto.id;
  }
  static transFrom(dto: EasyAbp.Abp.EntityUi.Entities.Dtos.EntityDto): EntityInfo {
    let result = new EntityInfo(dto);
    Object.keys(dto).forEach(p => {
      result[p] = dto[p];
    });
    return result;
  }
  get EntityProps(): EntityProp[] {
    return this.properties
      .filter(p => p.showIn.list)
      .map(p => {
        return EntityProp.create({
          type: p.typeOrEntityName.toLowerCase() as any,
          name: p.name.toLowerCase(),
          displayName: p.name,
          sortable: false,
          columnWidth: 90,
        });
      });
  }
  get ToolbarActions(): ToolbarAction[] {
    let result: ToolbarAction[] = [];
    if (this.creationEnabled) {
      result.push(
        ToolbarAction.create({
          text: 'Create',
          action: notify('Create'),
          permission: this.creationPermission,
          icon: 'fa fa-plus',
        })
      );
    }
    return result;
  }
  get EntityActions(): EntityAction[] {
    let result: EntityAction[] = [];
    if (this.editEnabled) {
      result.push(
        EntityAction.create({
          text: 'Edit',
          action: notify('Edit'),
          permission: this.editPermission,
          icon: 'fa fa-plus',
        })
      );
    }
    if (this.deletionEnabled) {
      result.push(
        EntityAction.create({
          text: 'Delete',
          action: notify('Delete'),
          permission: this.deletionPermission,
          icon: 'fa fa-plus',
        })
      );
    }
    return result;
  }

  get CreateFormProps(): FormProp[] {
    return this.properties
      .filter(p => p.showIn.creation)
      .map(p => {
        return FormProp.create({
          type: GetPropType(p.typeOrEntityName),
          id: p.name.toLowerCase(),
          name: p.name.toLowerCase(),
          displayName: p.name,
        });
      });
  }
  get EditFormProps(): FormProp[] {
    return this.properties
      .filter(p => p.showIn.edit)
      .map(p => {
        return FormProp.create({
          type: GetPropType(p.typeOrEntityName),
          id: p.name.toLowerCase(),
          name: p.name.toLowerCase(),
          displayName: p.name,
        });
      });
  }  
}
@Injectable({
  providedIn: 'root',
})
export class EntityUiService {
  apiName = 'EntityUi';
  apiService: EasyAbp.Abp.EntityUi.Integration.IntegrationService;
  definitions: EasyAbp.Abp.EntityUi.Integration.Dtos.EntityUiIntegrationDto;
  constructor(private restService: RestService, injector: Injector) {
    this.apiService = injector.get(EasyAbp.Abp.EntityUi.Integration.IntegrationService);
  }

  sample() {
    return this.apiService.get().pipe(
      tap(j => {
        this.definitions = j;
        console.log(j);
      })
    );
    // return this.restService.request<void, any>(
    //   { method: 'GET', url: '/api/EntityUi/sample' },
    //   { apiName: this.apiName }
    // );
  }

  getEntity(moduleName: string, entityNmae: string): EntityInfo {
    let dto = this.definitions.entities.find(
      x => x.moduleName === moduleName && x.name === entityNmae
    );
    return EntityInfo.transFrom(dto);
  }
}
