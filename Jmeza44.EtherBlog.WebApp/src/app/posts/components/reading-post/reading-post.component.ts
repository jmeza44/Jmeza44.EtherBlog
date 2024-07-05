import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PostService } from '../../../shared/services/posts.service';
import { Post } from '../../../shared/models/posts/post.model';
import { Comment } from '../../../shared/models/comments/comment.model';
import { CommentService } from '../../../shared/services/comments.service';
import { AuthService } from '../../../shared/services/auth-service.service';

@Component({
  selector: 'ether-reading-post',
  templateUrl: './reading-post.component.html',
  styleUrl: './reading-post.component.scss'
})
export class ReadingPostComponent implements OnInit {
  postId!: number;
  post: Post | null = null;
  comments: Comment[] = [];

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private postService: PostService,
    private commentService: CommentService,
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.postId = Number.parseInt(params.get('id') ?? '0');
      this.loadPost(this.postId);
      this.loadPostComments(this.postId);
    });
  }

  onCommentAdded() {
    this.loadPostComments(this.postId);
  }

  onCommentDeleted() {
    this.loadPostComments(this.postId);
  }

  isMyPost(postCreatedBy: string): boolean {
    return postCreatedBy === this.authService.user?.profile.email;
  }

  private loadPost(id: number): void {
    this.postService.getById(id).subscribe(post => this.post = post);
  }

  private loadPostComments(postId: number) {
    this.commentService.getPostComments(postId)
      .subscribe(comments => this.comments = comments);
  }
}
