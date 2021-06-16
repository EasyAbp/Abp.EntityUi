import type { ModuleDto } from '../../modules/dtos/models';
import type { MenuItemDto } from '../../menu-items/dtos/models';
import type { EntityDto } from '../../entities/dtos/models';

export interface EntityUiIntegrationDto {
  modules: ModuleDto[];
  menuItems: MenuItemDto[];
  entities: EntityDto[];
}
