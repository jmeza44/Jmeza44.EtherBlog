export interface Comment {
  id: number;
  content: string;
  postId: number;
  createdBy?: string;
  createdAt?: Date;
  lastModifiedBy?: string;
  lastModifiedByAt?: Date;
}
