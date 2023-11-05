import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Product } from 'src/app/core/models/domain/product';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private baseUrl = `${environment.apiEndpoint}/products`;

  constructor(private http: HttpClient) {}

  getAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseUrl}`);
  }

  upsertProduct(model: FormData): Observable<Product> {
    return this.http.put<Product>(`${this.baseUrl}`, model);
  }
}
