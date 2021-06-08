import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'EntityUi',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44374',
    redirectUri: baseUrl,
    clientId: 'EntityUi_App',
    responseType: 'code',
    scope: 'offline_access EntityUi role email openid profile',
  },
  apis: {
    default: {
      url: 'https://localhost:44374',
      rootNamespace: 'EasyAbp.Abp.EntityUi',
    },
    EntityUi: {
      url: 'https://localhost:44369',
      rootNamespace: 'EasyAbp.Abp.EntityUi',
    },
  },
} as Environment;
