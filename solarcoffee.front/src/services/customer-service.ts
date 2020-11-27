import axios from "axios";
import { ICustomer } from "@/types/Customer";
import { IServiceResponse } from "@/types/ServiceResponse";

export default class CustomerService {
  public async getCustomers(): Promise<ICustomer[]> {
    const result: any = await axios.get(`https://localhost:5001/api/customer/`);
    return result.data;
  }

  public async getCustomerById(id: number): Promise<ICustomer> {
    const result: any = await axios.get(
      `https://localhost:5001/api/customer/${id}`
    );
    return result.data;
  }

  public async addCustomer(
    newCustomer: ICustomer
  ): Promise<IServiceResponse<ICustomer>> {
    const result: any = await axios.post(
      `https://localhost:5001/api/customer`,
      newCustomer
    );
    return result.data;
  }

  public async deleteCustomer(customerId: number): Promise<boolean> {
    const result: any = await axios.delete(
      `https://localhost:5001/api/customer/${customerId}`
    );
    return result.data;
  }
}
