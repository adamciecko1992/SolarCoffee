import axios from "axios";
import { IProduct } from "@/types/Product";

export default class ProductService {
  async archiveProduct(id: number) {
    const response = await axios.patch(
      `https://localhost:5001/api/product/${id}`
    );
    return response.data;
  }

  async saveNewProduct(product: IProduct) {
    const response = await axios.post(
      "https://localhost:5001/api/product",
      product
    );
    return response.data;
  }
}
