import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PaginatedData } from '../models/paginated-data.model';
import { CreatePost } from '../models/posts/create-post.model';
import { EditPost } from '../models/posts/edit-post.model';
import { GetAllPosts } from '../models/posts/get-all-posts.model';
import { Post } from '../models/posts/post.model';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private apiBaseUrl = `${environment.apiBaseUrl}/api/post`;

  constructor(private http: HttpClient) {}

  getById(id: number): Observable<Post> {
    return this.http.get<Post>(`${this.apiBaseUrl}/${id}`);
  }

  getAll(getOptions: GetAllPosts): Observable<PaginatedData<Post>> {
    const params = new HttpParams({ fromObject: { ...getOptions } });
    return this.http.get<PaginatedData<Post>>(`${this.apiBaseUrl}/GetAll`, { params });
  }

  create(post: CreatePost): Observable<number> {
    return this.http.post<number>(`${this.apiBaseUrl}/Create`, post);
  }

  edit(post: EditPost): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiBaseUrl}/Edit`, post);
  }

  delete(id: number): Observable<boolean> {
    const params = new HttpParams().set('id', id.toString());
    return this.http.delete<boolean>(`${this.apiBaseUrl}/Delete`, { params });
  }
}
