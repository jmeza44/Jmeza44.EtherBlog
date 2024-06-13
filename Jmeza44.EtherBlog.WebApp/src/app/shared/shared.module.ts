import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AngularMaterialComponentsModule } from '../angular-material-components/angular-material-components.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    AngularMaterialComponentsModule,
  ],
  exports: [AngularMaterialComponentsModule]
})
export class SharedModule { }
