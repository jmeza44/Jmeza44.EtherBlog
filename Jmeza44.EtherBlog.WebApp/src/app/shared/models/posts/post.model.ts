export interface Post {
  id: number;
  title: string;
  content: string;
  createdBy: string;
  createdAt: Date;
  lastModifiedBy?: string | null; // Optional field, represented by '?' in TypeScript
  lastModifiedByAt?: Date | null; // Optional field
}
