export interface Post {
  id: number;
  title: string;
  content: string;
  createdBy: string;
  createdAt: Date;
  lastModifiedBy?: string | null;
  lastModifiedByAt?: Date | null;
}
