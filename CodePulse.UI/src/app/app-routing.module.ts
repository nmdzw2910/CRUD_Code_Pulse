import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { ProductListComponent } from './features/product/product-list/product-list.component';
import { AddProductComponent } from './features/product/add-product/add-product.component';
import { EditProductComponent } from './features/product/edit-product/edit-product.component';
import { ProductDetailComponent } from './features/product/product-detail/product-detail.component';

const routes: Routes = [
  {
    path: 'admin/products',
    component: ProductListComponent,
  },
  {
    path: 'admin/products/add',
    component: AddProductComponent,
  },
  {
    path: 'admin/products/edit/:id',
    component: EditProductComponent,
  },
  {
    path: 'admin/products/detail/:id',
    component: ProductDetailComponent,
  },
  {
    path: 'admin/categories',
    component: CategoryListComponent,
  },
  {
    path: 'admin/categories/add',
    component: AddCategoryComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
