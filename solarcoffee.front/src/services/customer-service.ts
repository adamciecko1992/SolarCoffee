import axios, { AxiosError, AxiosResponse } from "axios";
import { ICustomer } from "@/types/Customer";
// import { IServiceResponse } from "@/types/ServiceResponse";
import reachAPI from "./reachToApi-service"
import handleErrors from '@/helpers/handleErrors';
import isValidResponse from "@/helpers/axiosTypeGuard";


export default class CustomerService {
  public async getCustomers(): Promise<AxiosResponse<ICustomer[]> | AxiosError> {
    const result: AxiosResponse<ICustomer[]> | AxiosError = await reachAPI<ICustomer[]>("get", "customer");
    return result;
  }

  public async getCustomerById(id: number): Promise<any> {
    const result = await reachAPI("get", `customer/${id}`);
    return result;
  }

  public async addCustomer(newCustomer: ICustomer) {
    const result = await reachAPI("post", "customer", newCustomer);
  }

  public async deleteCustomer(customerId: number): Promise<boolean> {
    const result: any = await axios.delete(
      `https://localhost:5001/api/customer/${customerId}`
    );
    return result.data;
  }
}
