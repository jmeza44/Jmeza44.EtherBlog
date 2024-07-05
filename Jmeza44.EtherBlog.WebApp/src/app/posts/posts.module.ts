import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MarkdownModule } from 'ngx-markdown';
import { CommentsModule } from '../comments/comments.module';
import { SharedModule } from '../shared/shared.module';
import { PostsListComponent } from './components/posts-list/posts-list.component';
import { ReadingPostComponent } from './components/reading-post/reading-post.component';
import { PostsRoutingModule } from './posts-routing.module';
import { CreatePostComponent } from './components/create-post/create-post.component';
import { EditPostComponent } from './components/edit-post/edit-post.component';

@NgModule({
  declarations: [
    PostsListComponent,
    ReadingPostComponent,
    CreatePostComponent,
    EditPostComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    PostsRoutingModule,
    CommentsModule,
    MarkdownModule.forChild()
  ],
  exports: [
    PostsListComponent,
    CreatePostComponent,
    EditPostComponent
  ]
})
export class PostsModule {}
