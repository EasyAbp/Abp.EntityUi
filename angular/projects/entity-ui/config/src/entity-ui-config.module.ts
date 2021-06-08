import { ModuleWithProviders, NgModule } from '@angular/core';
import { ENTITY_UI_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class EntityUiConfigModule {
  static forRoot(): ModuleWithProviders<EntityUiConfigModule> {
    return {
      ngModule: EntityUiConfigModule,
      providers: [ENTITY_UI_ROUTE_PROVIDERS],
    };
  }
}
