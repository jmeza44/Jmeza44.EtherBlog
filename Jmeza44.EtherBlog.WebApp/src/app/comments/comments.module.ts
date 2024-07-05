import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

import { CommentsRoutingModule } from './comments-routing.module';
import { AddCommentComponent } from './components/add-comment/add-comment.component';
import { CommentsListComponent } from './components/comments-list/comments-list.component';
import { CommentComponent } from './components/comment/comment.component';

@NgModule({
  declarations: [
    CommentsListComponent,
    AddCommentComponent,
    CommentComponent
  ],
  imports: [
    CommonModule,
    CommentsRoutingModule,
    SharedModule
  ],
  exports: [
    CommentsListComponent,
    AddCommentComponent,
    CommentComponent
  ]
})
export class CommentsModule {}
