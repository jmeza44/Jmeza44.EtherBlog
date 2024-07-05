import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Comment } from '../../../shared/models/comments/comment.model';
import { AuthService } from '../../../shared/services/auth-service.service';
import { CommentService } from '../../../shared/services/comments.service';

@Component({
  selector: 'ether-comment',
  templateUrl: './comment.component.html',
  styleUrl: './comment.component.scss'
})
export class CommentComponent {
  @Input() comment!: Comment;
  @Output() commentDeleted = new EventEmitter<void>();

  constructor(
    public authService: AuthService,
    private commentService: CommentService,
  ) {}

  isEditing: boolean = false;
  editedContent: string = '';

  isMine(createdBy: string | undefined): boolean {
    return createdBy === this.authService.user?.profile.email;
  }

  onEdit() {
    this.isEditing = true;
    this.editedContent = this.comment.content;
  }

  onSave() {
    this.comment.content = this.editedContent;
    this.commentService.editComment(this.comment.id, this.comment)
      .subscribe({
        next: (comment) => this.comment = comment,
      });
    this.isEditing = false;
  }

  onDelete(commentId: number) {
    this.commentService.deleteComment(commentId)
      .subscribe({
        next: (deleted) => {
          if (deleted) {
            console.log("Comment deleted");
            this.commentDeleted.emit();
          }
        },
      });
  }
}
