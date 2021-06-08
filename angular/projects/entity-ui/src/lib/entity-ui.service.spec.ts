import { TestBed } from '@angular/core/testing';

import { EntityUiService } from './entity-ui.service';

describe('EntityUiService', () => {
  let service: EntityUiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EntityUiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
