import { Feedback } from './feedback.models';

export interface Project {
  id: number;
  projectTitle: string;
  country: string;
  projectId: string;
  commitmentAmount: number;
  status: 'Active' | 'Closed' | 'Dropped' | 'Pipeline';
  approvalDate: string;
  lastUpdatedDate: string;
  lastStageReached: string;
  projectManagerId: number;
  projectManager?: {
    id: number;
    username: string;
    email: string;
  };
  feedbacks?: Feedback[];
  sdgScores?: SDGScore[];
}

export interface SDGScore {
  sdgNumber: number;
  sdgTitle: string;
  score: number; // 0-100
  description: string;
}

export interface ProjectStats {
  totalProjects: number;
  activeProjects: number;
  closedProjects: number;
  totalCommitment: number;
  averageSDGScore: number;
  topPerformingSDG: number;
}

export interface CreateProjectRequest {
  title: string;
  description: string;
  status: string;
  startDate: string;
  endDate: string;
  budget: number;
  projectManagerId: number;
}

export interface UpdateProjectRequest {
  title?: string;
  description?: string;
  status?: string;
  startDate?: string;
  endDate?: string;
  budget?: number;
  projectManagerId?: number;
}
