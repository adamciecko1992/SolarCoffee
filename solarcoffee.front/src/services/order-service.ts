import reachToApi from "./reachToApi-service";
import axios, { AxiosError, AxiosResponse } from "axios";
import { ISalesOrder } from '@/types/SalesOrder';
export class OrderService {
  API_URL = "https://localhost:5001/api";

  public async getOrders(): Promise<AxiosError<any> | AxiosResponse<ISalesOrder[]>> {
    const result = await reachToApi<ISalesOrder[]>('get', 'order');
    return result;
  }

  public async markOrderComplete(id: number): Promise<AxiosError<any> | AxiosResponse<ISalesOrder>> {
    const result = await reachToApi<ISalesOrder>("patch", `order/complete/${id}`, id);
    return result;
  }
}