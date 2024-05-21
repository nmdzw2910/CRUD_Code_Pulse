import { Component, OnInit, computed } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from 'src/app/core/models/domain/product';
import { ProductService } from '../service/product.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css'],
})
export class ProductDetailComponent implements OnInit {
  productId: string = '';
  product: Product;
  selectedQuantity: number = 1;

  get imagesUrl(): string[] {
    return this.product.productImages?.map((image) => image.url) ?? [];
  }

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.product = {
      name: '',
    };
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.loadProduct(params['id']);
    });
  }

  private loadProduct(productId: string): void {
    this.productService.getProductById(productId).subscribe({
      next: (response) => {
        this.product = response;
      },
      error: (error) => {
        this.toastr.error(error);
      },
    });
  }

  decreaseQuantity(): void {
    if (this.selectedQuantity > 1) {
      this.selectedQuantity--;
    }
  }

  increaseQuantity(): void {
    if (this.product.stock && this.selectedQuantity < this.product.stock) {
      this.selectedQuantity++;
    }
  }

  addToCart(): void {
    console.log('Added to cart:', this.selectedQuantity);
  }

  buyNow(): void {
    console.log('Buy Now:', this.selectedQuantity);
  }
}
