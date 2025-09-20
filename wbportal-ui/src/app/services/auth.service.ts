import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, tap } from 'rxjs';
import { LoginRequest, RegisterRequest, AuthResponse, User } from '../models/auth.models';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly API_URL = 'http://localhost:5029/api';
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();

  // Signals for reactive state management
  public isAuthenticated = signal(false);
  public userRole = signal<string>('');

  constructor(private http: HttpClient) {
    // Check for existing token on service initialization
    this.checkAuthStatus();
  }

  login(credentials: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.API_URL}/auth/login`, credentials).pipe(
      tap((response) => {
        localStorage.setItem('token', response.token);
        localStorage.setItem('role', response.role);
        this.isAuthenticated.set(true);
        this.userRole.set(response.role);
        // Create a basic user object from the response
        const user: User = {
          id: 0, // We don't get this from login response
          username: credentials.username,
          email: '', // We don't get this from login response
          role: response.role,
        };
        this.currentUserSubject.next(user);
      })
    );
  }

  register(userData: RegisterRequest): Observable<any> {
    return this.http.post(`${this.API_URL}/auth/register`, userData);
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    this.isAuthenticated.set(false);
    this.userRole.set('');
    this.currentUserSubject.next(null);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getRole(): string | null {
    return localStorage.getItem('role');
  }

  private checkAuthStatus(): void {
    const token = this.getToken();
    const role = this.getRole();

    if (token && role) {
      this.isAuthenticated.set(true);
      this.userRole.set(role);
      // We could make an API call here to get full user details
      // For now, we'll create a basic user object
      const user: User = {
        id: 0,
        username: 'User', // We don't have this stored
        email: '',
        role: role,
      };
      this.currentUserSubject.next(user);
    }
  }

  isLoggedIn(): boolean {
    return this.isAuthenticated();
  }

  hasRole(role: string): boolean {
    return this.userRole() === role;
  }

  isAdmin(): boolean {
    return this.hasRole('Admin');
  }

  isManager(): boolean {
    return this.hasRole('Manager');
  }
}
