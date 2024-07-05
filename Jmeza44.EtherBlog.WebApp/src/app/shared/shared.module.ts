import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { LMarkdownEditorModule } from 'ngx-markdown-editor';
import { AngularMaterialComponentsModule } from '../angular-material-components/angular-material-components.module';
import { LandingPageComponent } from './components/landing-page/landing-page.component';
import { LoginRequiredPageComponent } from './components/login-required-page/login-required-page.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { SignInCallbackComponent } from './components/sign-in-callback/sign-in-callback.component';
import { AccessDeniedComponent } from './components/access-denied/access-denied.component';

@NgModule({
  declarations: [
    SignInCallbackComponent,
    NavBarComponent,
    LandingPageComponent,
    LoginRequiredPageComponent,
    AccessDeniedComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    AngularMaterialComponentsModule,
    LMarkdownEditorModule
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    AngularMaterialComponentsModule,
    SignInCallbackComponent,
    NavBarComponent,
    LandingPageComponent,
    LoginRequiredPageComponent,
    LMarkdownEditorModule,
    AccessDeniedComponent
  ]
})
export class SharedModule {}
