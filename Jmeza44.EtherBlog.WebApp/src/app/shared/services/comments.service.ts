import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Comment } from '../models/comments/comment.model';
import { AddComment } from '../models/comments/add-comment.model';
import { EditComment } from '../models/comments/edit-comment.model';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private apiBaseUrl = `${environment.apiBaseUrl}/api/comment`;

  constructor(private http: HttpClient) {}

  getPostComments(postId: number): Observable<Comment[]> {
    return this.http.get<Comment[]>(`${this.apiBaseUrl}/Post/${postId}`);
  }

  addComment(command: AddComment): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiBaseUrl}/Add`, command);
  }

  editComment(commentId: number, command: EditComment): Observable<Comment> {
    return this.http.put<Comment>(`${this.apiBaseUrl}/Edit/${commentId}`, command);
  }

  deleteComment(commentId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiBaseUrl}/Delete/${commentId}`);
  }
}
