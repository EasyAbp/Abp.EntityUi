import type { CreateUpdateMenuItemDto, MenuItemDto, MenuItemKey } from './dtos/models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class MenuItemService {
  apiName = 'EasyAbpAbpEntityUi';

  create = (input: CreateUpdateMenuItemDto) =>
    this.restService.request<any, MenuItemDto>({
      method: 'POST',
      url: '/api/abp/entity-ui/menu-item',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: MenuItemKey) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/abp/entity-ui/menu-item/${id.name}`,
    },
    { apiName: this.apiName });

  get = (id: MenuItemKey) =>
    this.restService.request<any, MenuItemDto>({
      method: 'GET',
      url: `/api/abp/entity-ui/menu-item/${id.name}`,
    },
    { apiName: this.apiName });

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<MenuItemDto>>({
      method: 'GET',
      url: '/api/abp/entity-ui/menu-item',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  update = (id: MenuItemKey, input: CreateUpdateMenuItemDto) =>
    this.restService.request<any, MenuItemDto>({
      method: 'PUT',
      url: `/api/abp/entity-ui/menu-item/${id.name}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
