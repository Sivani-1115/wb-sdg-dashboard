import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Project, CreateProjectRequest, UpdateProjectRequest } from '../models/project.models';

@Injectable({
  providedIn: 'root',
})
export class ProjectsService {
  private readonly API_URL = 'http://localhost:5029/api';

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    });
  }

  getProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(`${this.API_URL}/projects`, { headers: this.getHeaders() });
  }

  getProject(id: number): Observable<Project> {
    return this.http.get<Project>(`${this.API_URL}/projects/${id}`, { headers: this.getHeaders() });
  }

  createProject(project: CreateProjectRequest): Observable<Project> {
    return this.http.post<Project>(`${this.API_URL}/projects`, project, {
      headers: this.getHeaders(),
    });
  }

  updateProject(id: number, project: UpdateProjectRequest): Observable<Project> {
    return this.http.put<Project>(`${this.API_URL}/projects/${id}`, project, {
      headers: this.getHeaders(),
    });
  }

  deleteProject(id: number): Observable<void> {
    return this.http.delete<void>(`${this.API_URL}/projects/${id}`, { headers: this.getHeaders() });
  }
}
