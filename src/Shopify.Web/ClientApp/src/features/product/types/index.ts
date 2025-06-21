export type ProductPaged = {
  products: Product[];
  total: number;
  skip: number;
  limit: number;
};

export type Product = {
  id: number;
  title: string;
  description: string;
  category: string;
  brand: string;
  sku: string;
  price: number;
  discountPercentage?: number;
  rating: number;
  stock: number;
  minimumOrderQuantity: number;
  dimensions: Dimensions;
  weight: number;
  warrantyInformation?: string;
  shippingInformation?: string;
  availabilityStatus?: string;
  returnPolicy?: string;
  thumbnail: string;
  meta: Meta;
  images: string[];
  tags: string[];
  reviews?: Review[];
};

export type Dimensions = {
  width: number;
  height: number;
  depth: number;
};

export type Meta = {
  createdAt: string;
  updatedAt: string;
  barcode: string;
  qrCodeUrl: string;
};

export type Review = {
  rating: number;
  comment: string;
  reviewerName: string;
  reviewerEmail: string;
  date: string;
};
