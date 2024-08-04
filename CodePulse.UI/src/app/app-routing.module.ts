import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductListComponent } from './features/product/product-list/product-list.component';
import { UpsertProductComponent } from './features/product/upsert-product/upsert-product.component';
import { ProductDetailComponent } from './features/product/product-detail/product-detail.component';
import { CategoryListComponent } from './features/category/category-list/category-list.component';

const routes: Routes = [
  {
    path: 'admin/products',
    component: ProductListComponent,
  },
  {
    path: 'admin/products/add',
    component: UpsertProductComponent,
  },
  {
    path: 'admin/products/edit/:id',
    component: UpsertProductComponent,
  },
  {
    path: 'admin/product/:id',
    component: ProductDetailComponent,
  },
  {
    path: 'admin/categories',
    component: CategoryListComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
