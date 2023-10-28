import { ProductImage } from './product-image';
export interface Product {
  id?: string;
  name: string;
  description?: string;
  price?: number;
  stock?: number;
  brand?: string;
  category?: string;
  productImages?: ProductImage[];
}
