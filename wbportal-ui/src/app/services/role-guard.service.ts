import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { UserRole } from '../models/auth.models';

@Injectable({
  providedIn: 'root',
})
export class RoleGuardService {
  constructor(private authService: AuthService) {}

  canViewProjects(): boolean {
    return this.authService.isLoggedIn();
  }

  canEditProject(projectManagerId: number): boolean {
    const currentUser = this.authService.currentUser$;
    // This would need to be implemented with proper user ID checking
    return this.authService.isAdmin() || this.authService.isManager();
  }

  canManageUsers(): boolean {
    return this.authService.isAdmin();
  }

  canManageManagers(): boolean {
    return this.authService.isAdmin();
  }

  canViewSDGScores(): boolean {
    return this.authService.isLoggedIn();
  }

  canGiveReviews(): boolean {
    return this.authService.isLoggedIn();
  }

  canEditOwnProjects(): boolean {
    return this.authService.isManager() || this.authService.isAdmin();
  }

  hasRole(role: UserRole): boolean {
    return this.authService.hasRole(role);
  }

  getCurrentUserRole(): string {
    return this.authService.userRole();
  }
}
