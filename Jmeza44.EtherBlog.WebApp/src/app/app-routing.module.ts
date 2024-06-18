import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignInCallbackComponent } from './shared/components/sign-in-callback/sign-in-callback.component';

const routes: Routes = [
  { path: 'sign-in-callback', component: SignInCallbackComponent },
  { path: 'posts', loadChildren: () => import('./posts/posts.module').then(m => m.PostsModule) },
  { path: 'comments', loadChildren: () => import('./comments/comments.module').then(m => m.CommentsModule) },
  { path: 'users', loadChildren: () => import('./users/users.module').then(m => m.UsersModule) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
