import axios from "axios";
import { IProduct } from "@/types/Product";
import reachToApi from "./reachToApi-service";

export default class ProductService {
  async archiveProduct(id: number) {
    const response = await reachToApi('patch', `product/${id}`, id);
    return response;
  }

  async saveNewProduct(product: IProduct) {
    const response = await reachToApi<IProduct>('post', `product/`, product);
    return response;
  }
}
