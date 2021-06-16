import type { CreateUpdateEntityDto, EntityDto, EntityKey } from './dtos/models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class EntityService {
  apiName = 'EasyAbpAbpEntityUi';

  create = (input: CreateUpdateEntityDto) =>
    this.restService.request<any, EntityDto>({
      method: 'POST',
      url: '/api/abp/entity-ui/entity',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: EntityKey) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/abp/entity-ui/entity/${id.moduleName}/${id.name}`,
    },
    { apiName: this.apiName });

  get = (id: EntityKey) =>
    this.restService.request<any, EntityDto>({
      method: 'GET',
      url: `/api/abp/entity-ui/entity/${id.moduleName}/${id.name}`,
    },
    { apiName: this.apiName });

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<EntityDto>>({
      method: 'GET',
      url: '/api/abp/entity-ui/entity',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  update = (id: EntityKey, input: CreateUpdateEntityDto) =>
    this.restService.request<any, EntityDto>({
      method: 'PUT',
      url: `/api/abp/entity-ui/entity/${id.moduleName}/${id.name}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
