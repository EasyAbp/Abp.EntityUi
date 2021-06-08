import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { EntityUiComponent } from './components/entity-ui.component';
import { EntityUiRoutingModule } from './entity-ui-routing.module';

@NgModule({
  declarations: [EntityUiComponent],
  imports: [CoreModule, ThemeSharedModule, EntityUiRoutingModule],
  exports: [EntityUiComponent],
})
export class EntityUiModule {
  static forChild(): ModuleWithProviders<EntityUiModule> {
    return {
      ngModule: EntityUiModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<EntityUiModule> {
    return new LazyModuleFactory(EntityUiModule.forChild());
  }
}
