import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HeaderComponent } from './core/components/header/header.component';
import { BaseImageComponent } from './base/base-image/base-image.component';
import { UpsertProductComponent } from './features/product/upsert-product/upsert-product.component';
import { ProductListComponent } from './features/product/product-list/product-list.component';
import { FooterComponent } from './core/components/footer/footer.component';
import { ProductDetailComponent } from './features/product/product-detail/product-detail.component';
import { BaseLoadingComponent } from './base/base-loading/base-loading.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    CategoryListComponent,
    AddCategoryComponent,
    HeaderComponent,
    BaseImageComponent,
    UpsertProductComponent,
    ProductListComponent,
    FooterComponent,
    ProductDetailComponent,
    BaseLoadingComponent,
  ],
  imports: [BrowserModule, AppRoutingModule, FormsModule, HttpClientModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
