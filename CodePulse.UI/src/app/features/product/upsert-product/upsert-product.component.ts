import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/core/models/domain/product';
import { ProductService } from '../service/product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'upsert-product',
  templateUrl: './upsert-product.component.html',
  styleUrls: ['./upsert-product.component.css'],
})
export class UpsertProductComponent implements OnInit {
  isLoading: boolean = false;
  productId: string = '';
  product: Product;
  selectedImages: File[] = [];
  isEdit: boolean = false;
  imagePreviews: any[] = [];
  isUpdatingPicture: boolean = false;
  isSaving: boolean = false;

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private sanitizer: DomSanitizer
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
    this.isUpdatingPicture = true;
    this.selectedImages = event.target.files;
    this.getPreviewPictures(this.selectedImages);
  }

  onFormSubmit(): void {
    this.isSaving = true;
    const formData = this.upsertFormData(this.product);
    this.productService.upsertProduct(formData).subscribe({
      next: (response) => {
        this.product = response;
        this.toastr.success('success');
        this.router.navigate([`admin/products/edit/${this.product.id}`]);
        this.isSaving = false;
      },
      error: (error) => {
        this.toastr.error(error);
        this.isSaving = false;
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
    this.isLoading = true;
    this.productService.getProductById(productId).subscribe({
      next: (response) => {
        this.product = response;
        this.isLoading = false;
        this.isUpdatingPicture = false;
        this.getPreviewPictures(this.selectedImages);
      },
      error: (error) => {
        this.toastr.error(error);
        this.isLoading = false;
      },
    });
  }

  private getPreviewPictures(images: File[]): void {
    this.imagePreviews = [];

    for (const img of images) {
      const reader = new FileReader();

      reader.onload = (e: any) => {
        const imageUrl = this.sanitizer.bypassSecurityTrustUrl(e.target.result);
        this.imagePreviews.push(imageUrl);
      };

      reader.readAsDataURL(img);
    }
  }
}
