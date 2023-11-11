import { Component } from '@angular/core';
import { Product } from 'src/app/core/models/domain/product';
import { ProductService } from '../service/product.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css'],
})
export class AddProductComponent {
  product: Product;
  selectedImages: File[] = [];

  constructor(private productService: ProductService) {
    this.product = {
      name: '',
    };
  }

  onImageUpload(event: any) {
    this.selectedImages = event.target.files;
  }

  onFormSubmit(): void {
    const formData = this.createFormData(this.product);
    this.productService.upsertProduct(formData).subscribe({
      next: (response) => {
        this.product = response as Product;
        console.log('success');
      },
      error: (error) => {
        console.error('Error:', error);
      },
    });
  }

  createFormData(product: Product): FormData {
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
