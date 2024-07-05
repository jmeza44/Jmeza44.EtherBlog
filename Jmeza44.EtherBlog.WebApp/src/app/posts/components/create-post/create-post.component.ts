import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CreatePost } from '../../../shared/models/posts/create-post.model';
import { PostService } from '../../../shared/services/posts.service';
import { Router } from '@angular/router';

@Component({
  selector: 'ether-create-post',
  templateUrl: './create-post.component.html',
  styleUrl: './create-post.component.scss'
})
export class CreatePostComponent {
  postForm: FormGroup;
  markdownContent: string = '';
  editorOptions = {
    showPreviewPanel: true,
    resizable: true,
    placeholder: 'Write your post here using Markdown Syntax...'
  };

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private postService: PostService
  ) {
    this.postForm = this.formBuilder.group({
      title: ['', Validators.required],
      content: ['', Validators.required]
    });
  }

  submitPost() {
    if (this.postForm.invalid) {
      return;
    }

    const newPost: CreatePost = {
      title: this.postForm.value.title,
      content: this.markdownContent
    };

    this.postService.create(newPost).subscribe({
      next: (postId) => {
        console.log('Post created successfully');
        this.postForm.reset();
        this.markdownContent = '';
        this.router.navigate(['/', 'posts', postId]);
      },
      error: error => {
        console.error('Error creating post:', error);
      }
    });
  }
}
