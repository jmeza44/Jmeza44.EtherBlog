import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CreatePost } from '../models/posts/create-post.model';
import { EditPost } from '../models/posts/edit-post.model';
import { Post } from '../models/posts/post.model';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private apiBaseUrl = `${environment.apiBaseUrl}/api/posts`;

  constructor(private http: HttpClient) {}

  getById(id: number): Observable<Post> {
    return this.http.get<Post>(`${this.apiBaseUrl}/${id}`);
  }

  getAll(): Observable<Post[]> {
    return this.http.get<Post[]>(`${this.apiBaseUrl}/GetAll`);
  }

  create(post: CreatePost): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiBaseUrl}/Create`, post);
  }

  edit(post: EditPost): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiBaseUrl}/Edit`, post);
  }

  delete(id: number): Observable<boolean> {
    const params = new HttpParams().set('id', id.toString());
    return this.http.delete<boolean>(`${this.apiBaseUrl}/Delete`, { params });
  }
}
