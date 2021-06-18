import { EasyAbp } from '@easy-abp.Abp/entity-ui/proxy';
import { EntityAction, EntityProp, FormProp, ToolbarAction } from '@abp/ng.theme.shared/extensions';
import { GetPropType, notify } from '../utils/defaults.util';
export function camelize(str) {
  return str
    .replace(/_/g, ' ')
    .replace(/\s(.)/g, function ($1) {
      return $1.toUpperCase();
    })
    .replace(/\s/g, '')
    .replace(/^(.)/, function ($1) {
      return $1.toLowerCase();
    });
}



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

  static transFrom(dto: EasyAbp.Abp.EntityUi.Entities.Dtos.EntityDto): EntityInfo {
    let result = new EntityInfo();
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
          type: GetPropType(p.typeOrEntityName),
          name: camelize(p.name),
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
          id: camelize(p.name),
          name: camelize(p.name),
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
          id: camelize(p.name),
          name: camelize(p.name),
          displayName: p.name,
        });
      });
  }
}
