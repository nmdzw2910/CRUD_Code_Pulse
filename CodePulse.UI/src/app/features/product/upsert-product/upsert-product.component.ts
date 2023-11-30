import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/core/models/domain/product';
import { ProductService } from '../service/product.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'upsert-product',
  templateUrl: './upsert-product.component.html',
  styleUrls: ['./upsert-product.component.css'],
})
export class UpsertProductComponent implements OnInit {
  productId: string = '';
  product: Product;
  selectedImages: File[] = [];
  isEdit: boolean = false;

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.product = {
      name: '',
    };
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.isEdit = params['id'] !== undefined;

      if (this.isEdit) {
        this.productId = params['id'];
        this.loadProduct(this.productId);
      }
    });
  }

  onImageUpload(event: any) {
    this.selectedImages = event.target.files;
  }

  onFormSubmit(): void {
    const formData = this.upsertFormData(this.product);
    this.productService.upsertProduct(formData).subscribe({
      next: (response) => {
        this.product = response;
        console.log('success');
        this.router.navigate([`admin/products/edit/${this.product.id}`]);
      },
      error: (error) => {
        console.error('Error:', error);
      },
    });
  }

  upsertFormData(product: Product): FormData {
    const formData = new FormData();
    if (product.id) {
      formData.append('id', product.id || '');
    }
    formData.append('name', product.name);
    formData.append('description', product.description || '');
    formData.append('price', product.price?.toString() || '');
    formData.append('stock', product.stock?.toString() || '');
    formData.append('brand', product.brand || '');
    formData.append('category', product.category || '');

    Array.from(this.selectedImages).forEach((image) =>
      formData.append('image', image)
    );

    return formData;
  }

  private loadProduct(productId: string): void {
    this.productService.getProductById(productId).subscribe({
      next: (response) => {
        this.product = response;
      },
      error: (error) => {
        console.error('Error fetching product:', error);
      },
    });
  }
}
