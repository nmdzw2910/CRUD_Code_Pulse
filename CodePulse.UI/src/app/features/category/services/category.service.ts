import { Injectable } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private http: HttpClient) {}
  private baseUrl = 'https://localhost:7273';
  addCategory(model: AddCategoryRequest): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/api/categories`, model);
  }
}
