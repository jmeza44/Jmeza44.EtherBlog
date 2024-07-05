import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PostsListComponent } from './components/posts-list/posts-list.component';
import { ReadingPostComponent } from './components/reading-post/reading-post.component';
import { CreatePostComponent } from './components/create-post/create-post.component';
import { EditPostComponent } from './components/edit-post/edit-post.component';
import { editorGuard } from '../shared/guards/editor.guard';

const routes: Routes = [
  { path: '', component: PostsListComponent, data: { type: 'all' } },
  { path: 'mine', component: PostsListComponent, data: { type: 'mine' } },
  { path: 'create', component: CreatePostComponent, canActivate: [editorGuard] },
  { path: 'edit/:id', component: EditPostComponent, canActivate: [editorGuard] },
  { path: ':id', component: ReadingPostComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PostsRoutingModule {}
