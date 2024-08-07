import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HeaderComponent } from './core/components/header/header.component';
import { BaseImageComponent } from './base/base-image/base-image.component';
import { UpsertProductComponent } from './features/product/upsert-product/upsert-product.component';
import { ProductListComponent } from './features/product/product-list/product-list.component';
import { FooterComponent } from './core/components/footer/footer.component';
import { ProductDetailComponent } from './features/product/product-detail/product-detail.component';
import { BaseLoadingComponent } from './base/base-loading/base-loading.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BaseButtonComponent } from './base/base-button/base-button.component';
import { BaseInputFieldComponent } from './base/base-input-field/base-input-field.component';
import { BaseCarouselComponent } from './base/base-carousel/base-carousel.component';
import { CategoryListComponent } from './features/category/category-list/category-list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HeaderComponent,
    BaseImageComponent,
    UpsertProductComponent,
    ProductListComponent,
    FooterComponent,
    ProductDetailComponent,
    BaseLoadingComponent,
    BaseButtonComponent,
    BaseInputFieldComponent,
    BaseCarouselComponent,
    CategoryListComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      preventDuplicates: true,
      progressBar: true,
    }),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
