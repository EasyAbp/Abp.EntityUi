import type { EntityDto as AbpEntityDto } from '@abp/ng.core';

export interface CreateUpdateEntityDto {
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
  properties: CreateUpdatePropertyDto[];
}

export interface CreateUpdatePropertyDto {
  entityModuleName?: string;
  entityName?: string;
  name?: string;
  isEntityCollection: boolean;
  typeOrEntityName?: string;
  nullable: boolean;
  showIn: PropertyShowInDto;
}

export interface EntityDto extends AbpEntityDto {
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
  properties: PropertyDto[];
}

export interface EntityKey {
  moduleName?: string;
  name?: string;
}

export interface PropertyDto extends AbpEntityDto {
  entityModuleName?: string;
  entityName?: string;
  name?: string;
  isEntityCollection: boolean;
  typeOrEntityName?: string;
  nullable: boolean;
  showIn: PropertyShowInDto;
}

export interface PropertyShowInDto {
  list: boolean;
  detail: boolean;
  creation: boolean;
  edit: boolean;
}
