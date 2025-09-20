import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Feedback, CreateFeedbackRequest } from '../models/feedback.models';

@Injectable({
  providedIn: 'root',
})
export class FeedbackService {
  private readonly API_URL = 'http://localhost:5029/api';

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    });
  }

  getFeedbacks(): Observable<Feedback[]> {
    return this.http.get<Feedback[]>(`${this.API_URL}/feedback`, { headers: this.getHeaders() });
  }

  getFeedback(id: number): Observable<Feedback> {
    return this.http.get<Feedback>(`${this.API_URL}/feedback/${id}`, {
      headers: this.getHeaders(),
    });
  }

  createFeedback(feedback: CreateFeedbackRequest): Observable<Feedback> {
    return this.http.post<Feedback>(`${this.API_URL}/feedback`, feedback, {
      headers: this.getHeaders(),
    });
  }

  updateFeedback(id: number, feedback: Partial<CreateFeedbackRequest>): Observable<Feedback> {
    return this.http.put<Feedback>(`${this.API_URL}/feedback/${id}`, feedback, {
      headers: this.getHeaders(),
    });
  }

  deleteFeedback(id: number): Observable<void> {
    return this.http.delete<void>(`${this.API_URL}/feedback/${id}`, { headers: this.getHeaders() });
  }
}
