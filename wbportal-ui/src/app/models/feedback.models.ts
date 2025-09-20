export interface Feedback {
  id: number;
  content: string;
  rating: number;
  userId: number;
  projectId: number;
  createdAt: string;
  user?: {
    id: number;
    username: string;
    email: string;
  };
  project?: {
    id: number;
    title: string;
  };
}

export interface CreateFeedbackRequest {
  content: string;
  rating: number;
  projectId: number;
}
