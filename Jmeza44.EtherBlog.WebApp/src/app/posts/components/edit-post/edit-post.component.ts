import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, NonNullableFormBuilder } from '@angular/forms';
import { PostService } from '../../../shared/services/posts.service';
import { ActivatedRoute, Router } from '@angular/router';
import { EditPost } from '../../../shared/models/posts/edit-post.model';
import { AuthService } from '../../../shared/services/auth-service.service';

@Component({
  selector: 'ether-edit-post',
  templateUrl: './edit-post.component.html',
  styleUrl: './edit-post.component.scss'
})
export class EditPostComponent implements OnInit {
  postForm: FormGroup;
  postId: number = 0;
  isMine: boolean = false;
  markdownContent: string = '';
  editorOptions = {
    showPreviewPanel: true,
    resizable: true,
    placeholder: 'Write your post here using Markdown Syntax...'
  };

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private postService: PostService,
    private authService: AuthService,
    private readonly formBuilder: NonNullableFormBuilder,
  ) {
    this.postForm = this.formBuilder.group({
      title: ['', Validators.required],
      content: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.postId = Number.parseInt(params.get('id') ?? '0');
      this.loadPost(this.postId);
    });
  }

  submitPost() {
    if (this.postForm.invalid) {
      return;
    }

    const editedPost: EditPost = {
      id: this.postId,
      title: this.postForm.value.title,
      content: this.markdownContent
    };

    this.postService.edit(editedPost).subscribe({
      next: () => {
        console.log('Post edited successfully');
        this.router.navigate(['/', 'posts', this.postId]);
      },
      error: error => {
        console.error('Error editing post:', error);
      }
    });
  }

  private loadPost(id: number): void {
    this.postService.getById(id).subscribe(post => {
      this.postForm.setValue({
        title: post.title,
        content: post.content,
      });
      this.isMine = post.createdBy === this.authService.user?.profile.email;

      if(!this.isMine) {
        this.router.navigate(['/', 'access-denied']);
      }
    });
  }
}
