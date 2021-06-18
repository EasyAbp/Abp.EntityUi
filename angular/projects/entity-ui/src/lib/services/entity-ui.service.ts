import { Injectable, Injector } from '@angular/core';
import { ABP, InternalStore, PagedResultDto, RestService } from '@abp/ng.core';
import { EasyAbp } from '@easy-abp.Abp/entity-ui/proxy';
import { combineAll, concatAll, concatMap, map, mergeMap, switchMap, tap } from 'rxjs/operators';
import { setDefaults } from '../utils/defaults.util';
import { EntityInfo } from '../models/entity-info';
import { Observable } from 'rxjs';

export type EntityService<R> = {
  getList: (query: ABP.PageQueryParams) => Observable<PagedResultDto<R>>;
  get: (id: any) => Observable<R>;
  delete: (id: any) => Observable<R>;
  create: (input: any) => Observable<R>;
  update: (id: any, input: any) => Observable<R>;
};

export interface EntityUiState {
  entityUiKey: string;
}

@Injectable({
  providedIn: 'root',
})
export class EntityUiService {
  private readonly store = new InternalStore({} as EntityUiState);

  apiService: EasyAbp.Abp.EntityUi.Entities.EntityService;
  constructor(private injector: Injector) {
    this.apiService = injector.get(EasyAbp.Abp.EntityUi.Entities.EntityService);
  }

  getEntityUiKey() {
    return this.store.state['entityUiKey'];
  }

  setDefaults(entityUiKey: string) {
    this.store.patch({ entityUiKey });

    let names = entityUiKey.split('::', 2);
    const moduleName = names[0];
    const name = names[1];

    return this.apiService.get({ moduleName, name }).pipe(
      map(json => {
        let entityInfo = EntityInfo.transFrom(json);
        return setDefaults(this.injector, entityUiKey, {
          entityProps: entityInfo.EntityProps,
          toolbarActions: entityInfo.ToolbarActions,
          entityAction: entityInfo.EntityActions,
          createFormProps: entityInfo.CreateFormProps,
          editFormProps: entityInfo.EditFormProps,
        });
      })
    );
  }
}
