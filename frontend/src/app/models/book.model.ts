export interface Book {
  id: number;
  title: string;
  author: string;
  description: string;
  imageCover: File | null;
}
