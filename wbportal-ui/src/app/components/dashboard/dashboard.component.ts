import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ProjectsService } from '../../services/projects.service';
import { FeedbackService } from '../../services/feedback.service';
import { WorldBankDataService } from '../../services/worldbank-data.service';
import { RoleGuardService } from '../../services/role-guard.service';
import { SdgChartComponent } from '../sdg-chart/sdg-chart.component';
import { Project, ProjectStats } from '../../models/project.models';
import { Feedback } from '../../models/feedback.models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, SdgChartComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent implements OnInit {
  projects = signal<Project[]>([]);
  feedbacks = signal<Feedback[]>([]);
  isLoading = signal(false);
  errorMessage = signal('');

  // Dashboard stats
  projectStats = signal<ProjectStats>({
    totalProjects: 0,
    activeProjects: 0,
    closedProjects: 0,
    totalCommitment: 0,
    averageSDGScore: 0,
    topPerformingSDG: 0,
  });
  totalFeedbacks = signal(0);
  averageRating = signal(0);

  // Role-based access
  canManageUsers = signal(false);
  canEditProjects = signal(false);
  canGiveReviews = signal(false);

  constructor(
    public authService: AuthService,
    private projectsService: ProjectsService,
    private feedbackService: FeedbackService,
    private worldBankDataService: WorldBankDataService,
    private roleGuardService: RoleGuardService,
    private router: Router
  ) {}

  ngOnInit(): void {
    if (!this.authService.isLoggedIn()) {
      this.router.navigate(['/login']);
      return;
    }

    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    // Load sample data first for better UX
    this.loadSampleData();

    // Try to load real data from API
    this.projectsService.getProjects().subscribe({
      next: (projects) => {
        this.projects.set(projects);
        this.calculateProjectStats(projects);
        this.isLoading.set(false);
      },
      error: (error) => {
        console.error('Error loading projects:', error);
        // Keep sample data if API fails
        this.isLoading.set(false);
      },
    });

    // Load feedbacks
    this.feedbackService.getFeedbacks().subscribe({
      next: (feedbacks) => {
        this.feedbacks.set(feedbacks);
        this.calculateFeedbackStats(feedbacks);
      },
      error: (error) => {
        console.error('Error loading feedbacks:', error);
        // Keep sample data if API fails
      },
    });
  }

  private loadSampleData(): void {
    // Load real World Bank project data
    const worldBankProjects = this.worldBankDataService.getWorldBankProjects();
    this.projects.set(worldBankProjects);
    this.calculateProjectStats(worldBankProjects);

    // Set role-based permissions
    this.canManageUsers.set(this.roleGuardService.canManageUsers());
    this.canEditProjects.set(this.roleGuardService.canEditOwnProjects());
    this.canGiveReviews.set(this.roleGuardService.canGiveReviews());

    // Sample feedbacks data
    const sampleFeedbacks: Feedback[] = [
      {
        id: 1,
        content:
          'Excellent progress on the rural development project. Community engagement has been outstanding.',
        rating: 5,
        userId: 1,
        projectId: 1,
        createdAt: '2024-09-15T10:30:00Z',
        user: {
          id: 1,
          username: 'john.doe',
          email: 'john.doe@worldbank.org',
        },
        project: {
          id: 1,
          title: 'Rural Development and Climate Resilience Project',
        },
      },
      {
        id: 2,
        content:
          'The water and sanitation program exceeded expectations. Great collaboration with local partners.',
        rating: 5,
        userId: 2,
        projectId: 2,
        createdAt: '2024-09-10T14:20:00Z',
        user: {
          id: 2,
          username: 'sarah.smith',
          email: 'sarah.smith@worldbank.org',
        },
        project: {
          id: 2,
          title: 'Urban Water and Sanitation Improvement',
        },
      },
      {
        id: 3,
        content:
          'Education initiative shows excellent results. Digital platforms are working well.',
        rating: 4,
        userId: 3,
        projectId: 3,
        createdAt: '2024-09-08T09:15:00Z',
        user: {
          id: 3,
          username: 'mike.johnson',
          email: 'mike.johnson@worldbank.org',
        },
        project: {
          id: 3,
          title: 'Education for All Initiative',
        },
      },
    ];

    this.feedbacks.set(sampleFeedbacks);
    this.calculateFeedbackStats(sampleFeedbacks);
  }

  private calculateProjectStats(projects: Project[]): void {
    const totalProjects = projects.length;
    const activeProjects = projects.filter((p) => p.status === 'Active').length;
    const closedProjects = projects.filter((p) => p.status === 'Closed').length;
    const totalCommitment = projects.reduce((sum, p) => sum + p.commitmentAmount, 0);

    // Calculate average SDG score
    let totalSDGScore = 0;
    let sdgCount = 0;
    projects.forEach((project) => {
      if (project.sdgScores) {
        project.sdgScores.forEach((score) => {
          totalSDGScore += score.score;
          sdgCount++;
        });
      }
    });
    const averageSDGScore = sdgCount > 0 ? Math.round(totalSDGScore / sdgCount) : 0;

    this.projectStats.set({
      totalProjects,
      activeProjects,
      closedProjects,
      totalCommitment,
      averageSDGScore,
      topPerformingSDG: 0, // This would need more complex calculation
    });
  }

  private calculateFeedbackStats(feedbacks: Feedback[]): void {
    this.totalFeedbacks.set(feedbacks.length);
    if (feedbacks.length > 0) {
      const totalRating = feedbacks.reduce((sum, feedback) => sum + feedback.rating, 0);
      this.averageRating.set(Math.round((totalRating / feedbacks.length) * 10) / 10);
    }
  }

  getStatusColor(status: string): string {
    switch (status) {
      case 'Closed':
        return '#10b981';
      case 'Active':
        return '#3b82f6';
      case 'Pipeline':
        return '#f59e0b';
      case 'Dropped':
        return '#ef4444';
      default:
        return '#6b7280';
    }
  }

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString();
  }

  formatCurrency(amount: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      minimumFractionDigits: 0,
    }).format(amount);
  }

  formatLargeNumber(amount: number): string {
    if (amount >= 1000000000) {
      return `$${(amount / 1000000000).toFixed(1)}B`;
    } else if (amount >= 1000000) {
      return `$${(amount / 1000000).toFixed(1)}M`;
    } else if (amount >= 1000) {
      return `$${(amount / 1000).toFixed(1)}K`;
    }
    return `$${amount}`;
  }

  getScoreColor(score: number): string {
    if (score >= 80) return '#10b981'; // Green
    if (score >= 60) return '#f59e0b'; // Yellow
    if (score >= 40) return '#f97316'; // Orange
    return '#ef4444'; // Red
  }

  viewProjectDetails(project: Project): void {
    // This would open a modal or navigate to project details
    console.log('Viewing project details:', project);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  goToProjects(): void {
    this.router.navigate(['/projects']);
  }

  goToFeedbacks(): void {
    this.router.navigate(['/feedbacks']);
  }
}
