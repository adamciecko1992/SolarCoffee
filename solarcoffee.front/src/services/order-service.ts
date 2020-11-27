  
import axios from "axios";


export class OrderService {
  API_URL = "https://localhost:5001/api";

  public async getOrders(): Promise<any> {
   const result = await axios.get(`${this.API_URL}/order/`);
    return result.data;
  }

  public async markOrderComplete(id: number): Promise<any> {
   const result = await axios.patch(`${this.API_URL}/order/complete/${id}`);
    return result.data;
  }
}