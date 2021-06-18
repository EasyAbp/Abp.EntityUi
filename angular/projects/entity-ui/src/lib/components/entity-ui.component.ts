import { ListService, PagedResultDto } from '@abp/ng.core';
import {
  EXTENSIONS_IDENTIFIER,
  FormPropData,
  generateFormFromProps,
} from '@abp/ng.theme.shared/extensions';
import { Component, Injector, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Observable } from 'rxjs';
import { EntityService, EntityUiService } from '../services/entity-ui.service';
import { filter, mergeMap, switchMap, take, tap } from 'rxjs/operators';
import { FormGroup } from '@angular/forms';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ActionEvent } from '../providers/action-event.hub';
export type EntityUiProfile = {
  entityService: EntityService<any>;
  entityKey: string;
};
export function getValue(service: EntityUiService) {
  return service.getEntityUiKey();
}

@Component({
  selector: 'lib-entity-ui',
  template: `
    <ng-container *ngIf="data$ | async as data">
      <abp-page [title]="entityUiProfile.entityKey" [toolbar]="data"> </abp-page>
      <abp-extensible-table
        [data]="data.items"
        [recordsTotal]="data.totalCount"
        [list]="list"
      ></abp-extensible-table>
    </ng-container>

    <abp-modal [(visible)]="createModalVisible">
      <ng-template #abpHeader>
        <h5 class="modal-title" id="modal-basic-title">Create</h5>
      </ng-template>
      <ng-template #abpBody>
        <form [formGroup]="createForm">
          <abp-extensible-form [selectedRecord]="selectedRecord"></abp-extensible-form>
        </form>
      </ng-template>
      <ng-template #abpFooter>
        <button type="button" class="btn btn-secondary" abpClose>
          {{ 'AbpUi::Cancel' | abpLocalization }}
        </button>
        <abp-button iconClass="fa fa-check" (click)="create()">{{
          'AbpUi::Save' | abpLocalization
        }}</abp-button>
      </ng-template>
    </abp-modal>

    <abp-modal [(visible)]="editModalVisible">
      <ng-template #abpHeader>
        <h5 class="modal-title" id="modal-basic-title">E</h5>
      </ng-template>
      <ng-template #abpBody>
        <form [formGroup]="editForm">
          <abp-extensible-form [selectedRecord]="selectedRecord"></abp-extensible-form>
        </form>
      </ng-template>
      <ng-template #abpFooter>
        <button type="button" class="btn btn-secondary" abpClose>
          {{ 'AbpUi::Cancel' | abpLocalization }}
        </button>
        <abp-button iconClass="fa fa-check" (click)="edit()">{{
          'AbpUi::Save' | abpLocalization
        }}</abp-button>
      </ng-template>
    </abp-modal>
  `,
  styles: [],
  providers: [
    ListService,
    {
      provide: EXTENSIONS_IDENTIFIER,
      useFactory: getValue,
      deps: [EntityUiService],
    },
  ],
})
export class EntityUiComponent<R> implements OnInit, OnChanges {
  createModalVisible = false;
  createForm: FormGroup;
  editModalVisible = false;
  editForm: FormGroup;
  selectedRecord = {};

  @Input() entityUiProfile: EntityUiProfile;

  constructor(
    private injector: Injector,
    private entityUiService: EntityUiService,
    public readonly list: ListService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (!changes['entityUiProfile']) return;
    this.entityUiService
      .setDefaults(this.entityUiProfile.entityKey)
      .pipe(
        tap(x => this.hookToQuery()),
        mergeMap(x => x),
        tap(x => this.action(x))
      )
      .subscribe();    
  }

  data$: Observable<PagedResultDto<R>>;

  ngOnInit(): void {

  }

  action(action: ActionEvent<R>) {
    switch (action.method) {
      case 'Create':
        this.selectedRecord = null;
        const data = new FormPropData(this.injector, {} as R);
        this.createForm = generateFormFromProps(data);
        this.createModalVisible = true;
        break;
      case 'Edit':
        this.entityUiProfile.entityService
          .get(action.data.record)
          .pipe(take(1))
          .pipe(
            tap(selected => {
              this.selectedRecord = selected;
              const data = new FormPropData(this.injector, selected);
              this.editForm = generateFormFromProps(data);
            })
          )
          .subscribe(x => {
            this.editModalVisible = true;
          });
        break;
      case 'Delete':
        this.confirmationService
          .warn('Forms::FormDeletionConfirmationMessage', 'CmsKit::AreYouSure')
          .pipe(
            filter(status => status === Confirmation.Status.confirm),
            switchMap(_ => this.entityUiProfile.entityService.delete(action.data.record)),
            take(1)
          )
          .subscribe(_ => {
            this.list.get();
          });
        break;
    }
  }
  hookToQuery() {
    let streamCreator = q => this.entityUiProfile.entityService.getList({} as any);
    this.data$ = this.list.hookToQuery(streamCreator) as any;
  }

  create() {
    this.entityUiProfile.entityService
      .create(this.createForm.value)
      .pipe(take(1))
      .subscribe(_ => {
        this.createModalVisible = false;
        this.list.get();
      });
  }
  edit() {
    const request: any = {
      ...this.editForm.value,
    };

    this.entityUiProfile.entityService
      .update(this.editForm.value.id, request)
      .pipe(take(1))
      .subscribe(_ => {
        this.editModalVisible = false;
        this.list.get();
      });
  }
}
