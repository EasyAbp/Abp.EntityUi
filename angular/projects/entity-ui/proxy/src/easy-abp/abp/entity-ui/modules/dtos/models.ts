import type { EntityDto } from '@abp/ng.core';

export interface CreateUpdateModuleDto {
  name?: string;
  lResourceTypeName?: string;
  lResourceTypeAssemblyName?: string;
}

export interface ModuleDto extends EntityDto {
  name: string;
  lResourceTypeName?: string;
  lResourceTypeAssemblyName?: string;
}

export interface ModuleKey {
  name?: string;
}
