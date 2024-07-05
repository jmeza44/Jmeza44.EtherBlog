import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { GetAllPosts } from '../../../shared/models/posts/get-all-posts.model';
import { Post } from '../../../shared/models/posts/post.model';
import { PostService } from '../../../shared/services/posts.service';
import { AuthService } from '../../../shared/services/auth-service.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'ether-posts-list',
  templateUrl: './posts-list.component.html',
  styleUrl: './posts-list.component.scss'
})
export class PostsListComponent implements OnInit {
  dataSource = new MatTableDataSource<Post>();
  totalCount: number = 0;
  pageSizeOptions: number[] = [5, 10, 25, 50];
  getOptions: GetAllPosts = {
    pageNumber: 1,
    pageSize: 10,
    createdBy: '',
  };

  constructor(
    private route: ActivatedRoute,
    public authService: AuthService,
    private postService: PostService,
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.getOptions.createdBy = (data['type'] === 'mine' && this.authService.user?.profile.email) ? this.authService.user?.profile.email : '';
    });
    this.loadPosts();
    console.log(this.authService.user);
  }

  loadPosts(): void {
    this.postService.getAll(this.getOptions)
      .subscribe({
        next: (data) => {
          this.dataSource.data = data.data;
          this.totalCount = data.totalCount;
        },
        error: (error) => {
          console.error('Error fetching posts:', error);
        }
      });
  }

  deletePost(postId: number): void {
    this.postService.delete(postId).subscribe({
      next: (wasDeleted) => {
        if (wasDeleted) {
          console.log(`Post ${postId} deleted`);
          this.loadPosts();
        } else console.error(`Post with id ${postId} couldn't be deleted`);
      },
      error: (error) => console.error(`Post with id ${postId} couldn't be deleted`, error),
    });
  }

  onPageChange(event: PageEvent): void {
    this.getOptions.pageNumber = event.pageIndex + 1;
    this.getOptions.pageSize = event.pageSize;
    this.loadPosts();
  }

  isMyPost(postCreatedBy: string): boolean {
    return postCreatedBy === this.authService.user?.profile.email;
  }
}
