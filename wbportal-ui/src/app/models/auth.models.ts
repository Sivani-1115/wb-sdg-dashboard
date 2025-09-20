export interface LoginRequest {
  username: string;
  password: string;
}

export interface RegisterRequest {
  username: string;
  email: string;
  password: string;
  role: string;
}

export interface AuthResponse {
  token: string;
  role: string;
}

export interface User {
  id: number;
  username: string;
  email: string;
  role: 'User' | 'Admin' | 'Manager';
}

export type UserRole = 'User' | 'Admin' | 'Manager';
