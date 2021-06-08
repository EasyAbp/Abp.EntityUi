import { Component, OnInit } from '@angular/core';
import { EntityUiService } from '../services/entity-ui.service';

@Component({
  selector: 'lib-entity-ui',
  template: ` <p>entity-ui works!</p> `,
  styles: [],
})
export class EntityUiComponent implements OnInit {
  constructor(private service: EntityUiService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
