import { ListService, PagedResultDto } from '@abp/ng.core';
import { EXTENSIONS_IDENTIFIER, FormPropData, generateFormFromProps } from '@abp/ng.theme.shared/extensions';
import { Component, Injector, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { EntityUiService } from '../services/entity-ui.service';
import { EasyAbp, MvcSample } from '@easy-abp.Abp/entity-ui/proxy';
import { setDefaults } from '../utils/defaults.util';
import { filter, switchAll, switchMap, take, tap } from 'rxjs/operators';
import { FormGroup } from '@angular/forms';
import { ConfirmationService,Confirmation } from '@abp/ng.theme.shared';

@Component({
  selector: 'lib-entity-ui',
  template: `
    <ng-container *ngIf="data$ | async as data">
      <abp-page title="Books" [toolbar]="data"> </abp-page>
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
      useValue: 'MvcSample::Book',
    },
  ],
})
export class EntityUiComponent implements OnInit {
  createModalVisible = false;
  createForm: FormGroup;
  editModalVisible = false;
  editForm: FormGroup;
  selectedRecord = {};
  bookService:MvcSample.Books.BookService=null;
  constructor(
    private injector: Injector,
    private service: EntityUiService,
    public readonly list: ListService,
    private confirmationService: ConfirmationService
  ) {
    this.bookService = this.injector.get(MvcSample.Books.BookService);
  }

  data$: Observable<PagedResultDto<MvcSample.Books.Dtos.BookDto>>;
  ngOnInit(): void {
    this.service.sample().subscribe(x => {
      let entity = this.service.getEntity('MvcSample', 'Book');
      setDefaults(this.injector, 'MvcSample::Book', {
        entityProps: entity.EntityProps,
        toolbarActions: entity.ToolbarActions,
        entityAction: entity.EntityActions,
        createFormProps: entity.CreateFormProps,
        editFormProps:entity.EditFormProps
      }).subscribe(x => {
        console.log(x);
        switch (x.method) {
          case 'Create':
            this.selectedRecord=null;
            const data = new FormPropData(
              this.injector,
              {} as MvcSample.Books.Dtos.BookDto
            );
            this.createForm = generateFormFromProps(data);
            this.createModalVisible = true;            
            break;
            case 'Edit':
              this.bookService
              .get(x.data.record.id)
              .pipe(take(1))
              .pipe(
                tap((selected) => {
                  this.selectedRecord = selected;
                  const data = new FormPropData(this.injector, selected);
                  this.editForm = generateFormFromProps(data);
                })
              )
              .subscribe((x) => {
                this.editModalVisible = true;
              });              
              break;
              case 'Delete':
                this.confirmationService
                .warn('Forms::FormDeletionConfirmationMessage', 'CmsKit::AreYouSure')
                .pipe(
                  filter((status) => status === Confirmation.Status.confirm),
                  switchMap((_) => this.bookService.delete(x.data.record.id)),
                  take(1)
                )
                .subscribe((_) => {
                  this.list.get();
                });
                break;
        }
      });

      
      let streamCreator = q => this.bookService.getList({} as any);
      this.data$ = this.list.hookToQuery(streamCreator);
    });
  }

  create() {
    this.bookService
      .create(this.createForm.value)
      .pipe(take(1))
      .subscribe((_) => {
        this.createModalVisible = false;
        this.list.get();
      });
  }
  edit() {
    const request: MvcSample.Books.Dtos.CreateUpdateBookDto = {
      ...this.editForm.value,
    };

    this.bookService
      .update(this.editForm.value.id, request)
      .pipe(take(1))
      .subscribe((_) => {
        this.editModalVisible = false;
        this.list.get();
      });
  }
}
