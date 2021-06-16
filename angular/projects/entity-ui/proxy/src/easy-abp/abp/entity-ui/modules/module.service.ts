import type { CreateUpdateModuleDto, ModuleDto, ModuleKey } from './dtos/models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ModuleService {
  apiName = 'EasyAbpAbpEntityUi';

  create = (input: CreateUpdateModuleDto) =>
    this.restService.request<any, ModuleDto>({
      method: 'POST',
      url: '/api/abp/entity-ui/module',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: ModuleKey) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/abp/entity-ui/module/${id.name}`,
    },
    { apiName: this.apiName });

  get = (id: ModuleKey) =>
    this.restService.request<any, ModuleDto>({
      method: 'GET',
      url: `/api/abp/entity-ui/module/${id.name}`,
    },
    { apiName: this.apiName });

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<ModuleDto>>({
      method: 'GET',
      url: '/api/abp/entity-ui/module',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  update = (id: ModuleKey, input: CreateUpdateModuleDto) =>
    this.restService.request<any, ModuleDto>({
      method: 'PUT',
      url: `/api/abp/entity-ui/module/${id.name}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
