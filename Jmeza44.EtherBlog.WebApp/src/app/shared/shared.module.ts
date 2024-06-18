import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AngularMaterialComponentsModule } from '../angular-material-components/angular-material-components.module';
import { SignInCallbackComponent } from './components/sign-in-callback/sign-in-callback.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';

@NgModule({
  declarations: [
    SignInCallbackComponent,
    NavBarComponent
  ],
  imports: [
    CommonModule,
    AngularMaterialComponentsModule,
  ],
  exports: [
    AngularMaterialComponentsModule,
    SignInCallbackComponent,
    NavBarComponent
  ]
})
export class SharedModule {}
