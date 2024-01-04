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
  hoveredProductId: string | undefined = '';
  isLoading: boolean = false;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.productService.getAllProducts().subscribe({
      next: (response) => {
        this.successMessage = 'Product added successfully';
        this.products = response;
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = 'Error adding the product';
        console.error('Error:', error);
        this.isLoading = false;
      },
    });
  }

  goToProductDetail(productId?: string) {
    // Implement navigation logic to the product detail page
    // e.g., using Angular Router
    // this.router.navigate(['/product', productId]);
  }

  setHoveredProduct(productId?: string): void {
    this.hoveredProductId = productId;
  }

  resetHoveredProduct(): void {
    this.hoveredProductId = '';
  }

  isHovered(product: Product): boolean {
    return this.hoveredProductId === product.id;
  }
}
