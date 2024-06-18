import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AddComment } from '../models/comments/add-comment.model';
import { EditComment } from '../models/comments/edit-comment.model';


@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private apiBaseUrl = `${environment.apiBaseUrl}/api/comments`;

  constructor(private http: HttpClient) {}

  getPostComments(postId: number): Observable<Comment[]> {
    return this.http.get<Comment[]>(`${this.apiBaseUrl}/Post/${postId}`);
  }

  addComment(command: AddComment): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiBaseUrl}/Add`, command);
  }

  editComment(commentId: number, command: EditComment): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiBaseUrl}/Edit/${commentId}`, command);
  }

  deleteComment(commentId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiBaseUrl}/Delete/${commentId}`);
  }
}
