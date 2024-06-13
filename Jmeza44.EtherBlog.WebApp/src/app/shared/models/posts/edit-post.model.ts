import { CreatePost } from './create-post.model';

export interface EditPost extends CreatePost {
  id: number;
}
