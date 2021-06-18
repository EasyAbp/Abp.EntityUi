import { NgModule } from '@angular/core';
import { DynamicLayoutComponent } from '@abp/ng.core';
import { Routes, RouterModule } from '@angular/router';
import { EntityUiComponent } from './components/entity-ui.component';
import { DynamicComponent } from './components/dynamic.component';

const routes: Routes = [
  {
    path: '',
    component: DynamicLayoutComponent,
    pathMatch:'full',
    children: [
      {
        path: '',
        component: DynamicComponent,
      }
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EntityUiRoutingModule {}
