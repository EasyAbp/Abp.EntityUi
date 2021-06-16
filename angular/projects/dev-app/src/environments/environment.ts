import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl: 'http://localhost:4200',
    name: 'MvcSample',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44315',
    redirectUri: baseUrl,
    clientId: 'MvcSample_App',
    responseType: 'code',
    scope: 'offline_access MvcSample role email openid profile',
  },
  apis: {
    default: {
      url: 'https://localhost:44315',
      rootNamespace: '',
    },
    EntityUi: {
      url: 'https://localhost:44315',
      rootNamespace: '',
    },
  },
} as Environment;
