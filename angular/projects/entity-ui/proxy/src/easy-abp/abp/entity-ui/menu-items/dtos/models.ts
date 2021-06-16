import type { EntityDto } from '@abp/ng.core';

export interface CreateUpdateMenuItemDto {
  parentName?: string;
  name?: string;
  moduleName?: string;
  entityName?: string;
  permission?: string;
  menuItems: CreateUpdateMenuItemDto[];
}

export interface MenuItemDto extends EntityDto {
  parentName?: string;
  name?: string;
  moduleName?: string;
  entityName?: string;
  permission?: string;
  menuItems: MenuItemDto[];
}

export interface MenuItemKey {
  name?: string;
}
