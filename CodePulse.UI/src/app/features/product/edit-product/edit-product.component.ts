import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/core/models/domain/product';
import { ProductService } from '../service/product.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css'],
})
export class EditProductComponent implements OnInit {
  productId: string = '';
  product: Product;
  selectedImages: File[] = [];

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
      this.productId = params['id'];
    });

    this.productService.getProductById(this.productId).subscribe({
      next: (response) => {
        this.product = response;
      },
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
}
