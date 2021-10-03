export interface User {
  id: number;
  userName: string;
  created: Date;
  lastActive: Date;
  introduction?: string;
  roles?: string[];
}

