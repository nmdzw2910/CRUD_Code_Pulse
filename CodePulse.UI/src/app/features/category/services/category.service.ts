import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private baseUrl = `${environment.apiEndpoint}/categories`;

  constructor(private http: HttpClient) { }

  getAllCategories(): Observable<string[]> {
    return this.http.get<string[]>(this.baseUrl);
  }

  getCategoryByName(name: string): Observable<string> {
    return this.http.get<string>(`${this.baseUrl}/${name}`);
  }

  createCategory(name: string): Observable<any> {
    return this.http.post(this.baseUrl, { name }, {
      headers: { 'Content-Type': 'application/json' },
      responseType: 'text' as 'json'
    });
  }

  updateCategory(oldName: string, newName: string): Observable<any> {
    return this.http.put(`${this.baseUrl}/${oldName}`, {name: newName}, {
      headers: { 'Content-Type': 'application/json' },
      responseType: 'text' as 'json'
    });
  }

  deleteCategory(name: string): Observable<any> {
    return this.http.delete<void>(`${this.baseUrl}/${name}`, {
      headers: { 'Content-Type': 'application/json' },
      responseType: 'text' as 'json'
    });
  }
}
