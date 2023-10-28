import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Product } from 'src/app/core/models/domain/product';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private http: HttpClient) {}
  upsertProduct(model: Product): Observable<void> {
    return this.http.put<void>(`${environment.apiEndpoint}/products`, model);
  }
}
