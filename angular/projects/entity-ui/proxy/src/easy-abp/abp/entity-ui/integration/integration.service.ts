import type { EntityUiIntegrationDto } from './dtos/models';
import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class IntegrationService {
  apiName = 'EasyAbpAbpEntityUi';

  get = () =>
    this.restService.request<any, EntityUiIntegrationDto>({
      method: 'GET',
      url: '/api/abp/entity-ui/integration',
    },
    { apiName: this.apiName });

  getModule = (moduleName: string) =>
    this.restService.request<any, EntityUiIntegrationDto>({
      method: 'GET',
      url: `/api/abp/entity-ui/integration/module/${moduleName}`,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
