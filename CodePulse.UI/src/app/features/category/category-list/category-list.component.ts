import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {
  categories: string[] = [];
  newCategory: string = '';
  categoryToEdit: string | null = null;
  editedCategoryName: string = '';
  isEditing: boolean = false;
  isUpdating: boolean = false;

  constructor(private categoryService: CategoryService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.getAllCategories().subscribe({
      next: (data) => {
        this.categories = data;
      },
      error: (err) => {
        this.toastr.error(err.message);
      }
    })
  }

  addCategory(): void {
    if (this.newCategory.trim() === '') {
      this.toastr.error('Category name cannot be empty.');
      return;
    }

    this.categoryService.createCategory(this.newCategory).subscribe({
      next: (category) => {
        this.categories.push(category);
        this.newCategory = '';
        this.toastr.success('New category created');
      },
      error: (err) => {
        this.toastr.error(err.message);
      }
    });
  }

  editCategory(category: string): void {
    this.categoryToEdit = category;
    this.editedCategoryName = category;
    this.isEditing = true;
  }

  deleteCategory(category: string): void {
    this.categoryService.deleteCategory(category).subscribe({
      next: (category) => {
        this.toastr.success(category);
        this.loadCategories();
      },
      error: (err) => {
        this.toastr.error(err.message);
      }
    });
  }

  updateCategory(): void {
    if (this.editedCategoryName.trim() === '') {
      this.toastr.error('Category name cannot be empty.');
      return;
    }

    this.isUpdating = true;

    this.categoryService.updateCategory(this.categoryToEdit!, this.editedCategoryName).subscribe({
      next: () => {
        const index = this.categories.indexOf(this.categoryToEdit!);
        if (index > -1) {
          this.categories[index] = this.editedCategoryName;
          this.categoryToEdit = null;
          this.editedCategoryName = '';
          this.isEditing = false;
          this.isUpdating = false;
          this.toastr.success('Category updated');
        }
      },
      error: (err) => {
        this.toastr.error(err.message);
        this.isUpdating = false;
      }
    });
  }

  cancelEdit(): void {
    this.categoryToEdit = null;
    this.editedCategoryName = '';
    this.isEditing = false;
  }
}
