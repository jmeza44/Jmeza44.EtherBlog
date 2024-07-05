import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Comment } from '../../../shared/models/comments/comment.model';
import { AuthService } from '../../../shared/services/auth-service.service';
import { CommentService } from '../../../shared/services/comments.service';

@Component({
  selector: 'ether-comments-list',
  templateUrl: './comments-list.component.html',
  styleUrl: './comments-list.component.scss'
})
export class CommentsListComponent {
  @Input() comments: Comment[] = [];
  @Output() commentAdded = new EventEmitter<void>();

  constructor() {}

  onDelete() {
    this.commentAdded.emit();
  }
}
