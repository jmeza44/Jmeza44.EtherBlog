import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommentService } from '../../../shared/services/comments.service';
import { AddComment } from '../../../shared/models/comments/add-comment.model';

@Component({
  selector: 'ether-add-comment',
  templateUrl: './add-comment.component.html',
  styleUrl: './add-comment.component.scss'
})
export class AddCommentComponent {
  @Input() postId!: number;
  @Output() commentAdded = new EventEmitter<void>();
  commentContent: string = '';

  constructor(private commentService: CommentService) {}

  submitComment() {
    if (!this.commentContent.trim()) {
      return;
    }

    const newComment: AddComment = {
      postId: this.postId,
      content: this.commentContent
    };

    this.commentService.addComment(newComment).subscribe(
      () => {
        console.log('Comment added successfully');
        this.commentContent = '';
        this.commentAdded.emit();
      },
      error => {
        console.error('Error adding comment:', error);
      }
    );
  }
}
