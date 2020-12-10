import axios, { AxiosError, AxiosResponse } from "axios";
import { ICustomer } from "@/types/Customer";
// import { IServiceResponse } from "@/types/ServiceResponse";
import reachAPI from "./reachToApi-service"



export default class CustomerService {
  public static async getCustomers() {
    const result = await reachAPI<ICustomer[]>("get", "customer");
    return result;
  }

  public async getCustomerById(id: number): Promise<any> {
    const result = await reachAPI("get", `customer/${id}`);
    return result;
  }

  public async addCustomer(newCustomer: ICustomer) {
    let result: AxiosError | AxiosResponse<ICustomer>;
    try {
      result = await reachAPI<ICustomer>("post", "customer", newCustomer);
    } catch (error) {
      result = error as AxiosError;
    }
    return result;
  }

  public async deleteCustomer(customerId: number): Promise<boolean> {
    const result: any = await axios.delete(
      `https://localhost:5001/api/customer/${customerId}`
    );
    return result;
  }
}
