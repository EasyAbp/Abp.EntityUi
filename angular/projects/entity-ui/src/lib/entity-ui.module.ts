import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { EntityUiComponent } from './components/entity-ui.component';
import { EntityUiRoutingModule } from './entity-ui-routing.module';
import { UiExtensionsModule } from '@abp/ng.theme.shared/extensions';
import { PageModule } from '@abp/ng.components/page';
import { NgxValidateCoreModule } from '@ngx-validate/core';

@NgModule({
  declarations: [EntityUiComponent],
  imports: [
    CoreModule,
    ThemeSharedModule,
    EntityUiRoutingModule,
    UiExtensionsModule,
    PageModule,
    NgxValidateCoreModule
  ],
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
