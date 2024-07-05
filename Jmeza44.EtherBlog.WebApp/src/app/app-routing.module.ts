import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignInCallbackComponent } from './shared/components/sign-in-callback/sign-in-callback.component';
import { SignOutCallbackComponent } from './shared/components/sign-out-callback/sign-out-callback.component';
import { LandingPageComponent } from './shared/components/landing-page/landing-page.component';
import { LoginRequiredPageComponent } from './shared/components/login-required-page/login-required-page.component';
import { authGuard } from './shared/guards/auth.guard';
import { AccessDeniedComponent } from './shared/components/access-denied/access-denied.component';

const routes: Routes = [
  { path: '', component: LandingPageComponent, pathMatch: 'full' },
  { path: 'access-denied', component: AccessDeniedComponent, pathMatch: 'full' },
  { path: 'login-required', component: LoginRequiredPageComponent, pathMatch: 'full' },
  { path: 'sign-out-callback', component: SignOutCallbackComponent, pathMatch: 'full' },
  { path: 'sign-in-callback', component: SignInCallbackComponent, pathMatch: 'full' },
  { path: 'posts', loadChildren: () => import('./posts/posts.module').then(m => m.PostsModule), canActivate: [authGuard] },
  { path: 'comments', loadChildren: () => import('./comments/comments.module').then(m => m.CommentsModule) },
  { path: 'users', loadChildren: () => import('./users/users.module').then(m => m.UsersModule) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
