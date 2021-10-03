import { Photo } from './photo';

export interface Book {
    id: number;
    title: string;
    subTitle: string;
    description: string;
    authorId: number;
    authorName: string;
    publishedDate: Date;
    photoUrl: string;
    publisher?: string;
    photos?: Photo[];
  }