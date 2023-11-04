import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { Product } from 'src/app/core/models/domain/product';
import { ProductService } from '../service/product.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css'],
})
export class AddProductComponent {
  model: Product;
  private addProductSubscription?: Subscription;
  successMessage: string = '';
  errorMessage: string = '';

  constructor(private productService: ProductService) {
    this.model = {
      name: '',
    };
  }
  onFormSubmit() {
    this.productService.upsertProduct(this.model).subscribe(
      (response) => {
        // Handle success
        this.successMessage = 'Product added successfully';
        this.model = {
          name: '',
        }; // Clear the form
      },
      (error) => {
        // Handle error
        this.errorMessage = 'Error adding the product';
        console.error('Error:', error);
      }
    );
  }
}
