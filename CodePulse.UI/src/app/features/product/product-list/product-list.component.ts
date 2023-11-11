import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/core/models/domain/product';
import { ProductService } from '../service/product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  successMessage: string = '';
  errorMessage: string = '';

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe({
      next: (response) => {
        this.successMessage = 'Product added successfully';
        this.products = response;
      },
      error: (error) => {
        this.errorMessage = 'Error adding the product';
        console.error('Error:', error);
      },
    });
  }
}
